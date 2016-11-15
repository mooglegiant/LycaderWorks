namespace LycaderDesign
{
    partial class frmMaster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Global");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Animations");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Audio");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Images");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("TileSets");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Assets", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.dynamicContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeMaster = new System.Windows.Forms.TreeView();
            this.tabControlMaster = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dynamicContext
            // 
            this.dynamicContext.Name = "dynamicContext";
            this.dynamicContext.Size = new System.Drawing.Size(61, 4);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1033, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeMaster);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlMaster);
            this.splitContainer1.Size = new System.Drawing.Size(1033, 480);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeMaster
            // 
            this.treeMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMaster.Indent = 15;
            this.treeMaster.Location = new System.Drawing.Point(0, 0);
            this.treeMaster.Name = "treeMaster";
            treeNode1.Name = "NodeGlobal";
            treeNode1.Text = "Global";
            treeNode2.ContextMenuStrip = this.dynamicContext;
            treeNode2.Name = "NodeAnimations";
            treeNode2.Tag = "Animations";
            treeNode2.Text = "Animations";
            treeNode3.Name = "NodeAudio";
            treeNode3.Tag = "Audio";
            treeNode3.Text = "Audio";
            treeNode4.Name = "NodeImages";
            treeNode4.Tag = "Images";
            treeNode4.Text = "Images";
            treeNode5.ContextMenuStrip = this.dynamicContext;
            treeNode5.Name = "NodeTilesets";
            treeNode5.Tag = "TileSets";
            treeNode5.Text = "TileSets";
            treeNode6.ContextMenuStrip = this.dynamicContext;
            treeNode6.Name = "NodeAssets";
            treeNode6.Tag = "Assets";
            treeNode6.Text = "Assets";
            this.treeMaster.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6});
            this.treeMaster.Size = new System.Drawing.Size(199, 480);
            this.treeMaster.TabIndex = 0;
            // 
            // tabControlMaster
            // 
            this.tabControlMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMaster.Location = new System.Drawing.Point(0, 0);
            this.tabControlMaster.Name = "tabControlMaster";
            this.tabControlMaster.SelectedIndex = 0;
            this.tabControlMaster.Size = new System.Drawing.Size(830, 480);
            this.tabControlMaster.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1033, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 526);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMaster";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeMaster;
        private System.Windows.Forms.TabControl tabControlMaster;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip dynamicContext;
    }
}

