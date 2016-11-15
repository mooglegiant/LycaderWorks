//-----------------------------------------------------------------------
// <copyright file="ControlMap.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Custom Map Control
    /// </summary>
    public class ControlMap : Control
    {
        /// <summary>
        /// Initializes a new instance of the ControlMap class
        /// </summary>
        public ControlMap()
        {
            this.SuspendLayout();

            // ControlMap
            this.ResumeLayout(false);
            this.MouseDown += new MouseEventHandler(this.ControlMap_MouseDown);
            this.MouseUp += new MouseEventHandler(this.ControlMap_MouseUp);
            this.MouseMove += new MouseEventHandler(this.ControlMap_MouseMove);
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Updates the control and renders the map in the background
        /// </summary>
        public void ControlUpdate()
        {
            if (Project.ActiveLayer == -1 || String.IsNullOrEmpty(Project.TileFile))
            {
                PaintMap.MapImage = new Bitmap(1, 1);
            }
            else
            {
                PaintMap.RenderMap();
            }

            Project.MapSelectionBox = new Selection(PaintMap.MapImage.Width, PaintMap.MapImage.Height);
            this.Width = PaintMap.MapImage.Width;
            this.Height = PaintMap.MapImage.Height;
            this.BackgroundImage = PaintMap.MapImage;
        }

        /// <summary>
        /// Control's OnPaint Event Hanlder
        /// </summary>
        /// <param name="pe">Control's OnPaint Event Args</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (Tools.BrushMode == BrushMode.Selection || Tools.BrushMode == BrushMode.Paste)
            {
                if (Project.MapSelectionBox.Box.Width > 0)
                {
                    pe.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 30, 30, 30)), Project.MapSelectionBox.Box.Left, Project.MapSelectionBox.Box.Top, Project.MapSelectionBox.Box.Width, Project.MapSelectionBox.Box.Height);
                    pe.Graphics.DrawRectangle(new Pen(Color.LimeGreen), Project.MapSelectionBox.Box.Left, Project.MapSelectionBox.Box.Top, Project.MapSelectionBox.Box.Width, Project.MapSelectionBox.Box.Height);
                }
            }
        }

        /// <summary>
        /// Control's MouseDown Event Hanlder
        /// </summary>
        /// <param name="sender">Active Control</param>
        /// <param name="e">Mouse Event Args</param>
        protected void ControlMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Tools.BrushMode == BrushMode.Selection)
            {
                Project.MapSelectionBox.ClickStart(e.X, e.Y);
            }

            Tools.Click(e.Button == MouseButtons.Left, e.Button == MouseButtons.Right, e.X / (Project.Map.TileSize + 1), e.Y / (Project.Map.TileSize + 1));
            this.Invalidate();
        }

        /// <summary>
        /// Control's MouseMove Event Hanlder
        /// </summary>
        /// <param name="sender">Active Control</param>
        /// <param name="e">Mouse Event Args</param>
        protected void ControlMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Tools.BrushMode == BrushMode.Selection)
            {
                Project.MapSelectionBox.Move(e.X, e.Y);
            }

            Tools.Drag(e.Button == MouseButtons.Left, e.Button == MouseButtons.Right, e.X / (Project.Map.TileSize + 1), e.Y / (Project.Map.TileSize + 1));
            this.Invalidate();
        }

        /// <summary>
        /// Control's MouseUp Event Hanlder
        /// </summary>
        /// <param name="sender">Active Control</param>
        /// <param name="e">Mouse Event Args</param>
        protected void ControlMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Tools.BrushMode == BrushMode.Selection)
            {
                Project.MapSelectionBox.ClickEnd(e.X, e.Y);
                this.GetSelection();
            }

            this.Invalidate();
        }

        /// <summary>
        /// Selects the tiles from the map when using the Selection Tool
        /// </summary>
        private void GetSelection()
        {
            Project.TileSelectionBox.ClickStart(0, 0);
            Project.TileSelectionBox.ClickEnd(0, 0);

            int x = 0;
            int y = 0;
            int binder = Project.Map.TileSize + 1;

            Project.Selection = new int[Project.MapSelectionBox.Box.Width / binder, Project.MapSelectionBox.Box.Height / binder];

            for (int i = 0; i < Project.Map.Layers[Project.ActiveLayer].Width; i++)
            {
                for (int j = 0; j < Project.Map.Layers[Project.ActiveLayer].Height; j++)
                {
                    if (i * binder >= Project.MapSelectionBox.Box.Left && i * binder < Project.MapSelectionBox.Box.Right)
                    {
                        if (j * binder >= Project.MapSelectionBox.Box.Top && j * binder < Project.MapSelectionBox.Box.Bottom)
                        {
                            if (y * binder == Project.MapSelectionBox.Box.Height)
                            {
                                y = 0;
                                x++;
                            }

                            Project.Selection[x, y] = Project.Map.Layers[Project.ActiveLayer].Tiles[i, j];
                            y++;
                        }
                    }
                }
            }
        }
    }
}
