//-----------------------------------------------------------------------
// <copyright file="ICollidable.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Collision
{
    using OpenTK;

    public interface ICollidable
    {
        string Name { get; set; }

        Vector2 Position { get; set; }
    }
}
