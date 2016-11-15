using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;

using LycaderWorksEngine.Graphics;

namespace LycaderWorksEngine.IO
{
    public class PakFile
    {
        /// <summary>
        /// Loaded file data
        /// </summary>
        Dictionary<string, byte[]> Data = new Dictionary<string, byte[]>();

        public Stream GetResource(string fileName)
        {
            if (Data.ContainsKey(fileName))
            {
                return new MemoryStream(Data[fileName]);
            }
            return new MemoryStream();
        }

        public void LoadStreams()
        {
            foreach (KeyValuePair<string, byte[]> kvp in Data)
            {
                if (kvp.Key.Contains("Fonts"))
                {
                    string fileName = kvp.Key.Remove(0, kvp.Key.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    TextureManager.Load(fileName, GetResource(kvp.Key));
                }
            }
        }

        public void OpenPak(string filePath)
        {
            // File exists ??
            if (File.Exists(filePath) == false)
            {
                return;
            }

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                ClosePak();

                //TODO check if the bank is a zip file
                ZipInputStream zip = new ZipInputStream(fileStream);

                // Foreach file in the bank
                ZipEntry entry;
                while ((entry = zip.GetNextEntry()) != null)
                {
                    // If it isn't a file, skip it
                    if (!entry.IsFile)
                    {
                        continue;
                    }

                    // Uncompress data to a buffer
                    byte[] data = new byte[entry.Size];
                    zip.Read(data, 0, (int)entry.Size);

                    // Adds data to the list
                    Data[entry.Name] = data;
                }
            }
        }

        public void ClosePak()
        {
            Data.Clear();
        }

        public bool SavePak(string filePath)
        {
            // Return value
            bool retval = false;

            FileStream stream = null;
            ZipOutputStream zip = null;
            try
            {
                stream = File.Create(filePath);
                zip = new ZipOutputStream(stream);
                zip.SetLevel(5);

                // Save all Binaries
                foreach (KeyValuePair<string, byte[]> kvp in Data)
                {
                    ZipResource(zip, kvp.Key, kvp.Value);
                }

                retval = true;

            }
            catch (Exception e)
            {
            }
            finally
            {
                if (zip != null)
                {
                    zip.Finish();
                    zip.Close();
                }
                if (stream != null)
                    stream.Close();
            }

            return retval;
        }

        public void AddResource(string zipPath, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            byte[] data = null;
            if (!File.Exists(filePath))
            {
                return;
            }

            // Opens the file and copy it to memory
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            if (stream == null)
            {
                return;
            }

            data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            stream.Close();
            Data.Add(zipPath, data);
        }

        public void RemoveResource(string fileName)
        {
            if (Data.ContainsKey(fileName))
            {
                Data.Remove(fileName);
            }
        }

        private bool ZipResource(ZipOutputStream stream, string resourcename, byte[] data)
        {
            // Bad args
            if (stream == null || string.IsNullOrEmpty(resourcename) || data == null)
            {
                return false;
            }

            ZipEntry entry = new ZipEntry(resourcename);
            stream.PutNextEntry(entry);
            stream.Write(data, 0, data.Length);
            return true;
        }
    }
}
