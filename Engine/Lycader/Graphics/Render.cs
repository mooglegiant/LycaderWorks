﻿//-----------------------------------------------------------------------
// <copyright file="Render.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Graphics
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;

    using Lycader.Math;

    public static class Render
    {
        public static void DrawCircle(Camera camera, Vector3 position, Color4 color, float lineWidth, float radius, DrawType drawType)
        {
            position = camera.GetScreenPosition(position);

            int maxPoints = 0;
            GL.Disable(EnableCap.Texture2D);
            GL.PushMatrix();
            {
                GL.Color4(color);
                GL.LineWidth(lineWidth);

                camera.SetViewport();
                camera.SetOrtho();

                if (drawType == DrawType.Outline)
                {
                    GL.Begin(PrimitiveType.LineLoop);
                    {
                        maxPoints = 100;
                        for (int i = 0; i <= maxPoints; i++)
                        {
                            GL.Vertex3(position.X + (radius * System.Math.Cos(i * Calculate.TwoPi / maxPoints)), position.Y + (radius * System.Math.Sin(i * Calculate.TwoPi / maxPoints)), position.Z);
                        }
                    }

                    GL.End();
                }
                else if (drawType == DrawType.Solid)
                {
                    GL.Begin(PrimitiveType.TriangleFan);
                    {
                        maxPoints = 50;
                        GL.Vertex3(position.X, position.Y, position.Z); //Center of circle
                        for (int i = 0; i <= maxPoints; i++)
                        {
                            GL.Vertex3(position.X + (radius * System.Math.Cos(i * Calculate.TwoPi / maxPoints)), position.Y + (radius * System.Math.Sin(i * Calculate.TwoPi / maxPoints)), position.Z);
                        }
                    }

                    GL.End();
                }

                GL.Color4(Color4.White);
            }

            GL.PopMatrix();
            GL.Enable(EnableCap.Texture2D);
        }

        public static void DrawLine(Camera camera, Vector3 startingPoint, Vector3 endingPoint, Color4 color, float lineWidth)
        {
            GL.Disable(EnableCap.Texture2D);

            startingPoint = camera.GetScreenPosition(startingPoint);
            endingPoint = camera.GetScreenPosition(endingPoint);

            GL.PushMatrix();
            {
                GL.Color4(color);
                GL.LineWidth(lineWidth);

                camera.SetViewport();
                camera.SetOrtho();

                GL.Begin(PrimitiveType.Lines);
                {
                    GL.Vertex3(startingPoint.X, startingPoint.Y, startingPoint.Z);
                    GL.Vertex3(endingPoint.X, endingPoint.Y, endingPoint.Z);
                }

                GL.End();

                GL.Color4(Color4.White);
            }

            GL.PopMatrix();
            GL.Enable(EnableCap.Texture2D);
        }

        public static void DrawQuad(Camera camera, Vector3 position, float width, float height, Color4 color, float lineWidth, DrawType drawType)
        {
            position = camera.GetScreenPosition(position);

            GL.Disable(EnableCap.Texture2D);

            GL.PushMatrix();
            {
                GL.Color4(color);
                GL.LineWidth(lineWidth);

                camera.SetViewport();
                camera.SetOrtho();

                if (drawType == DrawType.Outline)
                {
                    GL.Begin(PrimitiveType.LineLoop);
                }
                else if (drawType == DrawType.Solid)
                {
                    GL.Begin(PrimitiveType.Quads);
                }

                GL.Vertex3(position.X, position.Y, position.Z);
                GL.Vertex3(position.X + width, position.Y, position.Z);
                GL.Vertex3(position.X + width, position.Y + height, position.Z);
                GL.Vertex3(position.X, position.Y + height, position.Z);

                GL.End();
                GL.Color4(Color4.White);
            }

            GL.PopMatrix();
            GL.Enable(EnableCap.Texture2D);
        }

        public static void DrawText(Camera camera, string texture, Vector3 position, Color4 color, double fontSize, int rotation, float spacing, string displayText)
        {
            if (string.IsNullOrEmpty(displayText))
            {
                return;
            }

            position = camera.GetScreenPosition(position);

            TextureManager.Find(texture).Bind();

            GL.PushMatrix();
            {
                GL.Color4(color);

                camera.SetViewport();
                camera.SetOrtho();

                GL.Translate(position.X, position.Y, 0);
                GL.Scale(fontSize, fontSize, 1f);
                GL.Rotate(rotation, 0, 0, 1);

                GL.Begin(PrimitiveType.Quads);
                {
                    double offsetX = 0;

                    foreach (var ch in displayText)
                    {
                        byte ascii;

                        unchecked
                        {
                            ascii = (byte)ch;
                        }

                        int row = ascii >> 4;
                        int col = ascii & 0x0F;

                        double left = col * .0625;
                        double top = row * .0625;
                        double right = left + .0625;
                        double bottom = top + .0625;

                        float z = position.Z;

                        GL.TexCoord2(left, top);
                        GL.Vertex3(offsetX, 1, z);

                        GL.TexCoord2(right, top);
                        GL.Vertex3(offsetX + 1, 1, z);

                        GL.TexCoord2(right, bottom);
                        GL.Vertex3(offsetX + 1, 0, z);

                        GL.TexCoord2(left, bottom);
                        GL.Vertex3(offsetX, 0, z);

                        offsetX += spacing;
                    }
                }

                GL.End();
            }

            GL.PopMatrix();
        }

        public static void DrawTexture(Camera camera, string texture, Vector3 position, double rotation, double zoom, int alpha)
        {
            if (texture == null)
            {
                return;
            }

            TextureManager.Find(texture).Bind();
            float width = TextureManager.Find(texture).Width;
            float height = TextureManager.Find(texture).Height;

            position = camera.GetScreenPosition(position);

            GL.PushMatrix();
            {
                double alphaOffset = 1 - (double)alpha / (double)255;
                GL.Color4(1 - alphaOffset, 1 - alphaOffset, 1 - alphaOffset, 1);

                camera.SetViewport();
                camera.SetOrtho();

                // Translate to center of the texture
                GL.Translate(position.X, position.Y, 0);
                GL.Translate(width / 2, -1 * (height / 2), 0.0f);

                GL.Rotate(rotation, Vector3d.UnitZ);
                GL.Scale(zoom * camera.Zoom, zoom * camera.Zoom, 1f);

                // Translate back to the starting co-ordinates so drawing works
                GL.Translate(-1 * (width / 2), 1 * (height / 2), 0.0f);
                GL.Translate(-position.X, -(position.Y + height), 0);

                GL.Begin(PrimitiveType.Quads);
                {
                    GL.TexCoord2(0, 0);
                    GL.Vertex3(position.X, position.Y + height, position.Z);

                    GL.TexCoord2(0, 1);
                    GL.Vertex3(position.X, position.Y, position.Z);

                    GL.TexCoord2(1, 1);
                    GL.Vertex3(position.X + width, position.Y, position.Z);

                    GL.TexCoord2(1, 0);
                    GL.Vertex3(position.X + width, position.Y + height, position.Z);
                }

                GL.End();
            }

            GL.PopMatrix();
        }

        public static void DrawTile(Camera camera, string texture, Vector3 position, double rotation, double zoom, int alpha, int tile, int tileWidth, int tileHeight)
        {
            if (texture == null)
            {
                return;
            }

            TextureManager.Find(texture).Bind();
            float textureWidth = TextureManager.Find(texture).Width;
            float textureHeight = TextureManager.Find(texture).Height;

            position = camera.GetScreenPosition(position);

            GL.PushMatrix();
            {
                double alphaOffset = 1 - (double)alpha / (double)255;
                GL.Color4(1 - alphaOffset, 1 - alphaOffset, 1 - alphaOffset, 1);

                camera.SetViewport();
                camera.SetOrtho();

                // Translate to center of the texture
                GL.Translate(position.X, position.Y + tileHeight, 0);
                GL.Translate(tileWidth / 2, -1 * (tileHeight / 2), 0.0f);

                GL.Rotate(rotation, Vector3d.UnitZ);
                GL.Scale(zoom * camera.Zoom, zoom * camera.Zoom, 1f);

                // Translate back to the starting co-ordinates so drawing works
                GL.Translate(-1 * (tileWidth / 2), 1 * (tileHeight / 2), 0.0f);
                GL.Translate(-position.X, -(position.Y + tileHeight), 0);

                GL.Begin(PrimitiveType.Quads);
                {
                    float countX = textureWidth / tileWidth;
                    float countY = textureHeight / tileHeight;

                    int rowY = 0;
                    while (tile >= countX)
                    {
                        rowY++;
                        tile -= (int)countX;
                    }

                    float left = tile / countX;
                    float right = left + (1 / countX);

                    float top = rowY * (1 / countY);
                    float bottom = top + (1 / countY);

                    GL.TexCoord2(left, top);
                    GL.Vertex3(position.X, position.Y + tileHeight, position.Z);

                    GL.TexCoord2(left, bottom);
                    GL.Vertex3(position.X, position.Y, position.Z);

                    GL.TexCoord2(right, bottom);
                    GL.Vertex3(position.X + tileWidth, position.Y, position.Z);

                    GL.TexCoord2(right, top);
                    GL.Vertex3(position.X + tileWidth, position.Y + tileHeight, position.Z);
                }
                GL.End();
            }

            GL.PopMatrix();
        }
    }
}
