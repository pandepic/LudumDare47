using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace PandaMonogame.Assets
{
    public class TexturePackerSprite
    {
        public Rectangle SourceRect;
        public string Name;
        public Texture2D Texture;
    }

    public class TexturePackerAsset
    {
        public string AssetName;
        public Texture2D Texture;
        public Dictionary<string, TexturePackerSprite> Sprites;
    }

    public static class TexturePacker
    {
        public static Dictionary<string, TexturePackerAsset> Assets = new Dictionary<string, TexturePackerAsset>();

        public static void LoadAsset(GraphicsDevice graphics, string assetName, bool premultiplyAlpha = false)
        {
            using (var fs = ModManager.Instance.AssetManager.GetFileStreamByAsset(assetName))
            {
                var asset = new TexturePackerAsset();
                asset.AssetName = assetName;
                asset.Sprites = new Dictionary<string, TexturePackerSprite>();

                var doc = XDocument.Load(fs);

                asset.Texture = ModManager.Instance.AssetManager.LoadTexture2D(graphics, doc.Root.Attribute("AssetName").Value, premultiplyAlpha);

                foreach (var elSprite in doc.Root.Elements("Sprite"))
                {
                    var sprite = new TexturePackerSprite();
                    sprite.Name = elSprite.Attribute("Name").Value;
                    sprite.Texture = asset.Texture;
                    sprite.SourceRect = new Rectangle()
                    {
                        X = int.Parse(elSprite.Attribute("X").Value),
                        Y = int.Parse(elSprite.Attribute("Y").Value),
                        Width = int.Parse(elSprite.Attribute("Width").Value),
                        Height = int.Parse(elSprite.Attribute("Height").Value),
                    };

                    asset.Sprites.Add(sprite.Name, sprite);
                }

                Assets.Add(asset.AssetName, asset);
            }
        } // LoadAsset

        public static TexturePackerSprite GetSprite(string assetName, string spriteName)
        {
            return Assets[assetName].Sprites[spriteName];
        }

        public static Rectangle GetSourceRect(string assetName, string spriteName)
        {
            return Assets[assetName].Sprites[spriteName].SourceRect;
        }
    }
}
