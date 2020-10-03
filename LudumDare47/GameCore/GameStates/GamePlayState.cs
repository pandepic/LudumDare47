using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class GamePlayState : GameState
    {
        protected GameStateType _nextState = GameStateType.None;

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
        }

        public override int Update(GameTime gameTime)
        {
            return (int)_nextState;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
        }
    }
}
