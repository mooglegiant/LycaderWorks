using Lycader;
using Lycader.Graphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    static public class Helper
    {
        static public void ScreenWrap(Sprite sprite)
        {
            if (sprite.Position.X < -sprite.Texture.Width)
            {
                sprite.Position += new Vector3(LycaderEngine.Resolution.Width + sprite.Texture.Width, 0, 0);
            }
            else if (sprite.Position.X > LycaderEngine.Resolution.Width)
            {
                sprite.Position -= new Vector3(LycaderEngine.Resolution.Width + sprite.Texture.Width, 0, 0);
            }

            if (sprite.Position.Y < -sprite.Texture.Height)
            {
                sprite.Position += new Vector3(0, LycaderEngine.Resolution.Height + sprite.Texture.Height, 0);
            }
            else if (sprite.Position.Y > LycaderEngine.Resolution.Height)
            {
                sprite.Position -= new Vector3(0, LycaderEngine.Resolution.Height + sprite.Texture.Height, 0);
            }
        }
    }
}
