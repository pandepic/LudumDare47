using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using PandaMonogame.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class MenuState : GameState
    {
        protected GameStateType _nextState = GameStateType.None;
        protected PUIMenu _menu = new PUIMenu();

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
        }

        public override int Update(GameTime gameTime)
        {
            return (int)GameStateType.GamePlay;
            //return (int)_nextState;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _menu.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
