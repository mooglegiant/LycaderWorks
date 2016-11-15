//-----------------------------------------------------------------------
// <copyright file="ControlToolbar.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using MogAssist.Maps;

    /// <summary>
    /// Custom ToolBar For Map Editor
    /// </summary>
    public class ControlToolBar : ToolStrip
    {
        #region Members
        /// <summary>
        /// Brush ToolButton
        /// </summary>
        private ToolStripButton brushButton = new ToolStripButton();

        /// <summary>
        /// Paint ToolButton
        /// </summary>
        private ToolStripButton paintButton = new ToolStripButton();

        /// <summary>
        /// seperator1 Toolbar Seperator
        /// </summary>
        private ToolStripSeparator seperator1 = new ToolStripSeparator();

        /// <summary>
        /// Select ToolButton
        /// </summary>
        private ToolStripButton selectButton = new ToolStripButton();

        /// <summary>
        /// seperator2 Toolbar Seperator
        /// </summary>
        private ToolStripSeparator seperator2 = new ToolStripSeparator();

        /// <summary>
        /// Properties ToolButton
        /// </summary>
        private ToolStripButton propertiesButton = new ToolStripButton();

        /// <summary>
        /// AddLayer ToolButton
        /// </summary>
        private ToolStripButton addLayerButton = new ToolStripButton();

        /// <summary>
        /// RemoveLayer ToolButton
        /// </summary>
        private ToolStripButton removeLayerButton = new ToolStripButton();

        /// <summary>
        /// SelectLayer ComboBox
        /// </summary>
        private ToolStripComboBox selectLayerComboBox = new ToolStripComboBox();

        /// <summary>
        /// seperator3 Toolbar Seperator
        /// </summary>
        private ToolStripSeparator seperator3 = new ToolStripSeparator();

        /// <summary>
        /// ShowAll ToolButton
        /// </summary>
        private ToolStripButton showAllButton = new ToolStripButton();

        /// <summary>
        /// ColorPicker ToolButton
        /// </summary>
        private ToolStripButton colorPickerButton = new ToolStripButton();
        #endregion

        /// <summary>
        /// Initializes a new instance of the ControlToolBar class
        /// </summary>
        public ControlToolBar()
        {
            // brushButton
            this.brushButton.Checked = true;
            this.brushButton.CheckState = CheckState.Checked;
            this.brushButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.brushButton.Image = global::MapEditor.Properties.Resources.pencil;
            this.brushButton.ImageTransparentColor = Color.Magenta;
            this.brushButton.Name = "brushButton";
            this.brushButton.Size = new Size(23, 22);
            this.brushButton.Text = "brushButton";
            this.brushButton.ToolTipText = "Draw A Tile";
            this.brushButton.Click += new EventHandler(this.Brush_Click);

            // paintButton
            this.paintButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.paintButton.Image = global::MapEditor.Properties.Resources.paintcan;
            this.paintButton.ImageTransparentColor = Color.Magenta;
            this.paintButton.Name = "paintButton";
            this.paintButton.Size = new Size(23, 22);
            this.paintButton.Text = "paintButton";
            this.paintButton.ToolTipText = "Paint A Group Of Tiles";
            this.paintButton.Click += new EventHandler(this.Paint_Click);

            // Separator1
            this.seperator1.Name = "toolStripSeparator1";
            this.seperator1.Size = new Size(6, 25);

            // SelectionButton
            this.selectButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.selectButton.Image = global::MapEditor.Properties.Resources.cursor;
            this.selectButton.ImageTransparentColor = Color.Magenta;
            this.selectButton.Name = "SelectionButton";
            this.selectButton.Size = new Size(23, 22);
            this.selectButton.Text = "SelectionButton";
            this.selectButton.ToolTipText = "Select / Paste A Group Of Tiles";
            this.selectButton.Click += new EventHandler(this.Select_Click);

            // Separator2
            this.seperator2.Name = "toolStripSeparator2";
            this.seperator2.Size = new Size(6, 25);

            // propertiesButton
            this.propertiesButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.propertiesButton.Image = global::MapEditor.Properties.Resources.application_edit;
            this.propertiesButton.ImageTransparentColor = Color.Magenta;
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new Size(23, 22);
            this.propertiesButton.Text = "propertiesButton";
            this.propertiesButton.ToolTipText = "Edit Properties";
            this.propertiesButton.Click += new EventHandler(this.Properties_Click);

            // addLayerButton
            this.addLayerButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.addLayerButton.Image = global::MapEditor.Properties.Resources.application_add;
            this.addLayerButton.ImageTransparentColor = Color.Magenta;
            this.addLayerButton.Name = "addLayerButton";
            this.addLayerButton.Size = new Size(23, 22);
            this.addLayerButton.Text = "addLayerButton";
            this.addLayerButton.ToolTipText = "Add A Map Layer";
            this.addLayerButton.Click += new EventHandler(this.AddLayer_Click);

            // removeLayerButton
            this.removeLayerButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.removeLayerButton.Image = global::MapEditor.Properties.Resources.application_delete;
            this.removeLayerButton.ImageTransparentColor = Color.Magenta;
            this.removeLayerButton.Name = "removeLayerButton";
            this.removeLayerButton.Size = new Size(23, 22);
            this.removeLayerButton.Text = "removeLayerButton";
            this.removeLayerButton.ToolTipText = "Remove Current Map Layer";
            this.removeLayerButton.Click += new EventHandler(this.RemoveLayer_Click);

            // SelectedLayerComboBox
            this.selectLayerComboBox.Name = "SelectLayer";
            this.selectLayerComboBox.AutoSize = false;
            this.selectLayerComboBox.Size = new Size(40, 22);
            this.selectLayerComboBox.ToolTipText = "Select Active Layer";
            this.selectLayerComboBox.SelectedIndexChanged += new EventHandler(this.SelectLayerComboBox_SelectedIndexChanged);
            this.selectLayerComboBox.Items.Add(0);
            this.selectLayerComboBox.FlatStyle = FlatStyle.Popup;
            this.selectLayerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Separator3
            this.seperator3.Name = "toolStripSeparator2";
            this.seperator3.Size = new Size(6, 25);

            // showAllButton
            this.showAllButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.showAllButton.Image = global::MapEditor.Properties.Resources.application_cascade;
            this.showAllButton.ImageTransparentColor = Color.Magenta;
            this.showAllButton.Name = "showAllButton";
            this.showAllButton.Size = new Size(23, 22);
            this.showAllButton.Text = "showAllButton";
            this.showAllButton.ToolTipText = "Show All Layers";
            this.showAllButton.Click += new EventHandler(this.ShowAll_Click);

            // colorPickerButton
            this.colorPickerButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.colorPickerButton.Image = global::MapEditor.Properties.Resources.color_swatch;
            this.colorPickerButton.ImageTransparentColor = Color.Magenta;
            this.colorPickerButton.Name = "colorPickerButton";
            this.colorPickerButton.Size = new Size(23, 22);
            this.colorPickerButton.Text = "colorPickerButton";
            this.colorPickerButton.ToolTipText = "Change Background Color";
            this.colorPickerButton.Click += new EventHandler(this.ColorPicker_Click);

            // ControlToolBar
            this.GripStyle = ToolStripGripStyle.Hidden;
            this.Items.AddRange(new ToolStripItem[]
            {
                this.brushButton,
                this.paintButton,
                this.seperator1,
                this.selectButton,
                this.seperator2,
                this.propertiesButton,
                this.addLayerButton,
                this.removeLayerButton,
                this.selectLayerComboBox,
                this.seperator3,
                this.showAllButton,
                this.colorPickerButton
            });
            this.Location = new Point(0, 24);
            this.Name = "tools";
            this.Padding = new Padding(10, 0, 1, 0);
            this.RenderMode = ToolStripRenderMode.System;
            this.Size = new Size(632, 25);
            this.TabIndex = 6;
            this.Text = "toolStrip1";
        }

        /// <summary>
        /// Updates the different brush mode buttons to whichever one is toggled
        /// </summary>
        public void UpdateBrushes()
        {
            this.brushButton.Checked = (Tools.BrushMode == BrushMode.Brush) ? true : false;
            this.paintButton.Checked = (Tools.BrushMode == BrushMode.Paint) ? true : false;
            this.selectButton.Checked = (Tools.BrushMode == BrushMode.Selection) ? true : false;
        }

        /// <summary>
        /// Updates the Layer Selector Combo Box
        /// </summary>
        public void UpdateLayerList()
        {
            for (int i = 0; i < Project.Map.Layers.Count; i++)
            {
                Project.Map.Layers[i].Order = i;
            }

            this.selectLayerComboBox.Items.Clear();
            for (int i = 0; i < Project.Map.Layers.Count; i++)
            {
                this.selectLayerComboBox.Items.Add(i);
            }

            if (Project.ActiveLayer > Project.Map.Layers.Count - 1)
            {
                Project.ActiveLayer = Project.Map.Layers.Count - 1;
            }

            this.selectLayerComboBox.SelectedIndex = Project.ActiveLayer;
        }

        /// <summary>
        /// btnBrush OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void Brush_Click(object sender, EventArgs e)
        {
            Tools.BrushMode = BrushMode.Brush;
            this.UpdateBrushes();
        }

        /// <summary>
        /// btnPaint OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void Paint_Click(object sender, EventArgs e)
        {
            Tools.BrushMode = BrushMode.Paint;
            this.UpdateBrushes();
        }

        /// <summary>
        /// btnSelect OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void Select_Click(object sender, EventArgs e)
        {
            Tools.BrushMode = BrushMode.Selection;
            this.UpdateBrushes();
        }

        /// <summary>
        /// btnProperties OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void Properties_Click(object sender, EventArgs e)
        {
            GlobalForms.MapProperties.SetValues();
            GlobalForms.MapProperties.Show();
        }

        /// <summary>
        /// btnAddLayer OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void AddLayer_Click(object sender, EventArgs e)
        {
            Project.ActiveLayer++;
            Project.Map.Layers.Insert(Project.ActiveLayer, new Layer(Project.ActiveLayer, 1, 1));
            this.UpdateLayerList();
        }

        /// <summary>
        /// btnRemoveLayer OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void RemoveLayer_Click(object sender, EventArgs e)
        {
            if (Project.Map.Layers.Count > 1)
            {
                Project.Map.Layers.RemoveAt(Project.ActiveLayer);
                Project.ActiveLayer--;
                this.UpdateLayerList();
            }
        }

        /// <summary>
        /// SelectLayer ComboBox IndexChanged Event Handler
        /// </summary>
        /// <param name="sender">ComboBox control</param>
        /// <param name="e">Index Changed Events</param>
        private void SelectLayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project.ActiveLayer = this.selectLayerComboBox.SelectedIndex;
            GlobalControls.UpdateControls();
        }

        /// <summary>
        /// btnShowAll OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void ShowAll_Click(object sender, EventArgs e)
        {
            this.showAllButton.Checked = !this.showAllButton.Checked;
            PaintMap.RenderAll = this.showAllButton.Checked;
            GlobalControls.UpdateControls();
        }

        /// <summary>
        /// btnColorPicker OnClick Event Handler
        /// </summary>
        /// <param name="sender">ToolStrip Button</param>
        /// <param name="e">OnClick Events</param>
        private void ColorPicker_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                Project.Map.Background = dlgColor.Color;
                GlobalControls.UpdateControls();
            }
        }
    }
}
