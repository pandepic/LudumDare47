using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.Entities
{
    public class Objects : Entity
    {
        
        public void update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, graphics, spriteBatch);
            if (PlaceHolderTexture == null)
                PlaceholderTextureInit(graphics, Color.Red);
        }
    }

}
