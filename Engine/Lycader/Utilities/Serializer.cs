using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lycader.Utilities
{
    static public class Serializer
    {
        static public void Save<T>(T values, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, values);
            }
        }

        static public T Read<T>(string path)
        {
            // Create a new serializer
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            // Create a StreamReader
            using (StreamReader reader = new StreamReader(path))
            {
                return (T)serializer.Deserialize(reader);
            }       
        }
    }
}
