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

        public Door()
        {
            
        }

        public Door(int new_next_room_id, int new_posX, int new_posY, int new_next_posX = 0, int new_next_posY = 0)
        {
            next_room_id = new_next_room_id;
            pos.X = new_posX;
            pos.Y = new_posY;

            next_posX = new_next_posX;
            next_posY = new_next_posY;
        }

        public void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            PlaceholderTextureInit(graphics, Color.Blue);
            base.Draw(gameTime, graphics, spriteBatch);
        }

        public void Unlock()
        {
            locked = false;
        }

        public void Lock()
        {
            locked = true;
        }
    }
}
