namespace Lycader.Entities
{
    using Collision;
    using OpenTK;

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
