using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    class TileDisplay : GraphicsDeviceControl
    {
        Editor editor;
        Image image;
        SpriteBatch spriteBatch;
        List<Image> selector;
        bool isMouseDown;
        Vector2 mousePosition, clickPosition;

        public TileDisplay(ref Editor editor)
        {
            this.editor = editor;
            editor.OnInitialize += LoadContent;
            isMouseDown = false;
        }

        private void LoadContent(object sender, EventArgs e)
        {
            image = editor.CurrentLayer.Image;
            selector = editor.Selectors;
        }
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MouseDown += TileDisplay_MouseDown;
            MouseUp += delegate
            {
                isMouseDown = false;
                List<Image> selector = editor.Selectors;

                editor.SelectedRegion = new Rectangle((int)selector[0].Position.X, (int)selector[0].Position.Y,
                    (int)(selector[1].Position.X - selector[0].Position.X), (int)(selector[2].Position.Y - selector[0].Position.Y));

                editor.SelectedRegion.X /= 32;
                editor.SelectedRegion.Y /= 32;
                editor.SelectedRegion.Width /= 32;
                editor.SelectedRegion.Height /= 32;
            };
            MouseMove += TileDisplay_MouseMove;
        }

        private void TileDisplay_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(!isMouseDown)
            {
                clickPosition = mousePosition;
                foreach (Image img in selector)
                    img.Position = mousePosition;
            }
            isMouseDown = true;
        }
        private void TileDisplay_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousePosition = new Vector2((int)(e.X / editor.CurrentLayer.TileDimension.X),
                (int)(e.Y / editor.CurrentLayer.TileDimension.Y));
            mousePosition *= 32;

            if(mousePosition != clickPosition && isMouseDown)
            {
                for(int i = 0; i <4; i++)
                {
                    if (i % 2 == 0 && mousePosition.X < clickPosition.X)
                        selector[i].Position.X = mousePosition.X;
                    else if (i % 2 != 0 && mousePosition.X > clickPosition.X)
                        selector[i].Position.X = mousePosition.X;
                    if (i < 2 && mousePosition.Y < clickPosition.Y)
                        selector[i].Position.Y = mousePosition.Y;
                    else if (i >= 2 && mousePosition.Y > clickPosition.Y)
                        selector[i].Position.Y = mousePosition.Y;
                }
                Invalidate();
            }
            else if(isMouseDown)
            {
                foreach (Image img in selector)
                    img.Position = mousePosition;
                Invalidate();
            }
        }
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            image.Draw(spriteBatch);
            foreach (Image img in selector)
                img.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
