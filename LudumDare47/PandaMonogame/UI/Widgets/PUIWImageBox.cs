using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PandaMonogame.UI
{
    public class PUIWImageBox : PUIWidget
    {
        protected Texture2D _image = null;
        protected Rectangle _sourceRect = Rectangle.Empty;

        public PUIWImageBox() { }

        public override void Load(PUIFrame parent, XElement el)
        {
            Init(parent, el);

            bool preMultiplyAlpha = false;

            var elAlpha = GetXMLElement("PreMultiplyAlpha");
            if (elAlpha != null)
                preMultiplyAlpha = bool.Parse(elAlpha.Value);

            _image = ModManager.Instance.AssetManager.LoadTexture2D(parent.CommonWidgetResources.Graphics, GetXMLElement("AssetName").Value, preMultiplyAlpha);

            Width = _image.Width;
            Height = _image.Height;

            var elSourceRect = GetXMLElement("SourceRect");
            if (elSourceRect != null)
            {
                _sourceRect = new Rectangle()
                {
                    X = GetXMLAttribute<int>("SourceRect", "X"),
                    Y = GetXMLAttribute<int>("SourceRect", "Y"),
                    Width = GetXMLAttribute<int>("SourceRect", "Width"),
                    Height = GetXMLAttribute<int>("SourceRect", "Height"),
                };

                Width = _sourceRect.Width;
                Height = _sourceRect.Height;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var drawPosition = Position + Parent.Position;
            var drawRect = new Rectangle((int)drawPosition.X, (int)drawPosition.Y, Width, Height);

            if (!_sourceRect.IsEmpty)
            {
                drawRect.Width = _sourceRect.Width;
                drawRect.Height = _sourceRect.Height;
                spriteBatch.Draw(_image, drawRect, _sourceRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(_image, drawRect, Color.White);
            }
        }
    }
}
