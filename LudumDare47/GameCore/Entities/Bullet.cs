using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PandaMonogame;
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
            speed = 1000;
            Shoot(player);
        }

        public void Shoot(Player player)
        {
            SetPosCentre(player.Centre());
            vel = new Vector2(0);
            if (player.facing == Directions.Up)
            {
                vel.Y--;
                facing = Directions.Up;
            }
            if (player.facing == Directions.Down)
            {
                vel.Y++;
                facing = Directions.Down;
            }
            if (player.facing == Directions.Left)
            {
                vel.X--;
                facing = Directions.Left;
            }
            if (player.facing == Directions.Right)
            {
                vel.X++;
                facing = Directions.Right;
            }
            vel *= speed;
        }

        public void Update(GameTime gameTime)
        {
            pos += vel * gameTime.DeltaTime();
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
            vel = new Vector2(0);
            ignore_collision = true;
            dead = true;
        }
    }
        
}

