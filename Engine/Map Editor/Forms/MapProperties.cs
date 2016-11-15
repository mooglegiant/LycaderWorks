//-----------------------------------------------------------------------
// <copyright file="MapProperties.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor.Forms
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Map Properties Edit Form
    /// </summary>
    public class MapProperties : Form
    {
        #region Controls
        /// <summary>
        /// Form's control container (used for visual designer)
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Map Properties Group Box
        /// </summary>
        private GroupBox gbxMapProperties;

        /// <summary>
        /// Tile size label
        /// </summary>
        private Label lblTileSize;

        /// <summary>
        /// Selects the tile size
        /// </summary>
        private ComboBox tileSizeComboBox;

        /// <summary>
        /// Layer properties group box
        /// </summary>
        private GroupBox gbxLayerProperties;

        /// <summary>
        /// Layer height label
        /// </summary>
        private Label lblHeight;

        /// <summary>
        /// Edits layer's Height property
        /// </summary>
        private NumericUpDown txtHeight;

        /// <summary>
        /// Layer width label
        /// </summary>
        private Label lblWidth;

        /// <summary>
        /// Edits layer's Width property
        /// </summary>
        private NumericUpDown txtWidth;

        /// <summary>
        /// ScrollSpeedX label
        /// </summary>
        private Label lblSpeedX;

        /// <summary>
        /// Edits layer's ScrollSpeedX property
        /// </summary>
        private NumericUpDown txtScrollSpeedX;

        /// <summary>
        /// ScrollSpeedY label
        /// </summary>
        private Label lblSpeedY;

        /// <summary>
        /// Edits layer's ScrollSpeedY property
        /// </summary>
        private NumericUpDown txtScrollSpeedY;

        /// <summary>
        /// Edits layer's RepeatX property
        /// </summary>
        private CheckBox chkRepeatX;

        /// <summary>
        /// Edits layer's RepeatY property
        /// </summary>
        private CheckBox chkRepeatY;

        /// <summary>
        /// Button for saving properties
        /// </summary>
        private Button btnSave;
        #endregion

        /// <summary>
        /// Initializes a new instance of the MapProperties class
        /// </summary>
        public MapProperties()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets values for editing
        /// </summary>
        public void SetValues()
        {
            this.tileSizeComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            this.tileSizeComboBox.Text = Project.Map.TileSize.ToString();
            this.tileSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            this.txtWidth.Value = (decimal)Project.Map.Layers[Project.ActiveLayer].Width;
            this.txtHeight.Value = (decimal)Project.Map.Layers[Project.ActiveLayer].Height;
            this.txtScrollSpeedX.Value = (decimal)Project.Map.Layers[Project.ActiveLayer].ScrollSpeedX;
            this.txtScrollSpeedY.Value = (decimal)Project.Map.Layers[Project.ActiveLayer].ScrollSpeedY;

            this.chkRepeatX.Checked = Project.Map.Layers[Project.ActiveLayer].RepeatX;
            this.chkRepeatY.Checked = Project.Map.Layers[Project.ActiveLayer].RepeatY;
        }

        /// <summary>
        /// btnSave Event Handler
        /// </summary>
        /// <param name="sender">Save Properties Button</param>
        /// <param name="e">OnClick Event Args</param>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Project.Map.TileSize = int.Parse(this.tileSizeComboBox.SelectedItem.ToString());
            Project.Map.Layers[Project.ActiveLayer].Resize((int)this.txtWidth.Value, (int)this.txtHeight.Value);
            Project.Map.Layers[Project.ActiveLayer].ScrollSpeedX = (float)this.txtScrollSpeedX.Value;
            Project.Map.Layers[Project.ActiveLayer].ScrollSpeedY = (float)this.txtScrollSpeedY.Value;
            Project.Map.Layers[Project.ActiveLayer].RepeatX = this.chkRepeatX.Checked;
            Project.Map.Layers[Project.ActiveLayer].RepeatY = this.chkRepeatY.Checked;
            GlobalControls.UpdateControls();
            this.Hide();
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
        /// Initializes the form's controls
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxMapProperties = new System.Windows.Forms.GroupBox();
            this.tileSizeComboBox = new System.Windows.Forms.ComboBox();
            this.lblTileSize = new System.Windows.Forms.Label();
            this.gbxLayerProperties = new System.Windows.Forms.GroupBox();
            this.chkRepeatY = new System.Windows.Forms.CheckBox();
            this.chkRepeatX = new System.Windows.Forms.CheckBox();
            this.txtScrollSpeedY = new System.Windows.Forms.NumericUpDown();
            this.lblSpeedY = new System.Windows.Forms.Label();
            this.txtScrollSpeedX = new System.Windows.Forms.NumericUpDown();
            this.lblSpeedX = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.NumericUpDown();
            this.txtWidth = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbxMapProperties.SuspendLayout();
            this.gbxLayerProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.txtScrollSpeedY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.txtScrollSpeedX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.txtHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.txtWidth).BeginInit();
            this.SuspendLayout();

            // gbxMapProperties
            this.gbxMapProperties.Controls.Add(this.tileSizeComboBox);
            this.gbxMapProperties.Controls.Add(this.lblTileSize);
            this.gbxMapProperties.Location = new System.Drawing.Point(12, 12);
            this.gbxMapProperties.Name = "gbxMapProperties";
            this.gbxMapProperties.Size = new System.Drawing.Size(218, 48);
            this.gbxMapProperties.TabIndex = 0;
            this.gbxMapProperties.TabStop = false;
            this.gbxMapProperties.Text = "Map Properties";

            // tileSizeComboBox
            this.tileSizeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tileSizeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tileSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tileSizeComboBox.FormattingEnabled = true;
            this.tileSizeComboBox.Items.AddRange(new object[] 
            {
                "8",
                "16",
                "32",
                "64",
                "128"
            });

            this.tileSizeComboBox.Location = new System.Drawing.Point(81, 19);
            this.tileSizeComboBox.Name = "tileSizeComboBox";
            this.tileSizeComboBox.Size = new System.Drawing.Size(121, 21);
            this.tileSizeComboBox.TabIndex = 1;

            // lblTileSize
            this.lblTileSize.AutoSize = true;
            this.lblTileSize.Location = new System.Drawing.Point(10, 19);
            this.lblTileSize.Name = "lblTileSize";
            this.lblTileSize.Size = new System.Drawing.Size(50, 13);
            this.lblTileSize.TabIndex = 0;
            this.lblTileSize.Text = "Tile Size:";

            // gbxLayerProperties
            this.gbxLayerProperties.Controls.Add(this.chkRepeatY);
            this.gbxLayerProperties.Controls.Add(this.chkRepeatX);
            this.gbxLayerProperties.Controls.Add(this.txtScrollSpeedY);
            this.gbxLayerProperties.Controls.Add(this.lblSpeedY);
            this.gbxLayerProperties.Controls.Add(this.txtScrollSpeedX);
            this.gbxLayerProperties.Controls.Add(this.lblSpeedX);
            this.gbxLayerProperties.Controls.Add(this.lblHeight);
            this.gbxLayerProperties.Controls.Add(this.lblWidth);
            this.gbxLayerProperties.Controls.Add(this.txtHeight);
            this.gbxLayerProperties.Controls.Add(this.txtWidth);
            this.gbxLayerProperties.Location = new System.Drawing.Point(12, 66);
            this.gbxLayerProperties.Name = "gbxLayerProperties";
            this.gbxLayerProperties.Size = new System.Drawing.Size(218, 173);
            this.gbxLayerProperties.TabIndex = 1;
            this.gbxLayerProperties.TabStop = false;
            this.gbxLayerProperties.Text = "Layer Properties";

            // chkRepeatY
            this.chkRepeatY.AutoSize = true;
            this.chkRepeatY.Location = new System.Drawing.Point(81, 148);
            this.chkRepeatY.Name = "chkRepeatY";
            this.chkRepeatY.Size = new System.Drawing.Size(114, 17);
            this.chkRepeatY.TabIndex = 9;
            this.chkRepeatY.Text = "Repeat Y Scrolling";
            this.chkRepeatY.UseVisualStyleBackColor = true;

            // chkRepeatX
            this.chkRepeatX.AutoSize = true;
            this.chkRepeatX.Location = new System.Drawing.Point(81, 125);
            this.chkRepeatX.Name = "chkRepeatX";
            this.chkRepeatX.Size = new System.Drawing.Size(114, 17);
            this.chkRepeatX.TabIndex = 8;
            this.chkRepeatX.Text = "Repeat X Scrolling";
            this.chkRepeatX.UseVisualStyleBackColor = true;

            // txtScrollSpeedY
            this.txtScrollSpeedY.DecimalPlaces = 2;
            this.txtScrollSpeedY.Increment = new decimal(new int[]
            {
                1,
                0,
                0,
                131072
            });

            this.txtScrollSpeedY.Location = new System.Drawing.Point(81, 99);
            this.txtScrollSpeedY.Name = "txtScrollSpeedY";
            this.txtScrollSpeedY.Size = new System.Drawing.Size(120, 20);
            this.txtScrollSpeedY.TabIndex = 7;
            this.txtScrollSpeedY.Value = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });
            
            // lblSpeedY             
            this.lblSpeedY.AutoSize = true;
            this.lblSpeedY.Location = new System.Drawing.Point(10, 99);
            this.lblSpeedY.Name = "lblSpeedY";
            this.lblSpeedY.Size = new System.Drawing.Size(51, 13);
            this.lblSpeedY.TabIndex = 6;
            this.lblSpeedY.Text = "Y Speed:";
            
            // txtScrollSpeedX             
            this.txtScrollSpeedX.DecimalPlaces = 2;
            this.txtScrollSpeedX.Increment = new decimal(new int[]
            {
                1,
                0,
                0,
                131072
            });

            this.txtScrollSpeedX.Location = new System.Drawing.Point(81, 73);
            this.txtScrollSpeedX.Name = "txtScrollSpeedX";
            this.txtScrollSpeedX.Size = new System.Drawing.Size(120, 20);
            this.txtScrollSpeedX.TabIndex = 5;
            this.txtScrollSpeedX.Value = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });
             
            // lblSpeedX             
            this.lblSpeedX.AutoSize = true;
            this.lblSpeedX.Location = new System.Drawing.Point(10, 73);
            this.lblSpeedX.Name = "lblSpeedX";
            this.lblSpeedX.Size = new System.Drawing.Size(51, 13);
            this.lblSpeedX.TabIndex = 4;
            this.lblSpeedX.Text = "X Speed:";
            
            // lblHeight             
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(10, 47);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(41, 13);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "Height:";
            
            // lblWidth             
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(10, 21);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(38, 13);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "Width:";
            
            // txtHeight             
            this.txtHeight.Location = new System.Drawing.Point(81, 47);
            this.txtHeight.Maximum = new decimal(new int[] 
            {
                10000,
                0,
                0,
                0
            });
            
            this.txtHeight.Minimum = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });

            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(120, 20);
            this.txtHeight.TabIndex = 1;
            this.txtHeight.Value = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });
             
            // txtWidth             
            this.txtWidth.Location = new System.Drawing.Point(81, 21);
            this.txtWidth.Maximum = new decimal(new int[]
            {
                10000,
                0,
                0,
                0
            });

            this.txtWidth.Minimum = new decimal(new int[] 
            {
                1,
                0,
                0,
                0
            });
            
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(120, 20);
            this.txtWidth.TabIndex = 0;
            this.txtWidth.Value = new decimal(new int[]
            {
                1,
                0,
                0,
                0
            });
             
            // btnSave             
            this.btnSave.Location = new System.Drawing.Point(138, 245);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // MapProperties
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 277);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbxLayerProperties);
            this.Controls.Add(this.gbxMapProperties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Properties";
            this.gbxMapProperties.ResumeLayout(false);
            this.gbxMapProperties.PerformLayout();
            this.gbxLayerProperties.ResumeLayout(false);
            this.gbxLayerProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.txtScrollSpeedY).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.txtScrollSpeedX).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.txtHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.txtWidth).EndInit();
            this.ResumeLayout(false);
        }
    }
}
