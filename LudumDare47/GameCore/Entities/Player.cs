using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using PandaMonogame.UI;
using GameCore.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace GameCore.Entities
{
    public class Player : Entity
    {

        public bool shooting = false;
        public AnimatedSprite Sprite;
        public int animation_int = 0;

        public Animation AnimIdleDown = new Animation(1, 4, 1000);
        public Animation AnimIdleUp = new Animation(9, 12, 1000);
        public Animation AnimIdleLeft = new Animation(17, 20, 1000);
        public Animation AnimIdleRight = new Animation(25, 28, 1000);

        public Animation AnimRunUp = new Animation(33, 40, 1000);
        public Animation AnimRunDown = new Animation(41, 48, 1000);
        public Animation AnimRunLeft = new Animation(49, 56, 1000);
        public Animation AnimRunRight = new Animation(57, 64, 1000);

        public Player()
        {
            draw_width = 32;
            draw_height = 32;
            speed = 100;
            hp = 3;
        }

        public void StartMoving(Directions direction)
        {
            facing = direction;

            switch (facing)
            {
                case Directions.Up:
                    {
                        vel.Y--;
                        Sprite.PlayAnimation(AnimRunUp);
                    }
                    break;

                case Directions.Down:
                    {
                        vel.Y++;
                        Sprite.PlayAnimation(AnimRunDown);
                    }
                    break;

                case Directions.Left:
                    {
                        vel.X--;
                        Sprite.PlayAnimation(AnimRunLeft);
                    }
                    break;

                case Directions.Right:
                    {
                        vel.X++;
                        Sprite.PlayAnimation(AnimRunRight);
                    }
                    break;
            }
        }

        //public void ResetMoving()
        //{
        //    vel = Vector2.Zero;
        //    PlayIdle();
        //    facing = Directions.None;
        //}

        public void StopMoving(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    {
                        vel.Y++;
                    }
                    break;

                case Directions.Down:
                    {
                        vel.Y--;
                    }
                    break;

                case Directions.Left:
                    {
                        vel.X++;
                    }
                    break;

                case Directions.Right:
                    {
                        vel.X--;
                    }
                    break;
            }

            if (vel == Vector2.Zero)
            {
                PlayIdle();
            }
        }

        protected void PlayIdle()
        {
            switch (facing)
            {
                case Directions.Up:
                    {
                        Sprite.PlayAnimation(AnimIdleUp);
                    }
                    break;

                case Directions.Down:
                    {
                        Sprite.PlayAnimation(AnimIdleDown);
                    }
                    break;

                case Directions.Left:
                    {
                        Sprite.PlayAnimation(AnimIdleLeft);
                    }
                    break;

                case Directions.Right:
                    {
                        Sprite.PlayAnimation(AnimIdleRight);
                    }
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            pos += vel * speed * gameTime.DeltaTime();

            Sprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, pos);
        }
    }
}
