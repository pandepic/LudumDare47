using GameCore;
using GameCore.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.Entities
{
    public class Door : Entity
    {
        public int next_room_id;
        public int next_posX;
        public int next_posY;
        public bool locked = false;
        public bool unlocking = false;
        public int unlock_id = -1;
        public float open_time = 0.0f;
        public float open_timer = 0.0f;
        public bool draw_if_unlocked = true;
        public bool animated = false;
        public bool anim_is_fade = true;
        public Animation lockAnimation;
        public Animation unlockAnimation;
        public Animation openAnimation;
        public Animation closedAnimation;

        public Door()
        {

        }

        public Door(int new_next_room_id, int new_posX, int new_posY, int new_next_posX = 0, int new_next_posY = 0, bool is_locked = false)
        {
            next_room_id = new_next_room_id;
            pos.X = new_posX;
            pos.Y = new_posY;

            next_posX = new_next_posX;
            next_posY = new_next_posY;
            locked = is_locked;
        }

        public void Update(GameTime gameTime)
        {

            // Doors don't unlock until the opening animation plays, the duration is open_time
            if (unlocking)
            {
                open_timer += gameTime.DeltaTime();
                if(open_timer >= open_time)
                {
                    locked = false;
                    unlocking = false;
                    open_timer = 0.0f;
                    if (!draw_if_unlocked)
                    {
                        draw = false;
                    }
                    
                }
            }

            if (animated)
                Sprite.Update(gameTime);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            base.Draw(gameTime, spriteBatch, Color.Blue);
        }

        public void Unlock()
        {
            unlocking = true;
            if (anim_is_fade && animated)
            {
                Sprite.BeginFadeEffect(0, 1000);
            }
            else if(animated)
            {
                Sprite.PlayAnimation(unlockAnimation, 1);
            }
            //play open animation
        }

        public void Lock()
        {
            locked = true;
            if (anim_is_fade && animated)
            {
                Sprite.BeginFadeEffect(1, 1000);
            }
            else if (animated)
            {
                Sprite.PlayAnimation(lockAnimation, 1);
            }
        }
    }
}
