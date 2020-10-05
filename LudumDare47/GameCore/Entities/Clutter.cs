using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using PandaMonogame;

namespace GameCore.Entities
{
    public class Clutter : Entity
    {
        public bool shootable = false;
        public bool button = false;
        public Door door_unlock = new Door();
        public bool collectable = false;
        public int keyid = 0;
        public bool floor_tile = false;
        public bool clock_repair = false;
        public int clock_handle = 0;
        public bool is_big_hand = false;
        public bool is_small_hand = false;

        public bool animated = false;

        public Animation idleAnimation = new Animation();
        public Animation destroyAnimation = new Animation();

        public void Update(GameTime gameTime)
        {
            if(animated)
                Sprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            base.Draw(gameTime, spriteBatch, Color.DarkGray);
        }

        public void Kill(GameTime gameTime)
        {
            ignore_collision = true;
            //draw_width = 0;
            if (!animated)
            {
                draw = false;
            }
            else
            {
                  Sprite.PlayAnimation(destroyAnimation, 1);
            }
        }
        
    }

}
