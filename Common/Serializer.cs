using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    public class Serializer
    {
        public static byte[] SerializeObject(object serializableObject)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                bf.Serialize(stream, serializableObject);
                return stream.ToArray();
            }
        }

        public static object DeserializeObject(byte[] inputData)
        {
            Stream stream = new MemoryStream(inputData);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }
    }
}
