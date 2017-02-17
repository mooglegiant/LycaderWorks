using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;

using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;

using SDLGE;

namespace Game.Objects
{
    static public class BlockSettings
    {
        static readonly public int blockwidth = 40;
        static readonly public int blockheight = 15;
        static readonly public int blockcount = 12;
        static readonly public int layercount = 6;
    }

    static class Blocks
    {
        static BlockRow[] Level;
        static Box layerblock;
        static Box powerup;
        static private int LayerCounter;
        static Surface floor;

        public static void Init()
        {
            Level = new BlockRow[BlockSettings.layercount];
            Level[0] = new BlockRow(500);
            Thread.Sleep(100);
            Level[1] = new BlockRow(600);
            Thread.Sleep(100);
            Level[2] = new BlockRow(700);
            Thread.Sleep(100);
            Level[3] = new BlockRow(800);
            Thread.Sleep(100);
            Level[4] = new BlockRow(900);
            Thread.Sleep(100);
            Level[5] = new BlockRow(1000);
            Thread.Sleep(100);
            layerblock = new Box(0, 0, (short)BlockSettings.blockwidth, (short)BlockSettings.blockheight);
            LayerCounter = 0;
            powerup = new Box(0, 0, 19, 20);

            Engine.Surfaces.AddSurface("Data\\Images\\floor.bmp", "floor", false);
            floor = Engine.Surfaces["floor"];
           
        }

        public static void Draw()
        {
            for (int i = 0; i < BlockSettings.layercount; i++)
            {
                for (int j = 0; j < BlockSettings.blockcount; j++)
                {
                    if (Level[i].block[j] == true)
                    {
                        layerblock.Location = new Point(32 + (BlockSettings.blockwidth * j), (int)Level[i].Height);
                        //layerblock.Draw(Globals.Layer2.Screen, Globals.LevelColor, false, true);
                        Globals.Layer2.Blit(floor, layerblock.Location);
                        if (Level[i].PowerUps[j].Exists)
                        {
                            powerup.Location = new Point(32 + (BlockSettings.blockwidth * j) + (BlockSettings.blockwidth / 4), (int)Level[i].Height - powerup.Height);
                            //powerup.Draw(Globals.Layer2.Screen, Color.Transparent, false, false);
                            Globals.Layer2.Blit(Engine.Surfaces["Powerup" + Level[i].PowerUps[j].Type.ToString().Trim()], powerup.Location);
                        }
                    }
                }
            }
        }

        public static void LevelUP()
        {
            floor.ReplaceColor(floor.GetPixel(new Point(1, 1)), Globals.LevelColor);
        }

        public static void Update()
        {
            for (int i = 0; i < 6; i++)
            {
                Level[i].Height -= Globals.BlockSpeed;

                if (Level[i].Height < -30)
                {
                    Level[i] = new BlockRow(570);
                    Globals.AddToScore(1);
                    LayerCounter++;

                    if (LayerCounter == 20)
                    {
                        Globals.LevelUp();
                        LevelUP();
                        LayerCounter = 0;
                    }
                }
            }
        }

        public static int CollisionCheck(Circle Ball)
        {
            int NewPosition;
            NewPosition = Ball.PositionY;

            for (int i = 0; i < BlockSettings.layercount; i++)
            {
                if (Level[i].Height < Ball.PositionY + (Ball.Radius) && Level[i].Height > Ball.PositionY - (Ball.Radius))
                {
                    for (int j = 0; j < BlockSettings.blockcount; j++)
                    {
                        if (32 + (BlockSettings.blockwidth * (j + 1)) >= Ball.Center.X - (Ball.Radius))
                        {
                            if (32 + (BlockSettings.blockwidth * j) <= Ball.Center.X + (Ball.Radius))
                            {
                                if (Level[i].block[j] == true)
                                {
                                    NewPosition = (int)Level[i].Height - Ball.Radius;
                                    if (Level[i].PowerUps[j].Exists)
                                    {
                                        if (32 + (BlockSettings.blockwidth * (j + 1)) - (BlockSettings.blockwidth / 4) >= Ball.Center.X - (Ball.Radius))
                                        {
                                            if (32 + (BlockSettings.blockwidth * j) + (BlockSettings.blockwidth / 4) <= Ball.Center.X + (Ball.Radius))
                                            {
                                                Level[i].PowerUps[j].Exists = false;
                                                Modifiers.Process(Level[i].PowerUps[j].Type);                                               
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return NewPosition;
        }
    }

    public class BlockRow
    {
        public bool[] block = new bool[BlockSettings.blockcount];
        public double Height;
        public PowerUps[] PowerUps = new PowerUps[BlockSettings.blockcount];
        public bool HasPowerUp;

        public BlockRow(int newheight)
        {
            Height = newheight;
            bool allok = false;

            Random rand = new Random();

            while (allok == false)
            {
                int count = 0;
                for (int j = 0; j < BlockSettings.blockcount; j++)
                {
                    if (rand.Next(4) > 0)
                    {
                        block[j] = true;
                        count++;

                        if (rand.Next(30) == 0)
                        {
                            PowerUps[j] = new PowerUps(true);
                        }
                        else
                        {
                            PowerUps[j] = new PowerUps(false);
                        }
                    }
                    else
                    {
                        block[j] = false;
                    }                    
                }

                if (count < BlockSettings.blockcount - 2 && count > BlockSettings.blockcount - 5)
                {
                    allok = true;
                }
            }
        }
    }

    public class PowerUps
    {
        public bool Exists;
        public int Type;

        public PowerUps(bool create)
        {
            if (create == true && Globals.GameMode == 2)
            {
                Random rand = new Random();
                Exists = true;
                Type = rand.Next(3) + 1;
            }
            else
            {
                Exists = false;
            }
        }
    }
}
