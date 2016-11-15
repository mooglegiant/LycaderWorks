//-----------------------------------------------------------------------
// <copyright file="FormEvents.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Does all the work of the form events, so we can call them outside of an Event
    /// </summary>
    public static class FormEvents
    {
        /// <summary>
        /// Resets the Map Editor
        /// </summary>
        public static void File_New()
        {
            Project.Initialize();
            GlobalControls.UpdateControls();
        }

        /// <summary>
        /// Opens a Map (And Texture if not loaded)
        /// </summary>
        public static void File_Open()
        {
            OpenFileDialog ofdOpenMap = new OpenFileDialog();
            ofdOpenMap.Filter = "Map Files (*.map)|*.map";
            ofdOpenMap.Title = "Open Map";
            if (ofdOpenMap.ShowDialog(GlobalForms.Master) == DialogResult.OK)
            {
                Project.MapFile = ofdOpenMap.FileName;
                Project.Map.Load(Project.MapFile);
            }

            if (string.IsNullOrEmpty(Project.TileFile))
            {
                File_LoadTexture();
            }

            GlobalForms.Master.SetNames(true);
            GlobalControls.UpdateControls();
            GlobalControls.ToolBar.UpdateLayerList();
        }

        /// <summary>
        /// Saves the current map as existing file
        /// </summary>
        public static void File_Save()
        {
            if (string.IsNullOrEmpty(Project.MapFile))
            {
                File_SaveAs();
            }
            else
            {
                Project.Map.Save(Project.MapFile);
            }

            GlobalControls.UpdateControls();
        }

        /// <summary>
        /// Saves the current map as a new file
        /// </summary>
        public static void File_SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Map Files (*.map)|*.map";
            saveFileDialog.Title = "Save Map As...";
            if (saveFileDialog.ShowDialog(GlobalForms.Master) == DialogResult.OK)
            {
                Project.MapFile = saveFileDialog.FileName;
                Project.Map.Name = new FileInfo(saveFileDialog.FileName).Name.Replace(".map", string.Empty);
                Project.Map.Save(Project.MapFile);
                GlobalForms.Master.SetNames(true);
            }

            GlobalControls.UpdateControls();
        }

        /// <summary>
        /// Opens a new texture for the tile selection
        /// </summary>
        public static void File_LoadTexture()
        {
            OpenFileDialog ofdOpenTexture = new OpenFileDialog();
            ofdOpenTexture.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ofdOpenTexture.Filter = "Image Files Files (*.bmp, *.jpg, *.gif, *.png)|*.bmp;*.jpg;*.gif;*.png|All Files (*.*)|*.*";
            ofdOpenTexture.Title = "Open Texture For Map";
            if (ofdOpenTexture.ShowDialog(GlobalForms.Master) == DialogResult.OK)
            {
                Project.TileFile = ofdOpenTexture.FileName;
            }

            GlobalControls.UpdateControls();
        }

        /// <summary>
        /// Exits the map editor
        /// </summary>
        public static void File_Exit()
        {
            Project.MapFile = string.Empty;
            Project.IsSaved = true;
            GlobalControls.UpdateControls();
        }
    }
}
