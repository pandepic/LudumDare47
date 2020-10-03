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
            posX = 0;
            posY = 0;
        }
        public Enemy(int new_hp, int new_posX, int new_posY)
        {
            hp = new_hp;
            posX = new_posX;
            posY = new_posY;
        }

        public void Update(GameTime gameTime)
        {
            
            posX += velX;
            posY += velY;

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
            velX = 0;
            velY = 0;
            dead = true;
            ignore_collision = true;
        }
    }
}
