//-----------------------------------------------------------------------
// <copyright file="ControlTiles.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Custom Tile Selector Control
    /// </summary>
    public partial class ControlTiles : Control
    {
        /// <summary>
        /// Image of all the tiles
        /// </summary>
        private Bitmap image;

        /// <summary>
        /// Initializes a new instance of the ControlTiles class
        /// </summary>
        public ControlTiles()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
            this.BackColor = Color.Gray;
            this.DoubleBuffered = true;

            this.MouseDown += new MouseEventHandler(this.ControlTiles_MouseDown);
            this.MouseUp += new MouseEventHandler(this.ControlTiles_MouseUp);
            this.MouseMove += new MouseEventHandler(this.ControlTiles_MouseMove);
        }

        /// <summary>
        /// Updates the control
        /// </summary>
        public void ControlUpdate()
        {
            if (!string.IsNullOrEmpty(Project.TileFile))
            {
                this.LoadTexture();
            }
            else
            {
                this.BackgroundImage = new Bitmap(1, 1);
                this.Width = 1;
                this.Height = 1;
            }
        }

        /// <summary>
        /// Draws the selction box if available
        /// </summary>
        /// <param name="pe">OnPaint Event Args</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (Project.TileSelectionBox.Box.Width > 0)
            {
                pe.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 30, 30, 30)), Project.TileSelectionBox.Box.Left, Project.TileSelectionBox.Box.Top, Project.TileSelectionBox.Box.Width, Project.TileSelectionBox.Box.Height);
                pe.Graphics.DrawRectangle(new Pen(Color.LimeGreen), Project.TileSelectionBox.Box.Left, Project.TileSelectionBox.Box.Top, Project.TileSelectionBox.Box.Width, Project.TileSelectionBox.Box.Height);
            }
        }

        /// <summary>
        /// Control's MouseDown Event Handler
        /// </summary>
        /// <param name="sender">Active Control</param>
        /// <param name="e">MouseEvent Args</param>
        protected void ControlTiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Project.TileSelectionBox.ClickStart(e.X, e.Y);
            }
        }

        /// <summary>
        /// Control's MouseMove Event Handler
        /// </summary>
        /// <param name="sender">Active Control</param>
        /// <param name="e">MouseEvent Args</param>
        protected void ControlTiles_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Project.TileSelectionBox.Move(e.X, e.Y);
            }

            this.Invalidate();
        }

        /// <summary>
        /// Control's MouseUp Event Handler
        /// </summary>
        /// <param name="sender">Active Control</param>
        /// <param name="e">MouseEvent Args</param>
        protected void ControlTiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Project.TileSelectionBox.ClickEnd(e.X, e.Y);
                this.GetSelection();
            }

            this.Invalidate();
        }

        /// <summary>
        /// Loades the texture image and creates the tileset
        /// </summary>
        private void LoadTexture()
        {
            Bitmap bitmap;

            try
            {
                bitmap = new Bitmap(Project.TileFile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            GlobalControls.Tiles.Controls.Clear();

            int horizontalCount = bitmap.Width / Project.Map.TileSize;
            int verticalCount = bitmap.Height / Project.Map.TileSize;
            int tileCount = horizontalCount * verticalCount;
            int x = 0;
            int y = 0;

            Project.Textures = new Bitmap[tileCount];
            Project.TileArray = new PictureBox[tileCount];
            Color pixel;

            for (int i = 0; i < tileCount; i++)
            {
                Project.Textures[i] = new Bitmap(Project.Map.TileSize, Project.Map.TileSize);
                for (int j = 0; j < Project.Map.TileSize; j++)
                {
                    for (int k = 0; k < Project.Map.TileSize; k++)
                    {
                        int offsetWidth = (i * Project.Map.TileSize) + j;
                        int offsetHeight = k;
                        int multiplierY = offsetWidth / bitmap.Width;
                        if (multiplierY > 0)
                        {
                            offsetWidth = offsetWidth % bitmap.Width;
                            offsetHeight = (multiplierY * Project.Map.TileSize) + k;
                        }

                        pixel = Color.FromArgb(bitmap.GetPixel(offsetWidth, offsetHeight).ToArgb());
                        Project.Textures[i].SetPixel(j, k, pixel);
                    }
                }

                Project.TileArray[i] = new PictureBox();
                Project.TileArray[i].Image = Project.Textures[i];
                Project.TileArray[i].Width = Project.Map.TileSize;
                Project.TileArray[i].Height = Project.Map.TileSize;
                Project.TileArray[i].Padding = new Padding(1, 1, 0, 0);
                Project.TileArray[i].Name = i.ToString();

                if (x == horizontalCount)
                {
                    x = 0;
                    y++;
                }

                Project.TileArray[i].Location = new Point((x * Project.Map.TileSize) + x, (y * Project.Map.TileSize) + y);
                x++;
            }

            this.Width = (x * Project.Map.TileSize) + x + 1;
            this.Height = (y * Project.Map.TileSize) + y + 1;
            Project.TileSelectionBox = new Selection(this.Width, this.Height);

            this.image = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(this.image);
            g.FillRectangle(new SolidBrush(Project.Map.Background), 0, 0, this.Width, this.Height);

            for (int i = 0; i < Project.TileArray.Length; i++)
            {
                g.DrawImageUnscaled(
                    Project.TileArray[i].Image,
                    Project.TileArray[i].Location.X + 1,
                    Project.TileArray[i].Location.Y + 1);

                g.DrawRectangle(
                    new Pen(Color.Black),
                    Project.TileArray[i].Location.X,
                    Project.TileArray[i].Location.Y,
                    Project.Map.TileSize + 1,
                    Project.Map.TileSize + 1);
            }

            this.BackgroundImage = this.image;
        }

        /// <summary>
        /// Gets the selected tiles and stores it for drawing on the map
        /// </summary>
        private void GetSelection()
        {
            Project.MapSelectionBox.ClickStart(0, 0);
            Project.MapSelectionBox.ClickEnd(0, 0);

            Project.Selection = new int[Project.TileSelectionBox.Box.Width / (Project.Map.TileSize + 1), Project.TileSelectionBox.Box.Height / (Project.Map.TileSize + 1)];

            int x = 0;
            int y = 0;

            for (int i = 0; i < Project.TileArray.Length; i++)
            {
                if (Project.TileArray[i].Location.X >= Project.TileSelectionBox.Box.Left && Project.TileArray[i].Location.X < Project.TileSelectionBox.Box.Right)
                {
                    if (Project.TileArray[i].Location.Y >= Project.TileSelectionBox.Box.Top && Project.TileArray[i].Location.Y < Project.TileSelectionBox.Box.Bottom)
                    {
                        if (x * (Project.Map.TileSize + 1) == Project.TileSelectionBox.Box.Width)
                        {
                            x = 0;
                            y++;
                        }

                        Project.Selection[x, y] = int.Parse(Project.TileArray[i].Name);
                        x++;
                    }
                }
            }
        }
    }
}
