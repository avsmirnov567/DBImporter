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
using Common;

namespace Client
{
    public partial class ClientConnectForm : Form
    {
        private Thread QueueThread;
        public ClientConnectForm()
        {
            InitializeComponent();
        }

        private void ClientConnectForm_Load(object sender, EventArgs e)
        {
            Encryptor.GenerateRSAKeys();
            ConfigurationManager.AppSettings.Set("rsaPrivate", Encryptor.PrivateKey);
            ConfigurationManager.AppSettings.Set("rsaPublic", Encryptor.PublicKey);
            string queueName = Dns.GetHostName() + @"\private$\clientQueue";
            ConfigurationManager.AppSettings.Set("qPath", queueName);
            QueueThread = new Thread(QueueWork);
            QueueThread.IsBackground = true;
            QueueThread.Start();
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
                    MessageBox.Show("Ты получил вонючий ключ: "+ message.Body.ToString());                 
                }
            }
        }

        private void tbQueue_TextChanged(object sender, EventArgs e)
        {
            if (tbQueue.Text.Length > 0)
                btnConnect.Enabled = true;
            else
                btnConnect.Enabled = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbQueue.Text.Length > 0)
                {
                    bool result = KeySendingHandler.SendPublicRsaToServerQueue(tbQueue.Text);
                    if (!result)
                        MessageBox.Show("Не удалось подключиться, проверьте всё ещё раз!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Указан неверный путь к очереди, либо очередь не существует", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
