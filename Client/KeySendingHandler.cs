using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Messaging;

namespace Client
{
    class KeySendingHandler
    {
        public static bool SendPublicRsaToServerQueue(string queuePath)
        {
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
                queue.Send(ConfigurationManager.AppSettings["rsaPublic"], "Key");
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
