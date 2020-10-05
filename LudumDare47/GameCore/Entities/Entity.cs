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
    public class Entity
    {
        public AnimatedSprite Sprite;

        public int hp = 3;
        public Vector2 pos = new Vector2(0);
        public int draw_width = 32;
        public int draw_height = 32;
        public int col_width = 32;
        public int col_height = 32;
        public Vector2 vel = new Vector2(0);
        public int speed = 5;
        public bool moveup;
        public bool movedown;
        public bool moveleft;
        public bool moveright;
        public bool ignore_collision = false;
        public bool invulnerable = false;
        public bool dead = false;
        public bool draw = true;
        public Directions facing = Directions.Down;
        public Directions moving = Directions.None;
        public Texture2D draw_texture;
        public double animation_timer;
        public double animation_speed;
        public float attack_cooldown = 0;
        public Vector2 collision_offset = new Vector2(0);
        public AnimationState animationState = AnimationState.Idle;

        public bool floor_collision = false;

        public Animation AnimIdleDown;
        public Animation AnimIdleUp;
        public Animation AnimIdleLeft;
        public Animation AnimIdleRight;

        public Animation AnimRunUp;
        public Animation AnimRunDown;
        public Animation AnimRunLeft;
        public Animation AnimRunRight;

        /// <summary>
        /// If this is false ignore collisions etc.
        /// </summary>
        public bool active = false;

        public Vector2 Centre()
        {
            return new Vector2(pos.X + draw_width / 2, pos.Y + draw_height / 2);
        }

        public void SetPos(int x, int y)
        {
            pos = new Vector2(x, y);
        }

        public void SetPos(Vector2 xy)
        {
            pos = xy;
        }

        public void SetPosCentre(int x, int y)
        {
            pos = new Vector2(x - draw_width / 2, y - draw_height / 2);
        }

        public void SetPosCentre(Vector2 xy)
        {
            pos = new Vector2((int)xy.X - draw_width / 2, (int)xy.Y - draw_height / 2);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            if (!draw) return;

            if (Sprite == null)
            {
                if (draw_texture == null) draw_texture = Globals.PlaceholderTexture;
                spriteBatch.Draw(
                        draw_texture,
                        pos,
                        new Rectangle(0, 0, draw_width, draw_height),
                        color,
                        MathHelper.ToRadians(0.0f),
                        new Vector2(0),
                        new Vector2(1),
                        SpriteEffects.None,
                        0.0f
                        );
            }
            else
            {
                Sprite.Draw(spriteBatch, pos);
            }
        }

        public static bool Collision(Entity a, Entity b, bool use_ignore_collisions = true)
        {
            Vector2 c;
            Vector2 d;
            // !Must put player as a if using floor collision!

            if (!a.floor_collision && !b.floor_collision)
            {
                c = a.pos + a.collision_offset;
                d = b.pos + b.collision_offset;
                if (use_ignore_collisions && (a.ignore_collision || b.ignore_collision))
                    return false;
                return (!((c.X > d.X + b.col_width) || (c.X + a.col_width < d.X)) && !((c.Y > d.Y + b.col_height) || (c.Y + a.col_height < d.Y)));
            }
            else
            {
                c.X = a.pos.X + a.collision_offset.X;
                c.Y = a.pos.Y + a.draw_height;
                d = b.pos + b.collision_offset;
                if (use_ignore_collisions && (a.ignore_collision || b.ignore_collision))
                    return false;
                return (!((c.X > d.X + b.col_width) || (c.X + a.col_width < d.X)) && !((c.Y > d.Y + b.col_height) || (c.Y + 1 < d.Y)));

            }
        }

        public void StartMoving(Directions direction)
        {
            // Dead things can't move
            if (dead) return;

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
    }
}
