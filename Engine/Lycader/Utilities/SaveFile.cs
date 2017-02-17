
namespace Lycader.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Xml;

    public class SaveFile : Dictionary<string, string>
    {
        public SaveFile()
        {
        }

        ~SaveFile()
        {
            this.Clear();
        }

        public string FileName { get; private set; } = string.Empty;

        public void Load(string fileName)
        {
            this.FileName = fileName;

            if (!System.IO.File.Exists(fileName))
            {
                return;
            }

            this.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);

            foreach (XmlNode Node in doc.SelectSingleNode("SaveFile").ChildNodes)
            {
                string Name = Node.Name;
                string Value = Node.InnerText;

                this.Add(Name, Value);
            }
        }

        public bool Save()
        {
            if (this.FileName != string.Empty)
            {
                return SaveAs(this.FileName);
            }

            return false;
        }

        public bool SaveAs(string fileName)
        {
            this.FileName = fileName;

            XmlWriterSettings writerSettings = new XmlWriterSettings(); ;
            writerSettings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(FileName, writerSettings))
            {
                writer.WriteStartElement("SaveFile");

                foreach (string key in this.Keys)
                {
                    writer.WriteElementString(key, this[key]);
                }

                writer.WriteEndElement();
            }

            return true;
        }

        public void AddInt(string NodeName, int Value)
        {
            int tempvalue = 0;

            if (this.ContainsKey(NodeName))
            {
                string temp = this[NodeName];
                try
                {
                    tempvalue = int.Parse(temp);
                }
                finally
                {
                    tempvalue += Value;
                }
            }
            this.Remove(NodeName);
            this.Add(NodeName, tempvalue.ToString());
        }

        public int GetInt(string NodeName)
        {
            int returnvalue = 0;

            if (this.ContainsKey(NodeName))
            {
                string temp = this[NodeName];
                try
                {
                    returnvalue = int.Parse(temp);
                }
                catch { }
                return returnvalue;
            }
            return returnvalue;
        }

        public void SetInt(string NodeName, int Value)
        {
            if (this.ContainsKey(NodeName))
            {
                this.Remove(NodeName);
                this.Add(NodeName, Value.ToString());
            }
        }

        public void ReplaceString(string NodeName, string NewValue)
        {
            if (this.ContainsKey(NodeName))
            {
                this.Remove(NodeName);
            }
            this.Add(NodeName, NewValue);
        }
    }
}