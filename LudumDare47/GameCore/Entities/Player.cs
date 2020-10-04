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
        public int animation_int = 0;
        public List<Clutter> inventory = new List<Clutter>();

        public Player()
        {
            draw_width = 32;
            draw_height = 32;
            speed = 100;
            hp = 3;
            collision_offset = new Vector2(8,17);
            col_width = 15;
            col_height = 15;
        }

        public void SetAnimations()
        {
            AnimIdleDown = new Animation(1, 4, 1000);
            AnimIdleUp = new Animation(9, 12, 1000);
            AnimIdleLeft = new Animation(17, 20, 1000);
            AnimIdleRight = new Animation(25, 28, 1000);
            AnimRunUp = new Animation(33, 40, 1000);
            AnimRunDown = new Animation(41, 48, 1000);
            AnimRunLeft = new Animation(49, 56, 1000);
            AnimRunRight = new Animation(57, 64, 1000);
        }

        public void Update(GameTime gameTime)
        {
            UpdateMoveVec();
            pos += vel * speed * gameTime.DeltaTime();

            Sprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            base.Draw(gameTime, spriteBatch, color);
        }
    }
}
