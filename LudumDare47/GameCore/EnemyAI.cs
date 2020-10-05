using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using GameCore.Entities;

namespace GameCore
{
    public class EnemyAI
    {
        public static void CaveManAI(Enemy enemy, Player player, Room room, List<Bullet> bullets, GameTime gameTime)
        {
            var move_vector = player.Centre() - enemy.Centre();

            // Attack if able, the 'swing' is a bullet
            if (enemy.attack_cooldown <= 0 && Vector2.Distance(player.Centre(), enemy.Centre()) < enemy.range)
            {
                Bullet attack;
                bool ranged = false;
                Directions bulletDirection = Directions.None;

                if (enemy.enemyType == EnemyType.Caveman)
                {
                    attack = new Bullet(BulletType.MeleeSwing, enemy.facing);
                    attack.speed = 0;
                    enemy.attack_cooldown = 2000;
                    attack.duration = 1000;
                    attack.damage = 1;

                    if (enemy.facing == Directions.Up)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeUp, 1);
                    if (enemy.facing == Directions.Down)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeDown, 1);
                    if (enemy.facing == Directions.Left)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeLeft, 1);
                    if (enemy.facing == Directions.Right)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeRight, 1);
                }
                else
                {
                    attack = new Bullet(BulletType.Cyborg, enemy.facing);
                    attack.speed = 250;
                    enemy.attack_cooldown = 2000;
                    attack.duration = 1000;
                    attack.damage = 1;

                    ranged = true;
                }

                // Figure out the direction of the attack
                Vector2 offset = Vector2.Zero;
                if (Vector2.Distance(player.Centre(), enemy.Centre()) < 20)
                {
                    attack.col_width = attack.draw_width = 30;
                    attack.col_height = attack.draw_height = 30;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, 0));
                    if (ranged) attack.SetPosCentre(enemy.Centre() + new Vector2(0, 10));

                    if (ranged)
                        attack.vel.X++;
                }
                else if (move_vector.X > Math.Abs(move_vector.Y) && move_vector.X >= 0)
                {
                    offset = new Vector2(-5, 14);
                    if (ranged) offset = Vector2.Zero;
                    attack.col_width = attack.draw_width = 20;
                    attack.col_height = attack.draw_height = 60;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(35, 0));
                    if (ranged) attack.SetPosCentre(enemy.Centre() + new Vector2(0, 10));
                    attack.pos += offset;
                    attack.collision_offset = -1 * offset;

                    if (ranged)
                        attack.vel.X++;
                }
                else if (Math.Abs(move_vector.X) > Math.Abs(Math.Abs(move_vector.Y)) && move_vector.X <= 0)
                {
                    offset = new Vector2(5, 14);
                    if (ranged) offset = Vector2.Zero;
                    attack.col_width = attack.draw_width = 20;
                    attack.col_height = attack.draw_height = 60;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(-35, 0));
                    if (ranged) attack.SetPosCentre(enemy.Centre() + new Vector2(0, 10));
                    attack.pos += offset;
                    attack.collision_offset = -1 * offset;

                    if (ranged)
                        attack.vel.X--;
                }
                else if (Math.Abs(move_vector.X) <= Math.Abs(move_vector.Y) && move_vector.Y <= 0)
                {
                    offset = new Vector2(14, 5);
                    if (ranged) offset = Vector2.Zero;
                    attack.col_width = attack.draw_width = 60;
                    attack.col_height = attack.draw_height = 20;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, -35));
                    if (ranged) attack.SetPosCentre(enemy.Centre() + new Vector2(0, 10));
                    attack.pos += offset;
                    attack.collision_offset = -1 * offset;

                    if (ranged)
                        attack.vel.Y--;
                }
                else if (Math.Abs(move_vector.X) <= move_vector.Y && move_vector.Y > 0)
                {
                    offset = new Vector2(14, -5);
                    if (ranged) offset = Vector2.Zero;
                    attack.col_width = attack.draw_width = 60;
                    attack.col_height = attack.draw_height = 20;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, 35));
                    if (ranged) attack.SetPosCentre(enemy.Centre() + new Vector2(0, 10));
                    attack.pos += offset;
                    attack.collision_offset = -1 * offset;

                    if (ranged)
                        attack.vel.Y++;
                }

                if (ranged)
                {
                    enemy.ExplosionSprite.PlayAnimation(enemy.AnimLightningExplosion, 1);
                    enemy.AnimExplosionDuration = enemy.AnimLightningExplosion.Duration;
                    enemy.ExplosionPos = attack.Centre();
                }

                bullets.Add(attack);
            }

            // Move towards player, unless too close or attack is on cooldown
            //enemy.vel = new Vector2(0);

            var playerCenter = player.Centre();
            var enemyCenter = enemy.Centre();
            int xDiff = (int)(playerCenter.X - enemyCenter.X);
            var yDiff = (int)(playerCenter.Y - enemyCenter.Y);

            if (Vector2.Distance(player.pos, enemy.pos) < enemy.range / 2 || enemy.attack_cooldown > 0)
            {
                enemy.vel = new Vector2(0);
                enemy.moveup = false;
                enemy.movedown = false;
                enemy.moveleft = false;
                enemy.moveright = false;
            }
            else
            {
                enemy.moveup = false;
                enemy.movedown = false;
                enemy.moveleft = false;
                enemy.moveright = false;

                if (xDiff > 0)
                {
                    enemy.moveright = true;
                }
                else if (xDiff < 0)
                {
                    enemy.moveleft = true;
                }
                if (yDiff > 0)
                {
                    enemy.movedown = true;
                }
                else if (yDiff < 0)
                {
                    enemy.moveup = true;
                }
            }
        } // CaveManAI
    }
}
