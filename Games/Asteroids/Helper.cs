using Lycader;
using Lycader.Entities;
using OpenTK;

namespace Asteroids
{
    static public class Helper
    {
        static public void ScreenWrap(SpriteEntity sprite)
        {
            if (sprite.Position.X < -sprite.GetTextureInfo().Width)
            {
                sprite.Position += new Vector3(LycaderEngine.Resolution.Width + sprite.GetTextureInfo().Width, 0, 0);
            }
            else if (sprite.Position.X > LycaderEngine.Resolution.Width)
            {
                sprite.Position -= new Vector3(LycaderEngine.Resolution.Width + sprite.GetTextureInfo().Width, 0, 0);
            }

            if (sprite.Position.Y < -sprite.GetTextureInfo().Height)
            {
                sprite.Position += new Vector3(0, LycaderEngine.Resolution.Height + sprite.GetTextureInfo().Height, 0);
            }
            else if (sprite.Position.Y > LycaderEngine.Resolution.Height)
            {
                sprite.Position -= new Vector3(0, LycaderEngine.Resolution.Height + sprite.GetTextureInfo().Height, 0);
            }
        }
    }
}
