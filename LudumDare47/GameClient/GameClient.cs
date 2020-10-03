using GameCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using PandaMonogame.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameClient
{
    public class GameClient : Game
    {
        readonly GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        protected GameState _currentGameState = null;

        protected readonly KeyboardManager _keyboardManager = new KeyboardManager();
        protected readonly MouseManager _mouseManager = new MouseManager();

        protected GameTime _lastGameTime = null;

#if DEBUG
        TimeSpan _frameCounterElapsedTime = TimeSpan.Zero;
        int _frameCounter = 0;
        long _drawMS, _updateMS;
        readonly string _windowTitle = "Game Client";
#endif

        public GameClient()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };
        }

        protected override void Initialize()
        {
            Content.RootDirectory = "";
            IsMouseVisible = true;

            PandaMonogameConfig.UISoundType = (int)SoundType.UI;

#if DEBUG
            PandaMonogameConfig.Logging = true;
            IsFixedTimeStep = false;
            _graphics.SynchronizeWithVerticalRetrace = false;
#endif

            ModManager.Instance.Init(Content);
            ModManager.Instance.LoadList("Mods", "mods.xml", "assets.xml");

            SettingsManager.Instance.Load("Assets\\settings.xml");
            _graphics.PreferredBackBufferWidth = SettingsManager.Instance.GetSetting<int>("window", "width");
            _graphics.PreferredBackBufferHeight = SettingsManager.Instance.GetSetting<int>("window", "height");
            _graphics.ApplyChanges();

            ModManager.Instance.SoundManager.SetVolume((int)SoundType.Music, SettingsManager.Instance.GetSetting<float>("sound", "musicvolume"));
            ModManager.Instance.SoundManager.SetVolume((int)SoundType.SoundEffect, SettingsManager.Instance.GetSetting<float>("sound", "sfxvolume"));
            ModManager.Instance.SoundManager.SetVolume((int)SoundType.UI, SettingsManager.Instance.GetSetting<float>("sound", "uivolume"));

            Globals.Load(GraphicsDevice);
            
            PUITooltipManager.Setup(GraphicsDevice, Globals.DefaultFont);

            Window.TextInput += Window_TextInput;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ChangeGameState((int)GameStateType.Menu);
        }

        protected override void UnloadContent()
        {
            PandaUtil.Cleanup();
        }

        protected override void Update(GameTime gameTime)
        {
#if DEBUG
            var watch = System.Diagnostics.Stopwatch.StartNew();
#endif

            ModManager.Instance.SoundManager.Update();
            _lastGameTime = gameTime;

            if (IsActive)
            {
                _keyboardManager.Update(gameTime);
                _mouseManager.Update(gameTime);
            }

            int newState = _currentGameState.Update(gameTime);

            if (newState == (int)GameStateType.Exit)
            {
                Exit();
                return;
            }

            if (newState != (int)GameStateType.None)
                ChangeGameState(newState);

#if DEBUG
            // fps counter
            _frameCounter++;
            _frameCounterElapsedTime += gameTime.ElapsedGameTime;

            if (_frameCounterElapsedTime >= TimeSpan.FromSeconds(1))
            {
                Window.Title = string.Format("{0} {1} fps - Draw {2}ms Update {3}ms", _windowTitle, _frameCounter, _drawMS, _updateMS);
                _frameCounter = 0;
                _frameCounterElapsedTime -= TimeSpan.FromSeconds(1);
            }
#endif

            base.Update(gameTime);

#if DEBUG
            watch.Stop();
            _updateMS = watch.ElapsedMilliseconds;
#endif
        }

        protected void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (_lastGameTime == null)
                return;

            _keyboardManager.TextInput(e, _lastGameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
#if DEBUG
            var watch = System.Diagnostics.Stopwatch.StartNew();
#endif

            _currentGameState.Draw(gameTime, GraphicsDevice, _spriteBatch);
            base.Draw(gameTime);

#if DEBUG
            watch.Stop();
            _drawMS = watch.ElapsedMilliseconds;
#endif
        }
        
        private void ChangeGameState(int newState)
        {
            ModManager.Instance.AssetManager.Clear();

            switch (newState)
            {
                case (int)GameStateType.Menu:
                    {
                        _currentGameState = new MenuState();
                    }
                    break;

                case (int)GameStateType.GamePlay:
                    {
                        _currentGameState = new GamePlayState();
                    }
                    break;
            }

            _currentGameState.Load(Content, GraphicsDevice);
            _keyboardManager.OnKeyDown = new KEYBOARD_EVENT(_currentGameState.OnKeyDown);
            _keyboardManager.OnKeyPressed = new KEYBOARD_EVENT(_currentGameState.OnKeyPressed);
            _keyboardManager.OnKeyReleased = new KEYBOARD_EVENT(_currentGameState.OnKeyReleased);
            _keyboardManager.OnTextInput = new TEXTINPUT_EVENT(_currentGameState.OnTextInput);
            _mouseManager.OnMouseClicked = new MOUSEBUTTON_EVENT(_currentGameState.OnMouseClicked);
            _mouseManager.OnMouseDown = new MOUSEBUTTON_EVENT(_currentGameState.OnMouseDown);
            _mouseManager.OnMouseMoved = new MOUSEPOSITION_EVENT(_currentGameState.OnMouseMoved);
            _mouseManager.OnMouseScroll = new MOUSESCROLL_EVENT(_currentGameState.OnMouseScroll);
        }
    }
}
