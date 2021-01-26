﻿using System;
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
    public class Image
    {
        private Texture2D texture;
        private ContentManager content;
        public Rectangle SourceRect;
        public float Alpha;
        public string Path;
        public Vector2 Position;
        
        [XmlIgnore]
        public Texture2D Texture
        {
            get { return texture; }
        }
        
        public Image()
        {
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
        }

        public void Initialize(ContentManager content)
        {
            //Path = @"Editor/SelecorB1.png";
            this.content = new ContentManager(content.ServiceProvider, "Content");

            if (!string.IsNullOrEmpty(Path))
                texture = this.content.Load<Texture2D>(Path);

            if (SourceRect == Rectangle.Empty)
                SourceRect = texture.Bounds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, SourceRect, Color.White * Alpha);
        }
    }
}
