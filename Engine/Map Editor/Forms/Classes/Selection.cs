//-----------------------------------------------------------------------
// <copyright file="Selection.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Class for our tile selection box
    /// </summary>
    public class Selection
    {
        /// <summary>
        /// The current control's width
        /// </summary>
        private int controlWidth;

        /// <summary>
        /// The current control's height
        /// </summary>
        private int controlHeight;

        /// <summary>
        /// The box's starting point
        /// </summary>
        private Point startingPoint;

        /// <summary>
        /// The box's current/ending point
        /// </summary>
        private Point endingPoint;

        /// <summary>
        /// Initializes a new instance of the Selection class
        /// </summary>
        /// <param name="controlWidth">The control's width edge</param>
        /// <param name="controlHeight">The control's height edge</param>
        public Selection(int controlWidth, int controlHeight)
        {
            this.Box = new Rectangle();
            this.controlWidth = controlWidth;
            this.controlHeight = controlHeight;
        }

        /// <summary>
        /// Gets the selection box
        /// </summary>
        public Rectangle Box { get; private set; }

        /// <summary>
        /// Sets the selction box's starting point
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public void ClickStart(int x, int y)
        {
            this.Box = new Rectangle();
            this.startingPoint = new Point(x, y);
            this.endingPoint = new Point(x, y);
        }

        /// <summary>
        /// Sets the selction box's current ending point
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public void Move(int x, int y)
        {
            this.endingPoint = new Point(x, y);
            this.FixSize();
        }

        /// <summary>
        /// Sets the selction box's final ending point
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public void ClickEnd(int x, int y)
        {
            this.endingPoint = new Point(x, y);
            this.FixSize();
        }

        /// <summary>
        /// Adjusts the size of the box to fixed lengths on the tile grid
        /// </summary>
        private void FixSize()
        {
            int left = 0, top = 0, right = 0, bottom = 0;

            if (Tools.BrushMode == BrushMode.Selection)
            {
                left = (this.startingPoint.X < this.endingPoint.X) ? this.startingPoint.X : this.endingPoint.X;
                top = (this.startingPoint.Y < this.endingPoint.Y) ? this.startingPoint.Y : this.endingPoint.Y;
                right = (this.startingPoint.X > this.endingPoint.X) ? this.startingPoint.X : this.endingPoint.X;
                bottom = (this.startingPoint.Y > this.endingPoint.Y) ? this.startingPoint.Y : this.endingPoint.Y;
            }
            else if (Tools.BrushMode == BrushMode.Brush || Tools.BrushMode == BrushMode.Paint)
            {
                left = this.endingPoint.X;
                top = this.endingPoint.Y;
                right = left;
                bottom = top;
            }

            while (top % (Project.Map.TileSize + 1) != 0 && top > 0)
            {
                top--;
            }

            while (left % (Project.Map.TileSize + 1) != 0 && left > 0)
            {
                left--;
            }

            while (right % (Project.Map.TileSize + 1) != 0)
            {
                right++;
            }

            while (bottom % (Project.Map.TileSize + 1) != 0)
            {
                bottom++;
            }

            // Make sure we are in bounds
            if (top < 0)
            {
                top = 0;
            }

            if (left < 0)
            {
                left = 0;
            }

            if (right > this.controlWidth - 1)
            {
                right = this.controlWidth - 1;
            }

            if (bottom > this.controlHeight - 1)
            {
                bottom = this.controlHeight - 1;
            }

            this.Box = new Rectangle(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
        }
    }
}
