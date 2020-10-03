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
        public Texture2D playerIdle;
        public int animation_int = 0;
        public Player()
        {
            width = 32;
            height = 32;
            speed = 1;
            hp = 3;
        }

        public void Update(GameTime gameTime)
        {
            // Basic movement
            if (moveup && !movedown)
            {
                facing = Directions.Up;
                velY = -1 * speed;
            }
            if (!moveup && movedown)
            {
                facing = Directions.Down;
                velY = speed;
            }
            if (!(moveup ^ movedown))
            {
                velY = 0;
            }
            if (moveleft && !moveright) 
            {
                facing = Directions.Left;
                velX = -1 * speed;
            }
            if (!moveleft && moveright) 
            {
                facing = Directions.Right;
                velX = 1 * speed; 
            }
            if (!(moveleft ^ moveright)) 
            { 
                velX = 0; 
            }

            posX += velX;
            posY += velY;

        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {

            // Idle vs running textures
            draw_texture = playerIdle;

            // Used to track which of the 4 animation textures to use from the sprite
            
            animation_speed = (double)500; //ms per frame
            int max_frames = 3;
            if (gameTime.TotalGameTime.TotalMilliseconds - animation_timer > animation_speed)
            {
                animation_timer = gameTime.TotalGameTime.TotalMilliseconds;
                animation_int = animation_int + 1;
            }

            if (animation_int > max_frames)
                animation_int = 0;            
            

            int face_int = 0;
            if (facing == Directions.Down)
            {
                face_int = 0;
            }
            else if (facing == Directions.Up)
            {
                face_int = 1;
            }
            else if (facing == Directions.Left)
            {
                face_int = 2;
            }
            else if (facing == Directions.Right)
            {
                face_int = 3;
            }
            

            if (PlaceHolderTexture == null)
                PlaceholderTextureInit(graphics, Color.Green);
            spriteBatch.Draw(
                        draw_texture,
                        new Vector2(posX, posY),
                        new Rectangle(animation_int * width, face_int * height, width, height),
                        Color.White,
                        MathHelper.ToRadians(0.0f),
                        new Vector2(0),
                        new Vector2(1),
                        SpriteEffects.None,
                        0.0f
                        ) ;
        }
    }
}
