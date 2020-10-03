using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaMonogame.UI
{
    public class PUITooltip
    {
        public string Name;
        public Texture2D Texture;
    }

    internal struct PUITooltipTextSection
    {
        public Vector2 Position, Size;
        public string Text;
    }

    internal struct PUITooltipImageSection
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle SourceRect;
    }

    public static class PUITooltipManager
    {
        public static GraphicsDevice Graphics;
        public static DynamicSpriteFont DefaultFont;
        public static Color DefaultColor = new Color(0, 0, 0, 120);
        public static Color DefaultTextColor = new Color(255, 255, 255, 255);
        public static PUITooltip ActiveTooltip;
        public static Dictionary<string, PUITooltip> Tooltips = new Dictionary<string, PUITooltip>();

        public static void Setup(GraphicsDevice graphics, DynamicSpriteFont font)
        {
            Graphics = graphics;
            DefaultFont = font;
        }

        public static void Update(GameTime gameTime)
        {
            if (ActiveTooltip == null)
                return;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (ActiveTooltip == null)
                return;

            var mousePos = MouseManager.GetMousePosition();

            spriteBatch.Draw(ActiveTooltip.Texture, mousePos, Color.White);
        }

        public static void AddTooltipFromTexture(string name, Texture2D texture)
        {
            if (Tooltips.ContainsKey(name))
                return;
        }

        public static void AddTextTooltip(string name, string text, int size = 26, int padding = 15, bool enableImages = false)
        {
            AddTextTooltip(name, text, DefaultColor, DefaultTextColor, size, padding, enableImages);
        }

        public static void AddTextTooltip(string name, string text, Color backgroundColor, Color textColor, int size = 26, int padding = 15, bool enableImages = false)
        {
            AddTextTooltip(name, text, backgroundColor, textColor, DefaultFont, size, padding, enableImages);
        }

        public static void AddTextTooltip(string name, string text, Color backgroundColor, Color textColor, DynamicSpriteFont font, int size = 26, int padding = 15, bool enableImages = false)
        {
            if (Tooltips.ContainsKey(name))
                return;


            font.Size = size;

            if (enableImages)
            {
                var textureSize = Vector2.Zero;
                var currentPosition = Vector2.Zero;

                var textSections = new List<PUITooltipTextSection>();
                var imageSections = new List<PUITooltipImageSection>();

                var imageStartIndex = text.IndexOf("[");
                var imageEndIndex = text.IndexOf("]");

                while (imageStartIndex != -1 && imageEndIndex != -1 && imageStartIndex < imageEndIndex)
                {
                    if (imageStartIndex > 0)
                    {
                        var newText = text.Substring(0, imageStartIndex);
                        var newTextSize = font.MeasureString(newText);

                        var newTextSection = new PUITooltipTextSection()
                        {
                            Position = new Vector2()
                            {
                                X = currentPosition.X,
                                Y = 0,
                            },
                            Size = newTextSize,
                            Text = newText,
                        };

                        // quick hack to make build tooltips work
                        if (newTextSection.Position.X == 0 && newTextSection.Text.Contains("\n\n"))
                        {
                        }
                        else
                        {
                            currentPosition.X = newTextSection.Position.X + newTextSection.Size.X;
                        }

                        textSections.Add(newTextSection);

                        if (newTextSection.Position.X + newTextSection.Size.X > textureSize.X)
                            textureSize.X = newTextSection.Position.X + newTextSection.Size.X;

                        if (newTextSection.Position.Y + newTextSection.Size.Y > textureSize.Y)
                            textureSize.Y = newTextSection.Position.Y + newTextSection.Size.Y;
                    }

                    var imageString = text.Substring(imageStartIndex, imageEndIndex - imageStartIndex + 1);
                    imageString = imageString.Remove(0, 1);
                    imageString = imageString.Remove(imageString.Length - 1, 1);

                    var premultiplyAlpha = false;
                    if (imageString.StartsWith(":"))
                    {
                        premultiplyAlpha = true;
                        imageString = imageString.Remove(0, 1);
                    }

                    var sourceRectString = "";
                    var sourceRectIndex = imageString.IndexOf('-');

                    if (sourceRectIndex > -1)
                    {
                        sourceRectString = imageString.Substring(sourceRectIndex + 1);
                        imageString = imageString.Remove(sourceRectIndex);
                    }

                    var texture = ModManager.Instance.AssetManager.LoadTexture2D(Graphics, imageString, premultiplyAlpha);
                    var sourceRect = new Rectangle()
                    {
                        X = 0,
                        Y = 0,
                        Width = texture.Width,
                        Height = texture.Height,
                    };

                    if (sourceRectString.Length > 0)
                    {
                        var sourceRectSplit = sourceRectString.Split(',');
                        sourceRect = new Rectangle()
                        {
                            X = int.Parse(sourceRectSplit[0]),
                            Y = int.Parse(sourceRectSplit[1]),
                            Width = int.Parse(sourceRectSplit[2]),
                            Height = int.Parse(sourceRectSplit[3]),
                        };
                    }

                    var newImageSection = new PUITooltipImageSection()
                    {
                        Texture = texture,
                        SourceRect = sourceRect,
                        Position = new Vector2()
                        {
                            X = currentPosition.X,
                            Y = 0,
                        },
                    };

                    currentPosition.X = newImageSection.Position.X + newImageSection.SourceRect.Width;

                    imageSections.Add(newImageSection);

                    if (newImageSection.Position.X + newImageSection.SourceRect.Width > textureSize.X)
                        textureSize.X = newImageSection.Position.X + newImageSection.SourceRect.Width;

                    if (newImageSection.Position.Y + newImageSection.SourceRect.Height > textureSize.Y)
                        textureSize.Y = newImageSection.Position.Y + newImageSection.SourceRect.Height;

                    text = text.Remove(0, imageEndIndex + 1);

                    imageStartIndex = text.IndexOf("[");
                    imageEndIndex = text.IndexOf("]");
                }

                if (text.Length > 0)
                {
                    var newTextSection = new PUITooltipTextSection()
                    {
                        Position = new Vector2()
                        {
                            X = currentPosition.X,
                            Y = 0,
                        },
                        Size = font.MeasureString(text),
                        Text = text,
                    };

                    if (newTextSection.Position.X + newTextSection.Size.X > textureSize.X)
                        textureSize.X = newTextSection.Position.X + newTextSection.Size.X;

                    if (newTextSection.Position.Y + newTextSection.Size.Y > textureSize.Y)
                        textureSize.Y = newTextSection.Position.Y + newTextSection.Size.Y;

                    textSections.Add(newTextSection);
                }

                textureSize.X += padding * 2;
                textureSize.Y += padding * 2;
                var vPadding = new Vector2(padding);

                var newTexture = new RenderTarget2D(Graphics, (int)textureSize.X, (int)textureSize.Y);

                using (var spriteBatch = new SpriteBatch(Graphics))
                {
                    Graphics.SetRenderTarget(newTexture);
                    Graphics.Clear(backgroundColor);

                    spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);

                    foreach (var textSection in textSections)
                    {
                        spriteBatch.DrawString(font, textSection.Text, textSection.Position + vPadding, textColor);
                    }

                    foreach(var imageSection in imageSections)
                    {
                        spriteBatch.Draw(imageSection.Texture, imageSection.Position + vPadding, imageSection.SourceRect, Color.White);
                    }

                    spriteBatch.End();

                    Graphics.SetRenderTarget(null);
                }

                Tooltips.Add(name, new PUITooltip()
                {
                    Name = name,
                    Texture = newTexture,
                });
            }
            else
            {
                var textSize = font.MeasureString(text);
                var newTexture = new RenderTarget2D(Graphics, (int)textSize.X + padding * 2, (int)textSize.Y + padding * 2);

                using (var spriteBatch = new SpriteBatch(Graphics))
                {
                    Graphics.SetRenderTarget(newTexture);
                    Graphics.Clear(backgroundColor);

                    spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
                    spriteBatch.DrawString(font, text, new Vector2(padding), textColor);
                    spriteBatch.End();

                    Graphics.SetRenderTarget(null);
                }

                Tooltips.Add(name, new PUITooltip()
                {
                    Name = name,
                    Texture = newTexture,
                });
            }
        }

        public static void SetActiveTooltip(string name = "")
        {
            if (!Tooltips.ContainsKey(name))
            {
                ActiveTooltip = null;
                return;
            }

            ActiveTooltip = Tooltips[name];
        }

        public static void SetActiveTooltip(PUITooltip tooltip = null)
        {
            ActiveTooltip = tooltip;
        }

        public static void Clear()
        {
            foreach (var kvp in Tooltips)
                kvp.Value.Texture.Dispose();
        }
    }
}
