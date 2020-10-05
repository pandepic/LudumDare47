using Dcrew.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PandaMonogame;
using PandaMonogame.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class GameOverState : GameState
    {
        protected GameStateType _nextState = GameStateType.None;
        protected PUIMenu _menu = new PUIMenu();

        protected Camera _camera = new Camera(new Vector2(160, 90), (320, 160));

        protected AnimatedSprite _menuSprite;
        protected Animation _animMenuSprite = new Animation(1, 10, 1000)
        {
            EndFrame = 10,
        };

        protected float sfxTimer = 500;
        protected bool sfxPlayed = false;

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
            _menu.Load(graphics, "GameOverMenuDefinition", "UITemplates");
            _menuSprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "YouWinSprite"), 320, 160);
            _menuSprite.PlayAnimation(_animMenuSprite, 1);

            ModManager.Instance.SoundManager.StopAll();
            ModManager.Instance.SoundManager.PlaySound("MusicMenu", (int)SoundType.Music, true);
        }

        public override int Update(GameTime gameTime)
        {
            sfxTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (sfxTimer <= 0)
            {
                sfxTimer = 0;

                if (!sfxPlayed)
                {
                    ModManager.Instance.SoundManager.PlaySound("SFXShoot", (int)SoundType.SoundEffect, false);
                    sfxPlayed = true;
                }
            }

            _menuSprite.Update(gameTime);
            return (int)_nextState;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);

            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: _camera.View());
            _menuSprite.Draw(spriteBatch);
            //_menu.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void OnMouseMoved(Vector2 originalPosition, GameTime gameTime)
        {
            //_menu.OnMouseMoved(originalPosition, gameTime);
        }
        public override void OnMouseDown(MouseButtonID button, GameTime gameTime)
        {
            //_menu.OnMouseDown(button, gameTime);
        }

        public override void OnMouseClicked(MouseButtonID button, GameTime gameTime)
        {
            //_menu.OnMouseClicked(button, gameTime);
        }

        public override void OnMouseScroll(MouseScrollDirection direction, int scrollValue, GameTime gameTime)
        {
            //_menu.OnMouseScroll(direction, scrollValue, gameTime);
        }

        public override void OnKeyPressed(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            //_menu.OnKeyPressed(key, gameTime, currentKeyState);

            switch (key)
            {
                case Keys.Back:
                case Keys.Escape:
                case Keys.Space:
                case Keys.Enter:
                    {
                        _nextState = GameStateType.Menu;
                    }
                    break;
            }
        }

        public override void OnKeyReleased(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            //_menu.OnKeyReleased(key, gameTime, currentKeyState);
        }

        public override void OnKeyDown(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            //_menu.OnKeyDown(key, gameTime, currentKeyState);
        }

        public override void OnTextInput(TextInputEventArgs e, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            //_menu.OnTextInput(e, gameTime, currentKeyState);
        }
    }
}
