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
    public class Enemy : Entity
    {
        public EnemyType enemyType = EnemyType.Null;
        public int range;
        public int damage;
        public bool spawned = false;
        public bool spawnFinished = false;

        public AnimatedSprite SpawnEffectSprite;
        public float SpawnEffectDuration;

        public Enemy()
        {
            hp = 5;
            pos = new Vector2(0);
        }

        public Enemy(int new_hp, int new_posX, int new_posY)
        {
            hp = new_hp;
            pos = new Vector2(new_posX, new_posY);
        }

        public void SetSprite()
        {
            switch (enemyType)
            {
                case EnemyType.Caveman:
                    {
                        Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Caveman"), 32, 32);
                    }
                    break;
            }
        }

        public void SetAnimations()
        {
            switch (enemyType)
            {
                case EnemyType.Caveman:
                    {
                        AnimIdleUp = new Animation(1, 4, 1000);
                        AnimIdleDown = new Animation(5, 8, 1000);
                        AnimIdleRight = new Animation(9, 12, 1000);
                        AnimIdleLeft = new Animation(13, 16, 1000);
                        AnimRunUp = new Animation(1, 4, 1000);
                        AnimRunDown = new Animation(5, 8, 1000);
                        AnimRunLeft = new Animation(9, 12, 1000);
                        AnimRunRight = new Animation(13, 16, 1000);

                        AnimMeleeUp = new Animation(17, 20, 1000);
                        AnimMeleeDown = new Animation(21, 24, 1000);
                        AnimMeleeLeft = new Animation(25, 28, 1000);
                        AnimMeleeRight = new Animation(29, 32, 1000);
                    }
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);

            if (dead)
                return;

            UpdateMoveVec();
            attack_cooldown -= gameTime.DeltaTime() * 1000;
            pos += speed * vel * gameTime.DeltaTime();

            if (spawned && !spawnFinished)
            {
                SpawnEffectDuration -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (SpawnEffectDuration > 0)
                    SpawnEffectSprite.Update(gameTime);
                else
                {
                    spawnFinished = true;
                    active = true;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            if (!spawned)
                return;

            if (!spawnFinished)
            {
                SpawnEffectSprite.Draw(spriteBatch, pos - new Vector2(draw_width / 2, draw_height / 2));
                return;
            }

            base.Draw(gameTime, spriteBatch, Color.White);
            //if (!dead) base.Draw(gameTime, spriteBatch, Color.Red);
        }

        public void Kill(GameTime gameTime)
        {
            if (dead) return;
            vel.X = 0;
            vel.Y = 0;
            dead = true;
            ignore_collision = true;

            Sprite.BeginFadeEffect(0f, 2000f);
        }
    }
}
