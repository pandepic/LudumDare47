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
                var attack = new Bullet(BulletType.MeleeSwing, enemy.facing);
                attack.speed = 0;
                enemy.attack_cooldown = 2000;
                attack.duration = 1000;
                attack.damage = 1;

                if (enemy.enemyType == EnemyType.Caveman)
                {
                    if (enemy.facing == Directions.Up)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeUp, 1);
                    if (enemy.facing == Directions.Down)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeDown, 1);
                    if (enemy.facing == Directions.Left)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeLeft, 1);
                    if (enemy.facing == Directions.Right)
                        enemy.Sprite.PlayAnimation(enemy.AnimMeleeRight, 1);
                }

                // Figure out the direction of the attack
                if (Vector2.Distance(player.Centre(), enemy.Centre()) < 20)
                {
                    attack.col_width = attack.draw_width = 30;
                    attack.col_height = attack.draw_height = 30;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, 0));
                }
                else if (move_vector.X > Math.Abs(move_vector.Y) && move_vector.X >= 0)
                {
                    attack.col_width = attack.draw_width = 20;
                    attack.col_height = attack.draw_height = 60;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(35, 0));
                }
                else if (Math.Abs(move_vector.X) > Math.Abs(Math.Abs(move_vector.Y)) && move_vector.X <= 0)
                {
                    attack.col_width = attack.draw_width = 20;
                    attack.col_height = attack.draw_height = 60;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(-35, 0));
                }
                else if (Math.Abs(move_vector.X) <= Math.Abs(move_vector.Y) && move_vector.Y <= 0)
                {
                    attack.col_width = attack.draw_width = 60;
                    attack.col_height = attack.draw_height = 20;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, -35));
                }
                else if (Math.Abs(move_vector.X) <= move_vector.Y && move_vector.Y > 0)
                {
                    attack.col_width = attack.draw_width = 60;
                    attack.col_height = attack.draw_height = 20;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, 35));
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
        }
    }
}
