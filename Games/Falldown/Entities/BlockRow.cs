

namespace Falldown
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;

    public class BlockRow : Entity, IEntity
    {
        static private Random rand = new Random();
        static private int BlockCount = 12;
        static private int BlockHeight = 15;
        static private int BlockWidth = 40;

        private bool[] blocks = new bool[BlockCount];
        private int[] powerUps = new int[BlockCount];

        public BlockRow(Vector3 position) : base(position)
        {
            ResetBlocks();
        }

        // Bonus Types
        // 1 - Metal
        // 2 - Speed
        public void ResetBlocks()
        {
            int gaps = 0;
            do
            {
                gaps = 0;
                for (int i = 0; i < 12; i++)
                {
                    blocks[i] = false;
                    powerUps[i] = 0;

                    if (rand.Next(4) > 0)
                    {
                        blocks[i] = true;

                        if (Globals.GameMode == 2)
                        {
                            if (rand.Next(30) == 0)
                            {
                                powerUps[i] = rand.Next(2) + 1;
                            }
                        }
                    }
                    else
                    {
                        gaps++;                   
                    }
                }
            } while (gaps < 2 || gaps > 5);
        }

        public override void Update()
        { 
            this.Position += new Vector3(0, Globals.BlockSpeed, 0);

            if (this.Position.Y > Engine.Resolution.Height + 30)
            {
                Globals.AddToScore(1);
                this.IsDeleted = true;
            }      
        }

        public override void Draw(Camera camera)
        {
            for (int i = 0; i < 12; i++)
            {
                if (blocks[i] == true)
                {
                    Render.DrawTexture(camera, "floor.bmp", new Vector3(32 + (i * BlockWidth), this.Position.Y, this.Position.Z), 0, 1, this.Alpha);

                    if (powerUps[i] == 1)
                    {
                        Render.DrawTexture(camera, "metal.png", new Vector3(32 + (i * BlockWidth) + (BlockWidth / 4), this.Position.Y + BlockHeight, this.Position.Z), 0, 1, this.Alpha);
                    }
                    if (powerUps[i] == 2)
                    {
                        Render.DrawTexture(camera, "shoe.png", new Vector3(32 + (i * BlockWidth) + (BlockWidth / 4), this.Position.Y + BlockHeight, this.Position.Z), 0, 1, this.Alpha);
                    }
                }
            }
        }


        public override bool IsOnScreen(Camera camera)
        {
            return true;
        }

        public void CheckCollision(Ball ball)
        {
            if (this.Position.Y + 15 > ball.Position.Y && this.Position.Y + 15 < ball.Position.Y + 20)
            {  
                for (int j = 0; j < BlockCount; j++)
                {
                    if (32 + ((j + 1) * BlockWidth) >= ball.Center.X - (ball.Radius * .5))
                    {
                        if (32 + (j * BlockWidth) <= ball.Center.X + (ball.Radius * .5))
                        {
                            if (blocks[j] == true)
                            {
                                ball.Position = new Vector3(ball.Position.X, this.Position.Y + 12 + ball.GetTotalFallSpeed(), ball.Position.Z);

                                if (powerUps[j] > 0)
                                {
                                    if (32 + (BlockWidth * (j + 1)) - (BlockWidth / 4) >= ball.Center.X - (ball.Radius))
                                    {
                                        if (32 + (BlockWidth * j) + (BlockWidth / 4) <= ball.Center.X + (ball.Radius))
                                        {
                                            if(powerUps[j] == 1)
                                            {
                                                ball.MetalTimer += 20;
                                            }
                                            else if (powerUps[j] == 2)
                                            {
                                                ball.ShoeTimer += 20;
                                            }

                                            powerUps[j] = 0;  
                                        }
                                    }
                                }
            
                            }
                        }
                    }
                }
            }
        }
    }
}