using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PandaMonogame;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.Entities
{
    public enum BulletType
    {
        None,
        PlayerBullet,
    }

    public class Bullet : Entity
    {
        public int damage = 1;
        public float duration = 10000;
        public BulletType Type { get; set; }

        public Animation PlayerBulletUp = new Animation(1, 2, 500);
        public Animation PlayerBulletDown = new Animation(3, 4, 500);
        public Animation PlayerBulletLeft = new Animation(5, 6, 500);
        public Animation PlayerBulletRight = new Animation(7, 8, 500);

        public Bullet(BulletType type = BulletType.None)
        {
            draw_height = 10;
            draw_width = 10;
            speed = 5;

            Type = type;

            SetSprite(Directions.None);
        }

        public Bullet(Player player, BulletType type = BulletType.PlayerBullet)
        {
            // Shoot the bullet from the player's location in the direction he is facing
            draw_height = 10;
            draw_width = 10;
            col_height = 10;
            col_width = 10;
            speed = 500;

            Type = type;
            SetSprite(player.facing);

            Shoot(player);
        }

        public void SetSprite(Directions direction)
        {
            if (Type == BulletType.PlayerBullet)
            {
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Bullet"), 16, 16);

                if (direction == Directions.Left)
                    Sprite.PlayAnimation(PlayerBulletLeft);
                else if (direction == Directions.Right)
                    Sprite.PlayAnimation(PlayerBulletRight);
                else if (direction == Directions.Up)
                    Sprite.PlayAnimation(PlayerBulletUp);
                else if (direction == Directions.Down)
                    Sprite.PlayAnimation(PlayerBulletDown);
            }
        }

        public void Shoot(Player player)
        {
            SetPosCentre(player.Centre());
            vel = new Vector2(0);
            var playerFacing = player.facing;
            if (playerFacing == Directions.None)
                playerFacing = Directions.Left;

            if (playerFacing == Directions.Up)
            {
                vel.Y--;
                facing = Directions.Up;
            }
            if (playerFacing == Directions.Down)
            {
                vel.Y++;
                facing = Directions.Down;
            }
            if (playerFacing == Directions.Left)
            {
                vel.X--;
                facing = Directions.Left;
            }
            if (playerFacing == Directions.Right)
            {
                vel.X++;
                facing = Directions.Right;
            }
        }

        public void Update(GameTime gameTime)
        {
            pos += vel * speed * gameTime.DeltaTime();
            duration -= gameTime.DeltaTime() * 1000;
            if (duration <= 0)
            {
                dead = true;
                vel = new Vector2(0);
                ignore_collision = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            if (!dead)
                base.Draw(gameTime, spriteBatch, Color.White);
        }
        public void Kill(GameTime gameTime)
        {
            dead = true;
            vel = new Vector2(0);
            ignore_collision = true;
        }
    }

}
