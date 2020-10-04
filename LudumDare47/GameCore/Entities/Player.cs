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
        public Animation AnimIdleUp = new Animation(5, 8, 1000);
        public Animation AnimIdleLeft = new Animation(9, 12, 1000);
        public Animation AnimIdleRight = new Animation(13, 16, 1000);

        public Player()
        {
            draw_width = 32;
            draw_height = 32;
            speed = 100;
            hp = 3;
        }

        public void Update(GameTime gameTime)
        {
            var prevFacing = facing;
            // Basic movement
            vel = new Vector2(0);
            if (moveup)
            {
                vel.Y--;
                facing = Directions.Up;

                if (prevFacing != facing)
                    Sprite.PlayAnimation(AnimIdleUp);
            }
            if (movedown)
            {
                vel.Y++;
                facing = Directions.Down;

                if (prevFacing != facing)
                    Sprite.PlayAnimation(AnimIdleDown);
            }
            if (moveleft)
            {
                vel.X--;
                facing = Directions.Left;

                if (prevFacing != facing)
                    Sprite.PlayAnimation(AnimIdleLeft);
            }
            if (moveright)
            {
                vel.X++;
                facing = Directions.Right;

                if (prevFacing != facing)
                    Sprite.PlayAnimation(AnimIdleRight);
            }
            
            pos += vel * speed * gameTime.DeltaTime();

            Sprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (PlaceHolderTexture == null)
                PlaceholderTextureInit(graphics, Color.Green);

            Sprite.Draw(spriteBatch, pos);
        }
    }
}
