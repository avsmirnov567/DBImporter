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
using System.Configuration;

namespace Server
{
    public partial class ServerForm : Form
    {
        private Thread QueueThread;
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            string queueName = Dns.GetHostName() + @"\private$\serverQueue";
            ConfigurationManager.AppSettings.Set("qPath", queueName);
            tbQueue.Text = queueName;
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
                    RecievingHandler.SendEncryptedSymmetricalKey(message.Body.ToString());
                }
            }
        }
    }
}
