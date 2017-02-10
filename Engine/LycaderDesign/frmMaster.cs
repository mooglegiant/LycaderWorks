namespace LycaderDesign
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    public partial class frmMaster : Form
    {
        public frmMaster()
        {
            InitializeComponent();
            treeMaster.NodeMouseClick += TreeMaster_NodeMouseClick;
        }

        private void TreeMaster_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeMaster.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                this.dynamicContext.Items.Clear();

                switch (this.treeMaster.SelectedNode.Tag.ToString())
                {
                    case "Assets":
                        {
                            this.dynamicContext.Items.Add("Add Animation", null, new System.EventHandler(this.dynamicContext_AddAnimation));
                            this.dynamicContext.Items.Add("Add Audio File", null, new System.EventHandler(this.dynamicContext_AddAudio));
                            this.dynamicContext.Items.Add("Add Image File", null, new System.EventHandler(this.dynamicContext_AddImage));
                            this.dynamicContext.Items.Add("Add TileMap", null, new System.EventHandler(this.dynamicContext_AddAnimation));
                            break;
                        }
                    case "Audio":
                        {
                            this.dynamicContext.Items.Add("Add Audio File", null, new System.EventHandler(this.dynamicContext_AddAudio));
                            break;
                        }
                    case "Animations":
                        {
                            this.dynamicContext.Items.Add("Add Animation", null, new System.EventHandler(this.dynamicContext_AddAnimation));
                            break;
                        }
                    case "Images":
                        {
                            this.dynamicContext.Items.Add("Add Image File", null, new System.EventHandler(this.dynamicContext_AddImage));
                            break;
                        }
                    case "TileMaps":
                        {
                            this.dynamicContext.Items.Add("Add TileMap", null, new System.EventHandler(this.dynamicContext_AddTileMap));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                this.dynamicContext.Show();

            }
        }

        private void dynamicContext_AddAnimation(object sender, EventArgs e)
        {
            AddNewTab(new frmAnimation());
            var node = this.treeMaster.FlattenTree()
                     .Where(n => n.Text == "Animations")
                     .First();

            node.Nodes.Add("Animation 0");
        }

        private void dynamicContext_AddAudio(object sender, EventArgs e)
        {
            AddNewTab(new frmAnimation());
            var node = this.treeMaster.FlattenTree()
                     .Where(n => n.Text == "Animations")
                     .First();

            node.Nodes.Add("Animation 0");
        }

        private void dynamicContext_AddImage(object sender, EventArgs e)
        {
            AddNewTab(new frmAnimation());
            var node = this.treeMaster.FlattenTree()
                     .Where(n => n.Text == "TileMaps")
                     .First();

            node.Nodes.Add("TileMap 0");
        }

        private void dynamicContext_AddTileMap(object sender, EventArgs e)
        {
            AddNewTab(new frmAnimation());
            var node = this.treeMaster.FlattenTree()
                     .Where(n => n.Text == "TileMaps")
                     .First();

            node.Nodes.Add("TileMap 0");
        }

        private void AddNewTab(Form frm)
        {

            TabPage tab = new TabPage(frm.Text);

            frm.TopLevel = false;
            frm.Parent = tab;
            frm.Visible = true;

            tabControlMaster.TabPages.Add(tab);
            tabControlMaster.SelectedTab = tab;

        }
    }
}
