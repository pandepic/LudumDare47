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

        public SimpleStateMachine StateMachine = new SimpleStateMachine();

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
                        moveup = true;
                    }
                    break;

                case Directions.Down:
                    {
                        movedown = true;
                    }
                    break;

                case Directions.Left:
                    {
                        moveleft = true;
                    }
                    break;

                case Directions.Right:
                    {
                        moveright = true;
                    }
                    break;
            }
        }

        public void StopMoving(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    {
                        moveup = false;
                    }
                    break;

                case Directions.Down:
                    {
                        movedown = false;
                    }
                    break;

                case Directions.Left:
                    {
                        moveleft = false;
                    }
                    break;

                case Directions.Right:
                    {
                        moveright = false;
                    }
                    break;
            }
        }

        protected void UpdateMoveVec()
        {
            var prevVel = vel;
            vel = Vector2.Zero;
            var newFacing = Directions.None;

            if (moveup)
            {
                vel.Y = -1;
                newFacing = Directions.Up;
            }
            else if (movedown)
            {
                vel.Y = 1;
                newFacing = Directions.Down;
            }

            if (moveleft)
            {
                vel.X = -1;
                if (newFacing == Directions.None)
                    newFacing = Directions.Left;
            }
            else if (moveright)
            {
                vel.X = 1;
                if (newFacing == Directions.None)
                    newFacing = Directions.Right;
            }

            if (newFacing != Directions.None)
                facing = newFacing;

            if (prevVel != vel)
            {
                if (vel == Vector2.Zero)
                    PlayIdle();
                else
                    PlayMoving();
            }
        }

        protected void PlayMoving()
        {
            switch (facing)
            {
                case Directions.Up:
                    {
                        Sprite.PlayAnimation(AnimRunUp);
                    }
                    break;

                case Directions.Down:
                    {
                        Sprite.PlayAnimation(AnimRunDown);
                    }
                    break;

                case Directions.Left:
                    {
                        Sprite.PlayAnimation(AnimRunLeft);
                    }
                    break;

                case Directions.Right:
                    {
                        Sprite.PlayAnimation(AnimRunRight);
                    }
                    break;
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
            UpdateMoveVec();
            pos += vel * speed * gameTime.DeltaTime();

            Sprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, pos);
        }
    }
}
