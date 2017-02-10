//-----------------------------------------------------------------------
// <copyright file="TileMap.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Maps
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Xml;
    
    /// <summary>
    /// The map data class
    /// </summary>
    public class TileMap
    {
        /// <summary>
        /// Initializes a new instance of the TileMap class
        /// </summary>
        public TileMap()
        {
            this.Layers = new List<Layer>();
            this.Layers.Add(new Layer(0, 1, 1));
            this.Background = Color.HotPink;
        }

        /// <summary>
        /// Gets or sets the map's tile size
        /// </summary>
        public int TileSize { get; set; }

        /// <summary>
        /// Gets or sets the map's tile texture
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Gets or sets the map's tile data
        /// </summary>
        public List<Layer> Layers { get; set; }

        /// <summary>
        /// Gets or sets the map's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the map's background color
        /// </summary>
        public Color Background { get; set; }

        /// <summary>
        /// Saves the current map to the given .map file path
        /// </summary>
        /// <param name="fileName">name of the file to save as, remember to add (.map)</param>
        public void Save(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            XmlWriter xmlWriter = XmlWriter.Create(fileStream, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Map");

            xmlWriter.WriteStartElement("Settings");
            xmlWriter.WriteAttributeString("TileSize", this.TileSize.ToString());
            xmlWriter.WriteAttributeString("Name", this.Name);
            xmlWriter.WriteAttributeString("R", this.Background.R.ToString());
            xmlWriter.WriteAttributeString("G", this.Background.G.ToString());
            xmlWriter.WriteAttributeString("B", this.Background.B.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Layers");

            for (int i = 0; i < this.Layers.Count; i++)
            {
                xmlWriter.WriteStartElement("Layer");
                xmlWriter.WriteAttributeString("Order", i.ToString());
                xmlWriter.WriteAttributeString("Width", this.Layers[i].Width.ToString());
                xmlWriter.WriteAttributeString("Height", this.Layers[i].Height.ToString());
                xmlWriter.WriteAttributeString("RepeatX", this.Layers[i].RepeatX.ToString());
                xmlWriter.WriteAttributeString("RepeatY", this.Layers[i].RepeatY.ToString());
                xmlWriter.WriteAttributeString("ScrollSpeedX", this.Layers[i].ScrollSpeedX.ToString());
                xmlWriter.WriteAttributeString("ScrollSpeedY", this.Layers[i].ScrollSpeedY.ToString());

                for (int x = 0; x < this.Layers[i].Width; x++)
                {
                    for (int y = 0; y < this.Layers[i].Height; y++)
                    {
                        xmlWriter.WriteStartElement("Tile");
                        xmlWriter.WriteAttributeString("X", x.ToString());
                        xmlWriter.WriteAttributeString("Y", y.ToString());
                        xmlWriter.WriteAttributeString("Tile", this.Layers[i].Tiles[x, y].ToString());
                        xmlWriter.WriteEndElement();
                    }
                }

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            fileStream.Close();
        }

        /// <summary>
        /// Loads the current map to the given .map file path
        /// </summary>
        /// <param name="fileName">name of the file to load, remember to add (.map) extension</param>
        public void Load(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            XmlReader xmlReader = XmlReader.Create(fileStream);

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (xmlReader.Name == "Settings")
                    {
                        this.TileSize = int.Parse(xmlReader.GetAttribute("TileSize"));
                        this.Name = xmlReader.GetAttribute("Name");

                        byte r, g, b;
                        r = byte.Parse(xmlReader.GetAttribute("R"));
                        g = byte.Parse(xmlReader.GetAttribute("G"));
                        b = byte.Parse(xmlReader.GetAttribute("B"));
                        this.Background = Color.FromArgb(255, r, g, b);
                    }

                    if (xmlReader.Name == "Layers")
                    {
                        this.Layers = new List<Layer>();
                    }

                    if (xmlReader.Name == "Layer")
                    {
                        int layer = int.Parse(xmlReader.GetAttribute("Order"));
                        this.Layers.Add(new Layer(int.Parse(xmlReader.GetAttribute("Order")), int.Parse(xmlReader.GetAttribute("Width")), int.Parse(xmlReader.GetAttribute("Height"))));
                        this.Layers[layer].RepeatX = bool.Parse(xmlReader.GetAttribute("RepeatX"));
                        this.Layers[layer].RepeatY = bool.Parse(xmlReader.GetAttribute("RepeatY"));
                        this.Layers[layer].ScrollSpeedX = float.Parse(xmlReader.GetAttribute("ScrollSpeedX"));
                        this.Layers[layer].ScrollSpeedY = float.Parse(xmlReader.GetAttribute("ScrollSpeedY"));
                        this.Layers[layer].Tiles = new int[this.Layers[layer].Width, this.Layers[layer].Height];

                        using (XmlReader innerNode = xmlReader.ReadSubtree())
                        {
                            while (innerNode.Read())
                            {
                                if (innerNode.Name == "Tile")
                                {
                                    int x = int.Parse(innerNode.GetAttribute("X"));
                                    int y = int.Parse(innerNode.GetAttribute("Y"));
                                    this.Layers[layer].Tiles[x, y] = int.Parse(innerNode.GetAttribute("Tile"));
                                }
                            }
                        }
                    }
                }
            }

            xmlReader.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Draws all map layers onto the screen
        /// </summary>
        public void Draw(Camera camera)
        {
            foreach (Layer layer in this.Layers)
            {
                layer.Draw(this.TileSize, camera, this.Texture);
            }
        }

        /// <summary>
        /// Draws a map layer onto the screen
        /// </summary>
        /// <param name="layer">current layer index to draw</param>
        public void Draw(int layer, Camera camera)
        {
            if (layer > this.Layers.Count)
            {
                return; // invalid layer index
            }

            this.Layers[layer].Draw(this.TileSize, camera, this.Texture);
        }

        /// <summary>
        /// Flips the Y tiles around
        /// </summary>
        public void FlipY()
        {
            foreach (Layer currentLayer in this.Layers)
            {
                int[,] temp = (int[,])currentLayer.Tiles.Clone();
                for (int x = 0; x < currentLayer.Tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < currentLayer.Tiles.GetLength(1); y++)
                    {
                        int flip = currentLayer.Tiles.GetLength(1) - y - 1;
                        currentLayer.Tiles[x, y] = temp[x, flip];
                    }
                }
            }
        }

        /// <summary>
        /// Flips the X tiles around
        /// </summary>
        public void FlipX()
        {
            foreach (Layer currentLayer in this.Layers)
            {
                int[,] temp = (int[,])currentLayer.Tiles.Clone();
                for (int x = 0; x < currentLayer.Tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < currentLayer.Tiles.GetLength(1); y++)
                    {
                        int flip = currentLayer.Tiles.GetLength(0) - x - 1;
                        currentLayer.Tiles[x, y] = temp[flip, y];
                    }
                }
            }
        }

        /// <summary>
        /// Moves all layers relation to the given movement (use for parallax and positioning)
        /// </summary>
        /// <param name="x">x axis movement</param>
        /// <param name="y">y axis movement</param>
        /// <param name="absolute">a value indicating if movement is absolute or relational</param>
        public void MoveAll(float x, float y, bool absolute)
        {
            foreach (Layer layer in this.Layers)
            {
                if (absolute)
                {
                    layer.ScrollX = x * layer.ScrollSpeedX;
                    layer.ScrollY = y * layer.ScrollSpeedY;
                }
                else
                {
                    layer.ScrollX += x * layer.ScrollSpeedX;
                    layer.ScrollY += y * layer.ScrollSpeedY;
                }

                if (layer.RepeatX)
                {
                    if (layer.ScrollX > layer.Width * this.TileSize)
                    {
                        layer.ScrollX -= layer.Width * this.TileSize;
                    }

                    if (layer.ScrollX < (layer.Width * this.TileSize) * -1)
                    {
                        layer.ScrollX += layer.Width * this.TileSize;
                    }
                }

                if (layer.RepeatY)
                {
                    if (layer.ScrollY > layer.Height * this.TileSize)
                    {
                        layer.ScrollY -= layer.Height * this.TileSize;
                    }

                    if (layer.ScrollY < (layer.Height * this.TileSize) * -1)
                    {
                        layer.ScrollY += layer.Height * this.TileSize;
                    }
                }
            }
        }
    }
}
