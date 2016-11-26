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
using Server.Model;
using Common;

namespace Server
{
    public partial class ServerForm : Form
    {
        private Socket serverListener;
        private Thread QueueThread;
        private Thread SocketThread;
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            string queueName = Dns.GetHostName() + @"\private$\serverQueue";
            ConfigurationManager.AppSettings.Set("ip", IPAddress());
            ConfigurationManager.AppSettings.Set("qPath", queueName);
            QueueThread = new Thread(QueueWorker);
            QueueThread.IsBackground = true;
            QueueThread.Start();
            SocketThread = new Thread(SocketWorker);
            SocketThread.IsBackground = true;
            SocketThread.Start();
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

        private void QueueWorker()
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
                    RecievingHandler.SendEncryptedSymmetricalKey(message.Body.ToString());
                }
                else if (message.Label == "Data")
                {
                    string input = Encryptor.DecryptSymmetrical(message.Body.ToString(), ConfigurationManager.AppSettings["tdesKey"]);
                    int result = RecievingHandler.RecieveData(input);
                    tbInput.Invoke((MethodInvoker)delegate
                    {
                        tbInput.AppendText("\r\n" + result.ToString() + " новых записей добавлено.");
                    });
                }
            }
        }

        private void SocketWorker()
        {
            string ipString = ConfigurationManager.AppSettings["ip"];
            IPAddress ip = new IPAddress(long.Parse(ipString));
            int port = 11000;

            IPEndPoint endPoint = new IPEndPoint(ip, port);
            byte[] buf;
            List<byte> data;

            serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                serverListener.Bind(endPoint);
                serverListener.Listen(10);

                while (true)
                {
                    Socket handler = serverListener.Accept();
                    data = new List<byte>();

                    while (data.Count() % 1024 == 0)
                    {
                        buf = new byte[1024];
                        int recievedBytes = handler.Receive(buf);
                        byte[] temp = new byte[recievedBytes];
                        Array.Copy(buf, temp, recievedBytes);
                        data.AddRange(temp);
                    }

                    string recieved = Convert.ToBase64String(data.ToArray());
                    string decrypted = Encryptor.DecryptSymmetrical(recieved, ConfigurationManager.AppSettings["tdesKey"]);
                    int result = RecievingHandler.RecieveData(decrypted);
                    tbInput.Invoke((MethodInvoker)delegate
                    {
                        tbInput.AppendText("\r\n" + result.ToString() + " новых объектов добавлено.");
                    });
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (ThreadAbortException)
            {
                Thread.CurrentThread.Abort();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            QueueThread.Abort();
            MessageQueue.Delete(ConfigurationManager.AppSettings["qPath"]);     
            serverListener.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string text = "";
            using (MedicineContext db = new MedicineContext())
            {
                int deleted = 0;
                deleted += db.Database.ExecuteSqlCommand("DELETE FROM dbo.[Diagnoses]");
                deleted += db.Database.ExecuteSqlCommand("DELETE FROM dbo.[Patients]");
                deleted += db.Database.ExecuteSqlCommand("DELETE FROM dbo.[Doctors]");
                deleted += db.Database.ExecuteSqlCommand("DELETE FROM dbo.[Diseases]");
                deleted = deleted / 3;
                text += "Удалено: " + deleted.ToString() + " записи.";
            }
            tbInput.AppendText("\r\n" + text);
        }
    }
}
