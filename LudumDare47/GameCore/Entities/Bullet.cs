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
        public float duration = 10000;
        public Bullet()
        {
            draw_height = 10;
            draw_width = 10;
            speed = 10;
        }

        public Bullet(Player player)
        {
            // Shoot the bullet from the player's location in the direction he is facing
            draw_height = 10;
            draw_width = 10;
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
        }

        public void Update(GameTime gameTime)
        {
            pos += vel * speed * gameTime.DeltaTime();
            duration -= gameTime.DeltaTime() * 1000;
            if (duration <= 0)
            {
                dead = true;
                vel = new Vector2(0);
                ignore_collision = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            if (!dead)
                base.Draw(gameTime, spriteBatch, Color.White);
        }
        public void Kill(GameTime gameTime)
        {
            dead = true;
            vel = new Vector2(0);
            ignore_collision = true;
        }
    }

}
