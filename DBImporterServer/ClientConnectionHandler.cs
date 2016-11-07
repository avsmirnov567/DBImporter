using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Messaging;
using Common;

namespace DBImporterServer
{
    class ClientConnectionHandler
    {
        public static bool SendRSAKeyToClientQueue(string clientQueuePath)
        {
            bool answer = false;
            byte[] dataToSend = GetPreparedRSA();
            try
            {
                MessageQueue q = new MessageQueue(clientQueuePath);
                q.Send(Convert.ToBase64String(dataToSend), "key");
                answer = true;
            }
            catch
            {
                answer = false;
            }
            return answer;
        }

        private static byte[] GetPreparedRSA()
        {
            DataBlob blob = new DataBlob();
            blob.Data = ConfigurationManager.AppSettings["RSA_public"];
            blob.Type = DataType.PUBLIC_RSA;
            blob.Key = null;
            return Serializer.SerializeObject(blob);
        }
    }
}
