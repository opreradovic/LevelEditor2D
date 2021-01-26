using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;
        }

        Vector2 tileDimensions;
        List<List<Vector2>> tileMap;

        //[XmlElement("TileMap")]
        public TileMap TileLayout = null;
        public Image Image;

        public Vector2 TileDimension
        {
            get { return tileDimensions; }
        }

        public Layer()
        {
            tileDimensions = new Vector2();
            tileMap = new List<List<Vector2>>();
        }
        public void ReplaceTiles(Vector2 position, Rectangle selectedRegion)
        {
            Vector2 startIndex = new Vector2(position.X / tileDimensions.X, position.Y / tileDimensions.Y);
            Vector2 tileIndex = new Vector2(selectedRegion.X, selectedRegion.Y - 1);
            Vector2 mapIndex = Vector2.Zero;

            for (int i = (int)startIndex.Y; i<= startIndex.Y + selectedRegion.Height; i++)
            {
                tileIndex.X = selectedRegion.X;
                tileIndex.Y++;
                for(int j = (int)startIndex.X; j<= startIndex.X + selectedRegion.Width;j++)
                {
                    if (tileIndex.X + tileDimensions.X > Image.Texture.Width ||
                        tileIndex.Y + tileDimensions.Y > Image.Texture.Height)
                        mapIndex = -Vector2.One;
                    else
                        mapIndex = tileIndex;

                    try
                    {
                        tileMap[i][j] = mapIndex;
                    }
                    catch(Exception e)
                    {
                        while (tileMap.Count <= i)
                        {
                            List<Vector2> tempTileMap = new List<Vector2>();
                            for (int k = 0; k < tileMap[0].Count; k++)
                                tempTileMap.Add(-Vector2.One);
                            tileMap.Add(tempTileMap);
                        }
                        while (tileMap[i].Count <= j)
                            tileMap[i].Add(-Vector2.One);

                        tileMap[i][j] = mapIndex;
                }

                    tileIndex.X++;
                }
            }
        }
        public void Initialize(ContentManager content, Vector2 tileDimensions)
        {
            //if (TileLayout == null) return;
            foreach(string row in TileLayout.Row)
            {
                string[] split = row.Split(']');
                List<Vector2> tempTileMap = new List<Vector2>();
                foreach(string s in split)
                {
                    int value1, value2 = 0;
                    if (s != String.Empty && !s.Contains('x'))
                    {
                        string str = s.Replace("[", String.Empty);
                        value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                        value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));
                    }

                    else
                        value1 = value2 = -1;

                    tempTileMap.Add(new Vector2(value1, value2));
                }
                tileMap.Add(tempTileMap);
            }

            Image.Initialize(content);
            this.tileDimensions = tileDimensions;
        }
        public void Draw(SpriteBatch spriteBatch)
        { 
            for(int i = 0; i < tileMap.Count; i++)
            {
                for(int j = 0; j < tileMap[i].Count;j++)
                {
                    if(tileMap[i][j] != -Vector2.One)
                    {
                        Image.Position = new Vector2(j * tileDimensions.X, i * tileDimensions.Y);

                        Image.SourceRect = new Rectangle((int)(tileMap[i][j].X * tileDimensions.X),
                            (int)(tileMap[i][j].Y * tileDimensions.Y), (int)tileDimensions.X,
                            (int)tileDimensions.Y);
                        Image.Draw(spriteBatch);
                    }
                }
            }
            Image.Position = Vector2.Zero;
            Image.SourceRect = Image.Texture.Bounds;
        }
    }
}
