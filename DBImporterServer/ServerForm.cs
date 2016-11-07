using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.Threading;
using System.Configuration;
using System.Net;
using Common;

namespace DBImporterServer
{
    public partial class ServerForm : Form
    {
        delegate void UpdateUI();
        private Thread QueueThread;
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            QueueThread = new Thread(QueueWork);
            QueueThread.IsBackground = true;
            QueueThread.Start();
        }

        private void QueueWork()
        {
            string queuePath = Dns.GetHostName() + ConfigurationManager.AppSettings["server_queue_path"];
            UpdateUI d = new UpdateUI(UpdateUIWithQueuePath);
            Invoke(d);
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
                if (message.Label == "path")
                {
                    DataBlob clientPath = (DataBlob)Serializer.DeserializeObject(Convert.FromBase64String(message.Body.ToString()));
                    ClientConnectionHandler.SendRSAKeyToClientQueue(clientPath.Data);
                }
            }
        }

        private void UpdateUIWithQueuePath()
        {
            Text = Dns.GetHostName() + ConfigurationManager.AppSettings["server_queue_path"];
        }
    }
}
