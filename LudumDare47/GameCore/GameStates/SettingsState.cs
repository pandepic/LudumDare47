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
    public class SettingsState : GameState
    {
        protected GameStateType _nextState = GameStateType.None;
        protected PUIMenu _menu = new PUIMenu();

        #region python bound methods
        protected void UpdateMusicVolume(params object[] args)
        {
            var volume = float.Parse(args[0].ToString());
            ModManager.Instance.SoundManager.SetVolume((int)SoundType.Music, volume);
            SettingsManager.Instance.UpdateSetting("sound", "musicvolume", volume.ToString());
        }

        protected void UpdateSFXVolume(params object[] args)
        {
            var volume = float.Parse(args[0].ToString());
            ModManager.Instance.SoundManager.SetVolume((int)SoundType.SoundEffect, volume);
            SettingsManager.Instance.UpdateSetting("sound", "sfxvolume", volume.ToString());
        }

        protected void UpdateUIVolume(params object[] args)
        {
            var volume = float.Parse(args[0].ToString());
            ModManager.Instance.SoundManager.SetVolume((int)SoundType.UI, volume);
            SettingsManager.Instance.UpdateSetting("sound", "uivolume", volume.ToString());
        }

        protected void BackToMenu(params object[] args)
        {
            _nextState = GameStateType.Menu;
        }
        #endregion

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
            _menu.AddMethod(UpdateMusicVolume);
            _menu.AddMethod(UpdateSFXVolume);
            _menu.AddMethod(UpdateUIVolume);
            _menu.AddMethod(BackToMenu);
            _menu.Load(graphics, "SettingsMenuDefinition", "UITemplates");

            ModManager.Instance.SoundManager.PlaySound("MusicMenu", (int)SoundType.Music, true);

            var musicVolume = SettingsManager.Instance.GetSetting<float>("sound", "musicvolume");
            var sfxVolume = SettingsManager.Instance.GetSetting<float>("sound", "sfxvolume");
            var uiVolume = SettingsManager.Instance.GetSetting<float>("sound", "uivolume");

            _menu.GetWidget<PUIWHScrollBar>("scrlMusicVolume").FValue = musicVolume;
            _menu.GetWidget<PUIWHScrollBar>("scrlSFXVolume").FValue = sfxVolume;
            _menu.GetWidget<PUIWHScrollBar>("scrlUIVolume").FValue = uiVolume;
        }

        public override int Update(GameTime gameTime)
        {
            return (int)_nextState;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _menu.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void OnMouseMoved(Vector2 originalPosition, GameTime gameTime)
        {
            _menu.OnMouseMoved(originalPosition, gameTime);
        }
        public override void OnMouseDown(MouseButtonID button, GameTime gameTime)
        {
            _menu.OnMouseDown(button, gameTime);
        }

        public override void OnMouseClicked(MouseButtonID button, GameTime gameTime)
        {
            _menu.OnMouseClicked(button, gameTime);
        }

        public override void OnMouseScroll(MouseScrollDirection direction, int scrollValue, GameTime gameTime)
        {
            _menu.OnMouseScroll(direction, scrollValue, gameTime);
        }

        public override void OnKeyPressed(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnKeyPressed(key, gameTime, currentKeyState);

            switch (key)
            {
                case Keys.Escape:
                case Keys.Space:
                case Keys.Enter:
                case Keys.Back:
                    {
                        _nextState = GameStateType.Menu;
                    }
                    break;
            }
        }

        public override void OnKeyReleased(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnKeyReleased(key, gameTime, currentKeyState);
        }

        public override void OnKeyDown(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnKeyDown(key, gameTime, currentKeyState);
        }

        public override void OnTextInput(TextInputEventArgs e, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnTextInput(e, gameTime, currentKeyState);
        }
    }
}
