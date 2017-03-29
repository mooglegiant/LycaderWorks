//-----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Entities
{
    using Lycader.Collision;
    using OpenTK;
    using Lycader.Graphics;

    public interface IEntity
    {
        Vector3 Position { get; set; }

        Vector3 Center { get; }

        float Zoom { get; set; }

        int Rotation { get; set; }

        bool IsDeleted { get; set; }

        ICollidable CollisionShape { get; set; }

        void Draw(Camera camera);

        void Update();

        bool IsOnScreen(Camera camera);
    }
}
