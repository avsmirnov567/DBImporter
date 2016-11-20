using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.Net;
using System.Configuration;
using System.Net.Sockets;
using Common;

namespace Client
{
    class SendingHandler
    {
        public static bool SendByMSMQ(string input)
        {
            string queuePath = Dns.GetHostName() + @"\private$\serverQueue";
            bool result = false;
            try
            {
                MessageQueue queue = null;
                if (!MessageQueue.Exists(queuePath))
                {
                    queue = MessageQueue.Create(queuePath);
                }
                else
                {
                    queue = new MessageQueue(queuePath);
                }

                string encrypted = Encryptor.EncryptSymmetrical(input, ConfigurationManager.AppSettings["tdesKey"]);
                queue.Send(encrypted, "Data");
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            return result;
        }

        public static bool SendBySocket(string input)
        {
            string encrypted = Encryptor.EncryptSymmetrical(input, ConfigurationManager.AppSettings["tdesKey"]);
            byte[] toSend = Convert.FromBase64String(encrypted);
            try
            {
                IPAddress ip = new IPAddress(long.Parse(ConfigurationManager.AppSettings["ip"]));
                IPEndPoint serverPoint = new IPEndPoint(ip, 11000);
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    clientSocket.Connect(serverPoint);
                    int bytesSent = clientSocket.Send(toSend);
                    clientSocket.Shutdown(SocketShutdown.Send);
                    clientSocket.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
