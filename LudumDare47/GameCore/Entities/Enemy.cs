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

        public void Update(GameTime gameTime)
        {
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

            if (!dead) base.Draw(gameTime, spriteBatch, Color.Red);
        }

        public void Kill(GameTime gameTime)
        {
            if (dead) return;
            vel.X = 0;
            vel.Y = 0;
            dead = true;
            ignore_collision = true;
        }
    }
}
