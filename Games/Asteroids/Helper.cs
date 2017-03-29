//-----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Asteroids
{
    using Lycader;
    using Lycader.Entities;
    using OpenTK;

    public static class Helper
    {
        public static void ScreenWrap(SpriteEntity sprite)
        {
            if (sprite.Position.X < -sprite.GetTextureInfo().Width)
            {
                sprite.Position += new Vector3(Engine.Resolution.Width + sprite.GetTextureInfo().Width, 0, 0);
            }
            else if (sprite.Position.X > Engine.Resolution.Width)
            {
                sprite.Position -= new Vector3(Engine.Resolution.Width + sprite.GetTextureInfo().Width, 0, 0);
            }

            if (sprite.Position.Y < -sprite.GetTextureInfo().Height)
            {
                sprite.Position += new Vector3(0, Engine.Resolution.Height + sprite.GetTextureInfo().Height, 0);
            }
            else if (sprite.Position.Y > Engine.Resolution.Height)
            {
                sprite.Position -= new Vector3(0, Engine.Resolution.Height + sprite.GetTextureInfo().Height, 0);
            }
        }
    }
}
