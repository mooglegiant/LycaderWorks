namespace LycaderDesign
{
    using System.Collections.Generic;
    using System.IO;

    using Lycader;
    using Lycader.Maps;

    public static class Workspace
    {
        public static Dictionary<string, Animation> Animations { get; set; } = new Dictionary<string, Animation>();

        public static Dictionary<string, string> Images { get; set; } = new Dictionary<string, string>();

        public static Dictionary<string, TileMap> TileMaps { get; set; } = new Dictionary<string, TileMap>();

        public static string FilePathRoot = string.Empty;

        public static void OpenWorkspace(string filePathRoot)
        {
            FilePathRoot = filePathRoot;
            CreateDirectories();
        }

        public static void CreateDirectories()
        {
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets"));   
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "Animations"));
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "Audio"));
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "Images"));
            Directory.CreateDirectory(Path.Combine(FilePathRoot, "Assets", "TileMaps"));
        }

    }
}
