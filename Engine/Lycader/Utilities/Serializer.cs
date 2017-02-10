//-----------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public static class Serializer
    {
        public static void Save<T>(T values, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, values);
            }
        }

        public static T Read<T>(string path)
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
