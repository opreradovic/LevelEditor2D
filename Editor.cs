using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    class Editor : GraphicsDeviceControl
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        Map map;
        int layerNumber;
        bool isMouseDown = false;
        public List<Image> Selectors;
        public Rectangle SelectedRegion;
        public List<Vector2> SelectedTiles;
        public Vector2 SelectorDimensions;
        Vector2 mousePosition;
        bool mouseOnScreen = false;
        string[] SelectorPath = { "SelecorT1", "SelecorT2", "SelecorB1", "SelectorB2" };

        public event EventHandler OnInitialize;

        public Editor()
        {
            map = new Map();
            layerNumber = 0;
            Selectors = new List<Image>();

            for (int i = 0; i < 4; i++)
                Selectors.Add(new Image());

            SelectorDimensions = Vector2.Zero;
            SelectedRegion = new Rectangle(0, 0, 0, 0);

            SelectedTiles = new List<Vector2>();
            SelectedTiles.Add(Vector2.Zero);
            MouseMove += Editor_MouseMove;
            MouseDown += Editor_MouseDown;
            MouseUp += delegate { isMouseDown = false; };
            MouseEnter += delegate { mouseOnScreen = true; };
            MouseLeave += delegate { mouseOnScreen = false; Draw(); Invalidate(); };
        }

        private void Editor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CurrentLayer.ReplaceTiles(mousePosition, SelectedRegion);
            isMouseDown = true;
        }

        private void Editor_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousePosition = new Vector2((int)(e.X / CurrentLayer.TileDimension.X),
               (int)(e.Y / CurrentLayer.TileDimension.Y));
            mousePosition *= 32;

            int width = (int)(SelectedRegion.Width * CurrentLayer.TileDimension.X);
            int height = (int)(SelectedRegion.Height * CurrentLayer.TileDimension.Y);

            Selectors[0].Position = mousePosition;
            Selectors[1].Position = new Vector2(mousePosition.X + width, mousePosition.Y);
            Selectors[2].Position = new Vector2(mousePosition.X, mousePosition.Y + height);
            Selectors[3].Position = new Vector2(mousePosition.X + width, mousePosition.Y + height);

            if (isMouseDown)
                Editor_MouseDown(this, null);

            Invalidate();
        }

        public Layer CurrentLayer
        {
            get { return map.Layer[layerNumber]; }
        }
        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < 4; i++)
            {
                Selectors[i].Path = SelectorPath[i];
                Selectors[i].Initialize(content);
            }

            XmlSerializer xml = new XmlSerializer(map.GetType());
            Stream stream = File.Open("Load/Map.xml", FileMode.Open);
            map = (Map)(xml.Deserialize(stream));
            map.Initialize(content);

            if (OnInitialize != null)
                OnInitialize(this, null);
        }
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            if(mouseOnScreen)
            {
                foreach (Image img in Selectors)
                    img.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
