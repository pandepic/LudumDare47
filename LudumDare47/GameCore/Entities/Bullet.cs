using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.Entities
{
    public class Bullet : Entity
    {
        public int damage = 1;

        public Bullet()
        {
            height = 10;
            width = 10;
            speed = 10;
        }

        public Bullet(Player player)
        {
            // Shoot the bullet from the player's location in the direction he is facing
            height = 10;
            width = 10;
            speed = 10;
            Shoot(player);
        }

        public void Shoot(Player player)
        {
            SetPosCentre(player.Centre());
            if (player.facing == Directions.Up)
            {
                velX = 0;
                velY = -speed;
            }
            else if (player.facing == Directions.Down)
            {
                velX = 0;
                velY = speed;
            }
            else if (player.facing == Directions.Left)
            {
                velX = -speed;
                velY = 0;
            }
            else if (player.facing == Directions.Right)
            {
                velX = speed;
                velY = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            posX += velX;
            posY += velY;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (PlaceHolderTexture == null) 
                PlaceholderTextureInit(graphics, Color.White);
            if (!dead) 
                base.Draw(gameTime, graphics, spriteBatch);
        }
        public void Kill(GameTime gameTime)
        {
            dead = true;
            velX = 0;
            velY = 0;
            ignore_collision = true;
            dead = true;
        }
    }
        
}

