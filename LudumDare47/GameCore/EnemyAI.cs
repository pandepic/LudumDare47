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

            if (enemy.attack_cooldown <= 0 && Vector2.Distance(player.Centre(), enemy.Centre()) < enemy.range){
                var attack = new Bullet();
                attack.speed = 0;
                enemy.attack_cooldown = 2000;
                attack.duration = 1000;
                attack.damage = 1;

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
                    attack.SetPosCentre(enemy.Centre() + new Vector2(30, 0));
                }
                else if (Math.Abs(move_vector.X) > Math.Abs(Math.Abs(move_vector.Y)) && move_vector.X <= 0)
                {
                    attack.col_width = attack.draw_width = 20;
                    attack.col_height = attack.draw_height = 60;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(-30, 0));
                }
                else if (Math.Abs(move_vector.X) <= Math.Abs(move_vector.Y) && move_vector.Y <= 0)
                {
                    attack.col_width = attack.draw_width = 60;
                    attack.col_height = attack.draw_height = 20;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, -30));
                }
                else if (Math.Abs(move_vector.X) <= move_vector.Y && move_vector.Y > 0)
                {
                    attack.col_width = attack.draw_width = 60;
                    attack.col_height = attack.draw_height = 20;
                    attack.SetPosCentre(enemy.Centre() + new Vector2(0, 30));
                }


                bullets.Add(attack);
            }
            
            
            enemy.vel = new Vector2(0);
            if (move_vector.X > 0) enemy.vel.X = 1;
            if (move_vector.X < 0) enemy.vel.X = -1;
            if (move_vector.Y > 0) enemy.vel.Y = 1;
            if (move_vector.Y < 0) enemy.vel.Y = -1;

            if (Vector2.Distance(player.pos, enemy.pos) < enemy.range/2 || enemy.attack_cooldown > 0) enemy.vel = new Vector2(0);

            enemy.pos += enemy.speed * enemy.vel * gameTime.DeltaTime();
            enemy.attack_cooldown -= gameTime.DeltaTime() * 1000;
        }
    }
}
