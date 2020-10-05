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
    public enum MainMenuButton
    {
        Play = 0, Settings, Exit
    }

    public class MenuState : GameState
    {
        protected GameStateType _nextState = GameStateType.None;
        protected PUIMenu _menu = new PUIMenu();

        protected Camera _camera = new Camera(new Vector2(160, 90), (320, 160));

        protected MainMenuButton _mainMenuButton = MainMenuButton.Play;

        protected AnimatedSprite _menuBG;
        protected AnimatedSprite _menuSprite;

        protected Animation _animMenuSpritePlay = new Animation(1, 26, 2600);
        protected Animation _animMenuSpriteSettings = new Animation(27, 52, 2600);
        protected Animation _animMenuSpriteExit = new Animation(53, 78, 2600);

        #region Python Methods
        protected void StartNewGame(params object[] args)
        {
            _nextState = GameStateType.GamePlay;
        }

        protected void Settings(params object[] args)
        {
            _nextState = GameStateType.Settings;
        }

        protected void Exit(params object[] args)
        {
            _nextState = GameStateType.Exit;
        }
        #endregion

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
            _menu.AddMethod(StartNewGame);
            _menu.AddMethod(Settings);
            _menu.AddMethod(Exit);
            _menu.Load(graphics, "MainMenuDefinition", "UITemplates");

            ModManager.Instance.SoundManager.PlaySound("MusicMenu", (int)SoundType.Music, true);

            _menuBG = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "TitleBackground"), 320, 160);
            _menuSprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "TitleMainSprite"), 320, 160);

            UpdateBG();
        }

        protected void UpdateBG()
        {
            switch (_mainMenuButton)
            {
                case MainMenuButton.Play:
                    {
                        _menuSprite.PlayAnimation(_animMenuSpritePlay);
                    }
                    break;

                case MainMenuButton.Settings:
                    {
                        _menuSprite.PlayAnimation(_animMenuSpriteSettings);
                    }
                    break;

                case MainMenuButton.Exit:
                    {
                        _menuSprite.PlayAnimation(_animMenuSpriteExit);
                    }
                    break;
            }
        }

        protected void SelectBG()
        {
            switch (_mainMenuButton)
            {
                case MainMenuButton.Play:
                    {
                        _nextState = GameStateType.GameOver;
                    }
                    break;

                case MainMenuButton.Settings:
                    {
                        _nextState = GameStateType.Settings;
                    }
                    break;

                case MainMenuButton.Exit:
                    {
                        _nextState = GameStateType.Exit;
                    }
                    break;
            }
        }

        protected void MenuOptionUp()
        {
            int option = (int)_mainMenuButton;

            option -= 1;
            if (option < 0)
                option = 0;

            _mainMenuButton = (MainMenuButton)option;

            UpdateBG();
        }

        protected void MenuOptionDown()
        {
            int option = (int)_mainMenuButton;

            option += 1;
            if (option > 2)
                option = 2;

            _mainMenuButton = (MainMenuButton)option;

            UpdateBG();
        }

        public override int Update(GameTime gameTime)
        {
            //return (int)GameStateType.GamePlay;
            _menuSprite.Update(gameTime);
            return (int)_nextState;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);

            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: _camera.View());
            _menuBG.Draw(spriteBatch);
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
                case Keys.W:
                case Keys.Up:
                    {
                        MenuOptionUp();
                    }
                    break;

                case Keys.S:
                case Keys.Down:
                    {
                        MenuOptionDown();
                    }
                    break;

                case Keys.Space:
                case Keys.Enter:
                    {
                        SelectBG();
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
