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
        public int hp = 3;
        public Vector2 pos = new Vector2(0);
        public int width = 32;
        public int height = 32;
        public Vector2 vel = new Vector2(0);
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
            return new Vector2(pos.X + width / 2, pos.Y + height / 2);
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
            pos = new Vector2(x - width / 2, y - height / 2);
        }

        public void SetPosCentre(Vector2 xy)
        {
            pos = new Vector2((int)xy.X - width / 2, (int)xy.Y - height / 2);
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
                        pos,
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
            return (!((a.pos.X > b.pos.X + b.width) || (a.pos.X + a.width < b.pos.X)) && !((a.pos.Y > b.pos.Y + b.height) || (a.pos.Y + a.height < b.pos.Y)));
        }
    }
}
