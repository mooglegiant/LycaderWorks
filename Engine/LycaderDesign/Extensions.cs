namespace LycaderDesign
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public static class Extensions
    {
            public static IEnumerable<TreeNode> FlattenTree(this TreeView tv)
            {
                return FlattenTree(tv.Nodes);
            }

            public static IEnumerable<TreeNode> FlattenTree(this TreeNodeCollection coll)
            {
                return coll.Cast<TreeNode>()
                            .Concat(coll.Cast<TreeNode>()
                                        .SelectMany(x => FlattenTree(x.Nodes)));
            }        
    }
}
