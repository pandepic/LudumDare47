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
        public Texture2D PlaceHolderTexture = null;
        public bool ignore_collision = false;
        public bool invulnerable = false;
        public bool dead = false;
        public Directions facing = Directions.Down;
        public Texture2D draw_texture;
        public double animation_timer;
        public double animation_speed;
        public float attack_cooldown = 0;
        public AnimationState animationState = AnimationState.Idle;

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
                        new Rectangle(0, 0, draw_width, draw_height),
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
            return (!((a.pos.X > b.pos.X + b.col_width) || (a.pos.X + a.col_width < b.pos.X)) && !((a.pos.Y > b.pos.Y + b.col_height) || (a.pos.Y + a.col_height < b.pos.Y)));
        }
    }
}
