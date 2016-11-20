using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Messaging;
using System.IO;
using System.Net.Sockets;
using Common;

namespace Client
{
    public partial class ClientForm : Form
    {
        private Thread QueueThread;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientConnectForm_Load(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings.Set("ip", IPAddress());
            Encryptor.GenerateRSAKeys();
            ConfigurationManager.AppSettings.Set("rsaPrivate", Encryptor.PrivateKey);
            ConfigurationManager.AppSettings.Set("rsaPublic", Encryptor.PublicKey);
            string queueName = Dns.GetHostName() + @"\private$\clientQueue";
            ConfigurationManager.AppSettings.Set("qPath", queueName);
            QueueThread = new Thread(QueueWork);
            QueueThread.IsBackground = true;
            QueueThread.Start();
            SendKeyToServerQueue();
        }

        public static string IPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.Address.ToString();
                }
            }
            throw new Exception("IP не найден!");
        }

        private void SendKeyToServerQueue()
        {
            bool result = false;
            try
            {
                result = KeySendingHandler.SendPublicRsaToServerQueue(Dns.GetHostName() + @"\private$\serverQueue");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QueueWork()
        {
            string queuePath = ConfigurationManager.AppSettings["qPath"];
            MessageQueue queue = null;
            if (!MessageQueue.Exists(queuePath))
            {
                queue = MessageQueue.Create(queuePath);
            }
            else
            {
                queue = new MessageQueue(queuePath);
            }
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
            while (true)
            {
                System.Messaging.Message message = queue.Receive();
                if (message.Label == "Key")
                {
                    byte[] decrypted = Encryptor.DecryptAsymmetrical(Convert.FromBase64String(message.Body.ToString()),
                        ConfigurationManager.AppSettings["rsaPrivate"]);
                    ConfigurationManager.AppSettings.Set("tdesKey",Convert.ToBase64String(decrypted));
                    MessageBox.Show("Соединение с сервером установлено. Теперь можете импортировать данные из CSV.");
                    btnLoadCsv.Invoke((MethodInvoker)delegate
                    {
                        btnLoadCsv.Enabled = true;
                    });                
                }
            }
        }

        private void btnLoadCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV files | *.csv";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                string filePath = dialog.FileName;
                try
                {
                    List<string> lines = new List<string>();
                    var sr = new StreamReader(File.OpenRead(filePath));
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        lines.Add(line);
                    }
                    textBox1.Text = string.Join("\r\n", lines);
                    if (textBox1.Text.Length > 0)
                    {
                        btnSend.Enabled = true;
                    }
                    else
                    {
                        btnSend.Enabled = false;
                        MessageBox.Show("Файл пустой, попробуйте другой!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"{exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            bool result = false;
            if (rbtnMSMQ.Checked)
            {
                result = SendingHandler.SendByMSMQ(textBox1.Text);
            }
            else
            {
                result = SendingHandler.SendBySocket(textBox1.Text);
            }

            btnSend.Enabled = false;
            textBox1.Clear();
        }
    }
}
