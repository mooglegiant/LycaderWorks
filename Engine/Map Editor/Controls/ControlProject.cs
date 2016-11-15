//-----------------------------------------------------------------------
// <copyright file="ControlProject.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor.Controls
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Custom Project Control (Holds Map and Tile Controls)
    /// </summary>
    public class ControlProject : Control
    {
        /// <summary>
        /// Control's Tab Control
        /// </summary>
        private TabControl tabs;

        /// <summary>
        /// Tab for Tiles
        /// </summary>
        private TabPage tilesTab;

        /// <summary>
        /// Tab for the Map
        /// </summary>
        private TabPage mapTab;

        /// <summary>
        /// Initializes a new instance of the ControlProject class
        /// </summary>
        public ControlProject()
        {
            this.InitializeComponent();

            Project.TileArray = new PictureBox[1];
            Project.Textures = new Bitmap[1];

            this.tilesTab.Controls.Add(GlobalControls.Tiles);
            this.mapTab.Controls.Add(GlobalControls.Map);
        }

        /// <summary>
        /// Control Initialization
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tilesTab = new System.Windows.Forms.TabPage();
            this.mapTab = new System.Windows.Forms.TabPage();
            this.tabs.SuspendLayout();
            this.SuspendLayout();

            // tabs
            this.tabs.Controls.Add(this.tilesTab);
            this.tabs.Controls.Add(this.mapTab);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "Tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(592, 566);
            this.tabs.TabIndex = 0;

            // tilesTab
            this.tilesTab.AutoScroll = true;
            this.tilesTab.Location = new System.Drawing.Point(4, 22);
            this.tilesTab.Name = "tilesTab";
            this.tilesTab.Padding = new System.Windows.Forms.Padding(3);
            this.tilesTab.Size = new System.Drawing.Size(584, 540);
            this.tilesTab.TabIndex = 1;
            this.tilesTab.Text = "Tiles";
            this.tilesTab.BackColor = Color.Gray;

            // mapTab
            this.mapTab.AutoScroll = true;
            this.mapTab.Location = new System.Drawing.Point(4, 22);
            this.mapTab.Name = "mapTab";
            this.mapTab.Padding = new System.Windows.Forms.Padding(3);
            this.mapTab.Size = new System.Drawing.Size(584, 540);
            this.mapTab.TabIndex = 2;
            this.mapTab.Text = "Map";
            this.mapTab.BackColor = Color.Gray;

            // FormProject
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.tabs);
            this.Name = "FormProject";
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
