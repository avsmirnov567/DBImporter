﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Messaging;
using Common;

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
                
                queue.Send(GetPreparedRequest(), "Key");
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        private static string GetPreparedRequest()
        {
            DataBlob dBlob = new DataBlob();
            dBlob.Data = ConfigurationManager.AppSettings["qPath"];
            dBlob.Key = ConfigurationManager.AppSettings["rsaPublic"];
            return Convert.ToBase64String(Serializer.SerializeObject(dBlob));
        }
    }
}
