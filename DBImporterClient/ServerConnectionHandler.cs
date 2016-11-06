using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Configuration;
using System.Messaging;

namespace DBImporterClient
{
    class ServerConnectionHandler
    {
        public static bool SendLocalQueuePathToServerQueue (string serverQueuePath)
        {
            bool answer = false;
            byte[] dataToSend = GetPreparedLocalQueuePath();
            try
            {
                MessageQueue q = new MessageQueue(serverQueuePath);
                q.Send(dataToSend);
                answer = true;
            }
            catch
            {
                answer = false;
            }
            return answer;
        }

        public static byte[] GetPreparedLocalQueuePath()
        {
            DataBlob message = new DataBlob();       
            message.Data = ConfigurationManager.AppSettings["local_queue_path"];
            message.Type = DataType.CLIENT_QUEUE_ADRESS;
            return Serializer.SerializeObject(message);
        }
    }
}
