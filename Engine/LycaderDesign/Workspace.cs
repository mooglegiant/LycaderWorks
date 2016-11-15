using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lycader;
using Lycader.Graphics;
using Lycader.Maps;

namespace LycaderDesign
{
    static public class Workspace
    {
        static public Dictionary<string, Animation> Animations { get; set; } = new Dictionary<string, Animation>();

        static public Dictionary<string, string> Images { get; set; } = new Dictionary<string, string>();

        static public Dictionary<string, TileMap> TileMaps { get; set; } = new Dictionary<string, TileMap>();

        static public string FilePathRoot = string.Empty;

        static public void OpenWorkspace(string filePathRoot)
        {
            FilePathRoot = filePathRoot;
            CreateDirectories();
        }

        static public void CreateDirectories()
        {
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets"));   
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "Animations"));
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "Audio"));
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "Images"));
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "TileMaps"));
        }

    }
}
