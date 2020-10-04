using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.Entities
{
    public class Clutter : Entity
    {
        public bool shootable = false;
        public bool button = false;
        public Door door_unlock = new Door();
        public bool collectable = false;
        public int keyid = 0;

        public void update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            base.Draw(gameTime, spriteBatch, Color.DarkGray);
        }

        public void Kill(GameTime gameTime)
        {
            ignore_collision = true;
            draw_width = 0;
        }
        
        public Clutter ShallowCopy()
        {
            return (Clutter)this.MemberwiseClone();
        }
    }

}
