﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PandaMonogame;
using PandaMonogame.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GameCore.Entities;
using System.Linq;
using Dcrew.Camera;

namespace GameCore
{
    public class GamePlayState : GameState
    {
        public const float PlayerAttackCooldown = 300.0f;

        protected GameStateType _nextState = GameStateType.None;
        protected PUIMenu _menu = new PUIMenu();
        protected Camera _camera = new Camera(new Vector2(160, 90), (320, 160));
        protected RenderTarget2D _gameTarget;
        protected bool isRipple = false;

        public List<Room> all_rooms;
        public Room current_room;
        public Player player;

        public List<Bullet> Bullets = new List<Bullet>();
        public List<Bullet> EnemyBullets = new List<Bullet>();

        public List<Clutter> Clutter = new List<Clutter>();

        readonly RoomMaps roomMaps = new RoomMaps();

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
            // Assets
            //playerIdle = ModManager.Instance.AssetManager.LoadTexture2D(graphics, "PlayerIdle");

            Globals.SpawnEffectTexture = ModManager.Instance.AssetManager.LoadTexture2D(graphics, "EnemySpawn");

            // Rooms
            roomMaps.LoadTileMaps(graphics);
            all_rooms = new List<Room>();
            foreach(var r in roomMaps.rooms)
            {
                all_rooms.Add(r);
            }
            current_room = Room.GetRoomByID(all_rooms, 100);
            SetRoom(current_room);

            // Load the player
            player = new Player();
            player.Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(graphics, "PlayerIdle"), 32, 32);
            player.facing = Directions.None;
            player.Sprite.PlayAnimation(player.AnimIdleLeft);
            player.SetPosCentre(280, 90);
            player.active = true;

            _menu.Load(graphics, "GameplayMenuDefinition", "UITemplates");

