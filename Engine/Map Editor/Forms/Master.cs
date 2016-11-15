//-----------------------------------------------------------------------
// <copyright file="Master.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor.Forms
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using MapEditor;

    /// <summary>
    /// Map Editor's Master Form
    /// </summary>
    public class Master : Form
    {
        #region Controls
        /// <summary>
        /// Form's control container (used for visual designer)
        /// </summary>
        private IContainer components = null;
        #endregion

        /// <summary>
        /// Initializes a new instance of the Master class
        /// </summary>
        public Master()
        {
            this.InitializeComponent();
            Project.IsSaved = true;
            GlobalControls.UpdateControls();
        }
        
        /// <summary>
        /// Puts the map name in the form's title and marks if the project is saved or not
        /// </summary>
        /// <param name="isSaved">A value indicating whether the current project is saved or not</param>
        public void SetNames(bool isSaved)
        {
            this.Text = "Map Editor - " + Project.Map.Name;
            Project.IsSaved = isSaved;

            if (!isSaved)
            {
                this.Text += " (*)";
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Form's OnClosed Event Handler
        /// </summary>
        /// <param name="e">OnClosed Event Args</param>
        protected override void OnClosed(EventArgs e)
        {
            if (Project.IsSaved)
            {
                base.OnClosed(e);
            }
            else
            {
                // TODO - Prompt for saving OnClose
            }
        }

        /// <summary>
        /// Initializes all the controls on the form
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Master
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(GlobalControls.Project);
            this.Controls.Add(GlobalControls.ToolBar);
            this.Controls.Add(GlobalControls.Menu);
            this.MainMenuStrip = GlobalControls.Menu;
            this.Name = "Master";
            this.Text = "Map Editor - Untitled";
            this.ResumeLayout(false);
        }
    }
}
