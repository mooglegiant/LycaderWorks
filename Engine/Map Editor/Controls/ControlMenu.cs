//-----------------------------------------------------------------------
// <copyright file="ControlMenu.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Custom MenuStrip for the map editor
    /// </summary>
    public class ControlMenu : MenuStrip
    {
        #region Members
        /// <summary>
        /// Menu -> File
        /// </summary>
        private ToolStripMenuItem menuFile = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> File -> New
        /// </summary>
        private ToolStripMenuItem menuFileNew = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> File -> Open
        /// </summary>
        private ToolStripMenuItem menuFileOpen = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> File -> Seperator 1
        /// </summary>
        private ToolStripSeparator menuFileSeparator1 = new ToolStripSeparator();

        /// <summary>
        /// Menu -> File -> Load Texture
        /// </summary>
        private ToolStripMenuItem menuFileLoadTexture = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> File -> Seperator 2
        /// </summary>
        private ToolStripSeparator menuFileSeparator2 = new ToolStripSeparator();

        /// <summary>
        /// Menu -> File -> Save
        /// </summary>
        private ToolStripMenuItem menuFileSave = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> File -> Save As
        /// </summary>
        private ToolStripMenuItem menuFileSaveAs = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> File -> Seperator 3
        /// </summary>
        private ToolStripSeparator menuFileSeparator3 = new ToolStripSeparator();

        /// <summary>
        /// Menu -> File -> Exit
        /// </summary>
        private ToolStripMenuItem menuFileExit = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> Help
        /// </summary>
        private ToolStripMenuItem menuHelp = new ToolStripMenuItem();

        /// <summary>
        /// Menu -> Help -> About
        /// </summary>
        private ToolStripMenuItem menuHelpAbout = new ToolStripMenuItem();
        #endregion

        /// <summary>
        /// Initializes a new instance of the ControlMenu class
        /// </summary>
        public ControlMenu()
        {
            // menuFile
            this.menuFile.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.menuFileNew,
                this.menuFileOpen,
                this.menuFileSeparator1,
                this.menuFileLoadTexture,
                this.menuFileSeparator2,
                this.menuFileSave,
                this.menuFileSaveAs,
                this.menuFileSeparator3,
                this.menuFileExit
            });

            this.menuFile.Name = "fileToolStripMenuItem";
            this.menuFile.Size = new Size(35, 20);
            this.menuFile.Text = "&File";

            // menuFileNew
            this.menuFileNew.Image = global::MapEditor.Properties.Resources.page_white;
            this.menuFileNew.Name = "newToolStripMenuItem";
            this.menuFileNew.Size = new Size(124, 22);
            this.menuFileNew.Text = "&New";
            this.menuFileNew.Click += new System.EventHandler(this.MenuFileNew_Click);

            // menuFileOpen
            this.menuFileOpen.Image = global::MapEditor.Properties.Resources.folder;
            this.menuFileOpen.Name = "openToolStripMenuItem";
            this.menuFileOpen.Size = new Size(124, 22);
            this.menuFileOpen.Text = "&Open";
            this.menuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);

            // menuFileSeparator1
            this.menuFileSeparator1.Name = "toolStripSeparator1";
            this.menuFileSeparator1.Size = new Size(121, 6);

            // closeToolStripMenuItem
            this.menuFileLoadTexture.Name = "loadTextureToolStripMenuItem";
            this.menuFileLoadTexture.Size = new Size(124, 22);
            this.menuFileLoadTexture.Text = "Load &Texture";
            this.menuFileLoadTexture.Click += new System.EventHandler(this.MenuFileLoadTexture_Click);

            // menuFileSeparator2
            this.menuFileSeparator2.Name = "toolStripSeparator3";
            this.menuFileSeparator2.Size = new Size(121, 6);

            // menuFileSave
            this.menuFileSave.Image = global::MapEditor.Properties.Resources.disk;
            this.menuFileSave.Name = "saveToolStripMenuItem";
            this.menuFileSave.Size = new Size(124, 22);
            this.menuFileSave.Text = "&Save";
            this.menuFileSave.Click += new System.EventHandler(this.MenuFileSave_Click);

            // menuFileSaveAs
            this.menuFileSaveAs.Name = "saveAsToolStripMenuItem";
            this.menuFileSaveAs.Size = new Size(124, 22);
            this.menuFileSaveAs.Text = "Save &As";
            this.menuFileSaveAs.Click += new System.EventHandler(this.MenuFileSaveAs_Click);

            // menuFileSeparator3
            this.menuFileSeparator3.Name = "toolStripSeparator3";
            this.menuFileSeparator3.Size = new Size(121, 6);

            // menuFileExit
            this.menuFileExit.Name = "exitToolStripMenuItem";
            this.menuFileExit.Size = new Size(124, 22);
            this.menuFileExit.Text = "E&xit";
            this.menuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);

            // menuHelp
            this.menuHelp.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.menuHelpAbout
            });

            this.menuHelp.Name = "helpToolStripMenuItem";
            this.menuHelp.Size = new Size(40, 20);
            this.menuHelp.Text = "&Help";

            // menuHelpAbout
            this.menuHelpAbout.Name = "aboutToolStripMenuItem";
            this.menuHelpAbout.Size = new Size(114, 22);
            this.menuHelpAbout.Text = "&About";
            this.menuHelpAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);

            // ControlMenu
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Items.AddRange(new ToolStripItem[]
            {
                this.menuFile,
                this.menuHelp
            });

            this.Location = new Point(0, 0);
            this.Name = "menu";
            this.RenderMode = ToolStripRenderMode.Professional;
            this.Size = new Size(632, 24);
            this.TabIndex = 4;
            this.Text = "ControlMenu";
        }

        #region Events
        /// <summary>
        /// File -> New - Event Handler
        /// </summary>
        /// <param name="sender">ToolStripButton clicked</param>
        /// <param name="e">No Event Args</param>
        private void MenuFileNew_Click(object sender, EventArgs e)
        {
            FormEvents.File_New();
        }

        /// <summary>
        /// File -> Open - Event Handler
        /// </summary>
        /// <param name="sender">ToolStripButton clicked</param>
        /// <param name="e">No Event Args</param>
        private void MenuFileOpen_Click(object sender, EventArgs e)
        {
            FormEvents.File_Open();
        }

        /// <summary>
        /// File -> Load Texture - Event Handler
        /// </summary>
        /// <param name="sender">ToolStripButton clicked</param>
        /// <param name="e">No Event Args</param>
        private void MenuFileLoadTexture_Click(object sender, EventArgs e)
        {
            FormEvents.File_LoadTexture();
        }

        /// <summary>
        /// File -> Save - Event Handler
        /// </summary>
        /// <param name="sender">ToolStripButton clicked</param>
        /// <param name="e">No Event Args</param>
        private void MenuFileSave_Click(object sender, EventArgs e)
        {
            FormEvents.File_Save();
        }

        /// <summary>
        /// File -> Save As - Event Handler
        /// </summary>
        /// <param name="sender">ToolStripButton clicked</param>
        /// <param name="e">No Event Args</param>
        private void MenuFileSaveAs_Click(object sender, EventArgs e)
        {
            FormEvents.File_SaveAs();
        }

        /// <summary>
        /// File -> Exit - Event Handler
        /// </summary>
        /// <param name="sender">ToolStripButton clicked</param>
        /// <param name="e">No Event Args</param>
        private void MenuFileExit_Click(object sender, EventArgs e)
        {
            FormEvents.File_Exit();
        }

        /// <summary>
        /// Help -> About - Event Handler
        /// </summary>
        /// <param name="sender">ToolStripButton clicked</param>
        /// <param name="e">No Event Args</param>
        private void MenuHelpAbout_Click(object sender, EventArgs e)
        {
            GlobalForms.About.Show();
        }
        #endregion
    }
}
