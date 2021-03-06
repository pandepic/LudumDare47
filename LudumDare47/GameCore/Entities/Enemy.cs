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
    public class Enemy : Entity
    {
        public EnemyType enemyType = EnemyType.Null;
        public int range;
        public int damage;
        public bool spawned = false;
        public bool spawnFinished = false;

        public AnimatedSprite ExplosionSprite;
        public AnimatedSprite SpawnEffectSprite;
        public float SpawnEffectDuration;

        public float AnimExplosionDuration;
        public Vector2 ExplosionPos;

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

                case EnemyType.Cyborg:
                    {
                        Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Robot"), 32, 32);
                        ExplosionSprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "LightningExplosion"), 32, 32);
                    }
                    break;

                case EnemyType.CaveBorg:
                    {
                        Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "CavemanCyborg"), 32, 48);
                        ExplosionSprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "LightningExplosion"), 32, 32);
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

                case EnemyType.Cyborg:
                    {
                        AnimIdleUp = new Animation(1, 1, 500);
                        AnimIdleDown = new Animation(9, 9, 500);
                        AnimIdleRight = new Animation(17, 17, 500);
                        AnimIdleLeft = new Animation(25, 25, 500);
                        AnimRunUp = new Animation(1, 8, 500);
                        AnimRunDown = new Animation(9, 16, 500);
                        AnimRunRight = new Animation(17, 24, 500);
                        AnimRunLeft = new Animation(25, 32, 500);

                        AnimLightningExplosion = new Animation(1, 8, 500);
                    }
                    break;

                case EnemyType.CaveBorg:
                    {
                        AnimIdleUp = new Animation(1, 1, 500);
                        AnimIdleDown = new Animation(9, 9, 500);
                        AnimIdleRight = new Animation(17, 17, 500);
                        AnimIdleLeft = new Animation(25, 25, 500);
                        AnimRunUp = new Animation(1, 8, 500);
                        AnimRunDown = new Animation(9, 16, 500);
                        AnimRunRight = new Animation(17, 24, 500);
                        AnimRunLeft = new Animation(25, 32, 500);

                        AnimLightningExplosion = new Animation(1, 8, 500);
                    }
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            AnimExplosionDuration -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (AnimExplosionDuration < 0)
                AnimExplosionDuration = 0;

            Sprite.Update(gameTime);
            ExplosionSprite?.Update(gameTime);

            if (dead)
                return;

            UpdateMoveVec();
            attack_cooldown -= gameTime.DeltaTime() * 1000;

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

            var offset = Vector2.Zero;
            if (enemyType == EnemyType.Cyborg)
                offset = new Vector2(0, 0);
            else if (enemyType == EnemyType.CaveBorg)
                offset = new Vector2(0, 13);

            if (AnimExplosionDuration > 0)
                ExplosionSprite?.Draw(spriteBatch, pos + offset);
        }

        public void Kill(GameTime gameTime)
        {
            if (dead) return;
            vel.X = 0;
            vel.Y = 0;
            dead = true;
            ignore_collision = true;

            Sprite.BeginFadeEffect(0f, 1000f);
        }
    }
}
