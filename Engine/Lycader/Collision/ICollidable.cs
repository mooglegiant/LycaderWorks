using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Collision
{
    using OpenTK;

    public interface ICollidable
    {
        string Name { get; set; }

        Vector2 Position { get; set; }
    }
}
