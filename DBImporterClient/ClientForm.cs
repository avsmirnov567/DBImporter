using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Messaging;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using Common;

namespace DBImporterClient
{
    public partial class ClientForm : Form
    {
        delegate void UpdateUI();
        private Thread QueueTread;

        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            QueueTread = new Thread(QueueWork);
            QueueTread.IsBackground = true;
            QueueTread.Start();
        }

        private void QueueWork()
        {
            string queuePath = Dns.GetHostName() + ConfigurationManager.AppSettings["local_queue_path"];
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
                if (message.Label == "key")
                {
                    DataBlob blob = (DataBlob)Serializer.DeserializeObject(Convert.FromBase64String(message.Body.ToString()));
                    ConfigurationManager.AppSettings.Set("RSA", blob.Data);
                    UpdateUI d = new UpdateUI(UpdateUIAfterRSARecieved);
                    Invoke(d);
                }
            }
        }

        private void UpdateUIAfterRSARecieved()
        {
            connectBtn.Text = "RSA ключ получен";
            connectBtn.Enabled = false;
            serverPathTextBox.Enabled = false;
            loadBtn.Enabled = true;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV files | *.csv";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                string path = dialog.FileName;
                try
                {
                    string result = CSVReader.ReadCSV(path);
                    loadedDataTextBox.Text = result;
                    if (result.Length > 0)
                        sendBtn.Enabled = true;
                    else
                        MessageBox.Show("Файл пуст!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {

        }

        private void serverPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (serverPathTextBox.Text.Length > 0)
                connectBtn.Enabled = true;
            else
                connectBtn.Enabled = false;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageQueue.Exists(serverPathTextBox.Text))
                    ServerConnectionHandler.SendLocalQueuePathToServerQueue(serverPathTextBox.Text);
                else
                    MessageBox.Show("Указан неверный путь к очереди сервера, либо очередь не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
