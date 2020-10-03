using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using PandaMonogame.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Text;

namespace GameCore.Entities
{
    public class Enemy : Entity
    {

        public Enemy()
        {
            hp = 5;
            pos = new Vector2(0);
        }
        public Enemy(int new_hp, int new_posX, int new_posY)
        {
            hp = new_hp;
            pos = new Vector2(new_posX, new_posY);
        }

        public void Update(GameTime gameTime)
        {
            pos += vel;

        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (PlaceHolderTexture == null)
                PlaceholderTextureInit(graphics, Color.Red);
            
            if (!dead) base.Draw(gameTime, graphics, spriteBatch);
        }

        public void Kill(GameTime gameTime)
        {
            if (dead) return;
            vel.X = 0;
            vel.Y = 0;
            dead = true;
            ignore_collision = true;
        }
    }
}
