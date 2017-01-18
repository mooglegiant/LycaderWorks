namespace MapTest
{
    using Lycader;
    using Lycader.Graphics;

    /// <summary>
    /// Player's animation states
    /// </summary>
    public enum AnimationStates : int
    {
        /// <summary>
        /// Standing Animation
        /// </summary>
        Standing = 1,

        /// <summary>
        /// Running Animation
        /// </summary>
        Running = 2,

        /// <summary>
        /// Jumping Animation
        /// </summary>
        Jumping = 3,

        /// <summary>
        /// Stopping Animation
        /// </summary>
        Stopping = 4
    }

    /// <summary>
    /// The player sprite
    /// </summary>
    public class Player : Sprite
    {
        /// <summary>
        /// Current player state
        /// </summary>
        private AnimationStates animationState;

        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        public Player()
        {
            this.Position = new OpenTK.Vector3((float)(LycaderEngine.Screen.Width / 2) - 30, (float)(LycaderEngine.Screen.Height / 2) - 39, 1.0f);

            this.Animations.Add(1, new Animation(false));
            this.Animations.Add(2, new Animation(true));
            this.Animations.Add(3, new Animation(false));
            this.Animations.Add(4, new Animation(false));

            Animations[(int)AnimationStates.Standing].Add("mario-stand", 1);
            Animations[(int)AnimationStates.Running].Add("mario-run1", 20);
            Animations[(int)AnimationStates.Running].Add("mario-run2", 20);
            Animations[(int)AnimationStates.Running].Add("mario-run3", 20);
            Animations[(int)AnimationStates.Jumping].Add("mario-jump", 1);
            Animations[(int)AnimationStates.Stopping].Add("mario-stop", 1);

            this.animationState = AnimationStates.Standing;
            this.Update();
        }

        /// <summary>
        /// Animates the sprite on update
        /// </summary>
        public override void Update()
        {
            Texture = Animations[(int)this.animationState].GetTexture();
            Animations[(int)this.animationState].Animate();          
        }
    }
}
