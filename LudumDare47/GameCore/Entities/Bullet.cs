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
        MeleeSwing,
        Cyborg,
        CaveBorg,
    }

    public class Bullet : Entity
    {
        public int damage = 1;
        public float duration = 10000;
        public BulletType Type { get; set; }

        public Animation AnimPlayerBulletUp = new Animation(1, 2, 500);
        public Animation AnimPlayerBulletDown = new Animation(3, 4, 500);
        public Animation AnimPlayerBulletLeft = new Animation(5, 6, 500);
        public Animation AnimPlayerBulletRight = new Animation(7, 8, 500);

        public Animation AnimMeleeSwingUp = new Animation(1, 4, 500);
        public Animation AnimMeleeSwingDown = new Animation(5, 8, 500);
        public Animation AnimMeleeSwingLeft = new Animation(9, 12, 500);
        public Animation AnimMeleeSwingRight = new Animation(13, 16, 500);

        public Animation AnimLightningBullet = new Animation(1, 2, 200);

        public Bullet(BulletType type = BulletType.None, Directions direction = Directions.None)
        {
            draw_height = 10;
            draw_width = 10;
            speed = 5;

            Type = type;

            SetSprite(direction);
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
                    Sprite.PlayAnimation(AnimPlayerBulletLeft);
                else if (direction == Directions.Right)
                    Sprite.PlayAnimation(AnimPlayerBulletRight);
                else if (direction == Directions.Up)
                    Sprite.PlayAnimation(AnimPlayerBulletUp);
                else if (direction == Directions.Down)
                    Sprite.PlayAnimation(AnimPlayerBulletDown);
            }
            else if (Type == BulletType.MeleeSwing)
            {
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Hit"), 32, 32);

                if (direction == Directions.Left)
                    Sprite.PlayAnimation(AnimMeleeSwingLeft);
                else if (direction == Directions.Right)
                    Sprite.PlayAnimation(AnimMeleeSwingRight);
                else if (direction == Directions.Up)
                    Sprite.PlayAnimation(AnimMeleeSwingUp);
                else if (direction == Directions.Down)
                    Sprite.PlayAnimation(AnimMeleeSwingDown);
            }
            else if (Type == BulletType.Cyborg || Type == BulletType.CaveBorg)
            {
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "LightningBullet"), 16, 16);
                Sprite.PlayAnimation(AnimLightningBullet);
            }
        }

        public void Shoot(Entity entity)
        {

            vel = new Vector2(0);
            var playerFacing = entity.facing;
            Vector2 offset = new Vector2(0, 0);

            if (playerFacing == Directions.None)
                playerFacing = Directions.Left;

            if (playerFacing == Directions.Up)
            {
                offset = new Vector2(-3, -29);
            }
            if (playerFacing == Directions.Down)
            {
                offset = new Vector2(-1, 22);
            }
            if (playerFacing == Directions.Left)
            {
                offset = new Vector2(-30, 0);
            }
            if (playerFacing == Directions.Right)
            {
                offset = new Vector2(22, 1);
            }

            SetPosCentre(entity.Centre() + offset);
            
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

            Sprite.Update(gameTime);
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
