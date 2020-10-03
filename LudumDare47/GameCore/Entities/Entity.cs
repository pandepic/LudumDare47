﻿using Microsoft.Xna.Framework;
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
        public int hp = 3;
        public int posX = 0;
        public int posY = 0;
        public int width = 32;
        public int height = 32;
        public int velX = 0;
        public int velY = 0;
        public int speed = 5;
        public bool moveup;
        public bool movedown;
        public bool moveleft;
        public bool moveright;
        public Texture2D PlaceHolderTexture = null;
        public bool ignore_collision = false;
        public bool invulnerable = false;
        public bool dead = false;
        public Directions facing = Directions.Down;
        public Texture2D draw_texture;
        public double animation_timer;
        public double animation_speed;

        public Vector2 Centre()
        {
            return new Vector2(posX + width / 2, posY + height / 2);
        }

        public void SetPos(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void SetPos(Vector2 xy)
        {
            posX = (int)xy.X;
            posY = (int)xy.Y;
        }

        public void SetPosCentre(int x, int y)
        {
            posX = x - width / 2;
            posY = y - height / 2;
        }

        public void SetPosCentre(Vector2 xy)
        {
            posX = (int)xy.X - width / 2;
            posY = (int)xy.Y - height / 2;
        }

        public void PlaceholderTextureInit(GraphicsDevice graphics, Color pcolor)
        {
            if (PlaceHolderTexture == null)
            {
                PlaceHolderTexture = new RenderTarget2D(graphics, 1, 1);
                graphics.SetRenderTarget((RenderTarget2D)PlaceHolderTexture);
                graphics.Clear(pcolor);
                graphics.SetRenderTarget(null);
            }
        }


        public virtual void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (draw_texture == null) draw_texture = PlaceHolderTexture;
            if (PlaceHolderTexture != null)
            {
                
                spriteBatch.Draw(
                        draw_texture,
                        new Vector2(posX, posY),
                        new Rectangle(0, 0, width, height),
                        Color.White,
                        MathHelper.ToRadians(0.0f),
                        new Vector2(0),
                        new Vector2(1),
                        SpriteEffects.None,
                        0.0f
                        );
            }
        }

        public static bool Collision(Entity a, Entity b, bool use_ignore_collisions = true)
        {
            if (use_ignore_collisions && (a.ignore_collision || b.ignore_collision)) 
                return false;
            return (!((a.posX > b.posX + b.width) || (a.posX + a.width < b.posX)) && !((a.posY > b.posY + b.height) || (a.posY + a.height < b.posY)));
        }
    }
}