            _gameTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
        }

        public void SetRoom(Room room)
        {
            foreach (var e in room.enemies)
            {
                if (!e.spawned && !e.spawnFinished)
                {
                    e.spawned = true;
                    e.SpawnEffectDuration = Globals.SpawnEffectDuration;
                    e.SpawnEffectSprite = new AnimatedSprite(Globals.SpawnEffectTexture, 64, 64);
                    e.SpawnEffectSprite.PlayAnimation(Globals.SpawnEffectAnimation);
                }
            }
        }

        public override int Update(GameTime gameTime)
        {

            // Player
            player.Update(gameTime);
            Room.NudgeOOB(current_room, player);
            // Check for touching doors

            foreach (var d in current_room.doors)
            {
                if (Entity.Collision(player, d))
                {
                    if(d.locked == true)
                    {
                        player.pos -= player.vel;
                    }
                    else if (Room.GetRoomByID(all_rooms, d.next_room_id).room_id != -1)
                    {
                        // Transition to next room
                        Bullets.Clear();
                        current_room = Room.GetRoomByID(all_rooms, d.next_room_id);
                        player.SetPos(d.next_posX, d.next_posY);
                        //player.vel = new Vector2(0);
                        //player.ResetMoving();
                        SetRoom(current_room);

                        Console.WriteLine(current_room.room_id);
                    }
                }
            }


            foreach (var e in current_room.enemies)
            {
                if (!e.dead && e.spawnFinished)
                {
                    //Enemy AI
                    if (e.enemyType == EnemyType.Caveman) EnemyAI.CaveManAI(e, player, current_room, EnemyBullets, gameTime);
                }
            }

            // Rooms
            current_room.Update(gameTime);

            // Bullets
            foreach (var b in Bullets)
            {
                if (Room.CheckOOB(current_room, b))
                {
                    b.Kill(gameTime);
                }
                foreach (var e in current_room.enemies)
                {
                    if (!e.active)
                        continue;

                    if (Entity.Collision(e, b))
                    {
                        e.hp -= b.damage;
                        if (e.hp <= 0) e.Kill(gameTime);

                        b.Kill(gameTime);
                    }
                }
                b.Update(gameTime);
            }

            // Enemy bullets
            foreach (var b in EnemyBullets)
            {
                if (Entity.Collision(player, b))
                {
                    player.hp -= b.damage;
                    // If player.hp <= 0
                    b.ignore_collision = true;
                    Console.WriteLine(player.hp);
                }
                b.Update(gameTime);
            }

            player.attack_cooldown -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (player.attack_cooldown <= 0)
            {
                player.attack_cooldown = 0;

                if (player.shooting == true)
                {
                    player.attack_cooldown = PlayerAttackCooldown;
                    Bullets.Add(new Bullet(player));
                }
            }

            if (isRipple) {
                Globals.Ripple.Parameters["phase"].SetValue((float)gameTime.TotalGameTime.TotalMilliseconds / 100);
            }

            return (int)_nextState;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.SetRenderTarget(_gameTarget);
            graphics.Clear(Color.Black);

            // Determine draw order by sorting by y value + height
            List<Entity> DrawEntities = new List<Entity>();
            DrawEntities.Add(player);
            foreach (var e in current_room.enemies)
            {
                DrawEntities.Add(e);
            }
            foreach (var d in current_room.doors)
            {
                DrawEntities.Add(d);
            }
            DrawEntities.Sort(delegate (Entity a, Entity b)
            {
                if (a.pos.Y + a.draw_height > b.pos.Y + b.draw_height) return 1;
                else return -1;
            });

            // Draw
            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: _camera.View());
            current_room.Draw(gameTime, spriteBatch);
            foreach (var e in DrawEntities)
            {
                e.Draw(gameTime, spriteBatch, Color.White);
            }

            foreach (var b in Bullets)
            {
                b.Draw(gameTime, spriteBatch, Color.White);
            }
            foreach (var b in EnemyBullets)
            {
                b.Draw(gameTime, spriteBatch, Color.White);
            }
            spriteBatch.End();

            graphics.SetRenderTarget(null);

            if (isRipple) {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, effect: Globals.Ripple);
                spriteBatch.Draw(_gameTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            } else {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
                spriteBatch.Draw(_gameTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _menu.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void OnKeyPressed(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnKeyPressed(key, gameTime, currentKeyState);

            if (key == Keys.W || key == Keys.Up)
            {
                player.StartMoving(Directions.Up);
            }
            else if (key == Keys.S || key == Keys.Down)
            {
                player.StartMoving(Directions.Down);
            }
            else if (key == Keys.A || key == Keys.Left)
            {
                player.StartMoving(Directions.Left);
            }
            else if (key == Keys.D || key == Keys.Right)
            {
                player.StartMoving(Directions.Right);
            }

            if (key == Keys.Space)
            {
                player.shooting = true;
            }

            if (key == Keys.F1) {
                isRipple = !isRipple;
            }
        }

        public override void OnMouseMoved(Vector2 originalPosition, GameTime gameTime)
        {
            _menu.OnMouseMoved(originalPosition, gameTime);
        }
        public override void OnMouseDown(MouseButtonID button, GameTime gameTime)
        {
            _menu.OnMouseDown(button, gameTime);
        }

        public override void OnMouseClicked(MouseButtonID button, GameTime gameTime)
        {
            _menu.OnMouseClicked(button, gameTime);
        }

        public override void OnMouseScroll(MouseScrollDirection direction, int scrollValue, GameTime gameTime)
        {
            _menu.OnMouseScroll(direction, scrollValue, gameTime);
        }

        public override void OnKeyReleased(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            if (key == Keys.W || key == Keys.Up)
            {
                player.StopMoving(Directions.Up);
            }
            else if (key == Keys.S || key == Keys.Down)
            {
                player.StopMoving(Directions.Down);
            }
            else if (key == Keys.A || key == Keys.Left)
            {
                player.StopMoving(Directions.Left);
            }
            else if (key == Keys.D || key == Keys.Right)
            {
                player.StopMoving(Directions.Right);
            }

            if (key == Keys.Space)
            {
                player.shooting = false;
            }

            _menu.OnKeyReleased(key, gameTime, currentKeyState);
        }

        public override void OnKeyDown(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnKeyDown(key, gameTime, currentKeyState);
        }

        public override void OnTextInput(TextInputEventArgs e, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnTextInput(e, gameTime, currentKeyState);
        }
    }
}
