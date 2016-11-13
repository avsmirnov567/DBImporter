using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using Common;

namespace Server
{
    class RecievingHandler
    {
        private static byte[] symmetricalKey;
        public static bool SendEncryptedSymmetricalKey(string rsaRequest)
        {
            bool result = false;

            DataBlob input = (DataBlob)Serializer.DeserializeObject(Convert.FromBase64String(rsaRequest));
            string queuePath = input.Data;

            symmetricalKey = Encryptor.GenerateSymmetricalKey();
            byte[] encryptedKey = Encryptor.EncryptAsymmetrycal(symmetricalKey, input.Key);

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
                queue.Send(Convert.ToBase64String(encryptedKey), "Key");
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
