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
using SpriteFontPlus;

namespace GameCore
{
    public class GamePlayState : GameState
    {
        protected PUIMenu _menu = new PUIMenu();
        protected PUIWLabel _timerLabel;

        public const float PlayerAttackCooldown = 300.0f;

        protected GameStateType _nextState = GameStateType.None;
        protected Camera _camera = new Camera(new Vector2(160, 90), (320, 160));
        protected RenderTarget2D _gameTarget;
        protected RenderTarget2D _rippleTarget;
        protected RenderTarget2D _windTarget;
        protected RenderTarget2D _deathTarget;
        protected RenderTarget2D _finalTarget;
        protected bool isRipple = false;

        public List<Room> all_rooms;
        public Room current_room;
        public Player player;

        public List<Bullet> Bullets = new List<Bullet>();
        public List<Bullet> EnemyBullets = new List<Bullet>();

        public List<Clutter> Clutter = new List<Clutter>();

        public List<DeathEffect> DeathEffects = new List<DeathEffect>();

        public Clutter popup_e;
        public Clutter lowerUI;

        public RoomMaps roomMaps = new RoomMaps();

        public float countdown = Globals.countdown_time;
        public float ripple_timer = 0.0f;
        public float respawn_timer = 0.0f;

        public bool respawning = false;

        public bool clock_repair_big = false;
        public bool clock_repair_small = false;

        public Clutter heart;

        public float winning_timer = 5;

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
            // Assets
            Globals.SpawnEffectTexture = ModManager.Instance.AssetManager.LoadTexture2D(graphics, "EnemySpawn");

            // A letter E that pops up when you are near a button
            popup_e = new Clutter()
            {
                draw = true,
                ignore_collision = true,
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "PopupE"), 16, 16),
                draw_height = 16,
                draw_width = 16
            };

            lowerUI = new Clutter()
            {
                draw = true,
                ignore_collision = true,
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "lowerui"), 320, 20),
                idleAnimation = new Animation(1, 2, 1000),
                animated = true,
                invulnerable = true,
                draw_width = 320,
                draw_height = 20,
                pos = new Vector2(0, 160)
            };

            heart = new Clutter()
            {
                draw = true,
                ignore_collision = true,
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Heart"), 16, 16),
                draw_height = 16,
                draw_width = 16
            };

            // Rooms
            roomMaps.LoadTileMaps(graphics);
            all_rooms = new List<Room>();
            foreach(var r in roomMaps.rooms)
            {
                all_rooms.Add(r);
            }

            // SPAWNROOM
            current_room = Room.GetRoomByID(all_rooms, Globals.spawnroomID);
            SetRoom(current_room);

            // Load the player
            player = new Player();
            player.Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(graphics, "PlayerIdle"), 32, 32);
            player.facing = Directions.Left;
            player.SetAnimations();
            player.Sprite.PlayAnimation(player.AnimIdleLeft);
            player.SetPosCentre(Globals.spawn_location);
            player.active = true;

            _menu.Load(graphics, "GameplayMenuDefinition", "UITemplates");

            _timerLabel = _menu.GetWidget<PUIWLabel>("lblTimer");

            _gameTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            _rippleTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            _windTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            _deathTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
            _finalTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

            ModManager.Instance.SoundManager.StopAll();
            ModManager.Instance.SoundManager.PlaySound("MusicIngame", (int)SoundType.Music, true);
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
            // Timers
            countdown -= gameTime.DeltaTime();
            ripple_timer -= gameTime.DeltaTime();
            if(ripple_timer <= 0)
            {
                isRipple = false;
                ripple_timer = 0;
            }
            respawn_timer -= gameTime.DeltaTime();
            if (respawn_timer <= 0)
            {
                respawn_timer = 0;
            }

            for (int i = DeathEffects.Count - 1; i >= 0; i--)
            {
                DeathEffects[i].TTL -= gameTime.DeltaTime();

                if (DeathEffects[i].TTL <= 0) {
                    DeathEffects.RemoveAt(i);
                }
            }

            if (clock_repair_big && clock_repair_small)
            {
                player.inventory.Clear();
                winning_timer -= gameTime.DeltaTime();
            }
            if(winning_timer < 0)
            {
                return (int)GameStateType.GameOver;
            }

            if (countdown < 0)
                countdown = 0;
            _timerLabel.Text = countdown.ToString("0.0");

            // Player
            Vector2 oldpos = new Vector2(0);
            Vector2 newpos = new Vector2(0);
            oldpos.X = player.pos.X;
            oldpos.Y = player.pos.Y;
            player.Update(gameTime);
            Room.NudgeOOB(current_room, player);
            newpos.X = player.pos.X;
            newpos.Y = player.pos.Y;

            // Death and respawning
            if (player.hp <= 0 && respawn_timer <= 0)
                if (!respawning)
                {
                    player.dead = true;
                    // todo Play death animation
                    respawn_timer = 3;
                    respawning = true;

                    ripple_timer = 6;
                    isRipple = true;
                }
            if (countdown <= 0 && !respawning)
            {
                respawn_timer = 3;
                respawning = true;
                ripple_timer = 6;
                isRipple = true;
            }
            if (respawning && respawn_timer <= 0)
            {
                // todo make this cleaner
                roomMaps = new RoomMaps();
                roomMaps.LoadTileMaps(Globals.GraphicsDevice);
                all_rooms = roomMaps.rooms;
                current_room = Room.GetRoomByID(all_rooms, Globals.spawnroomID);
                SetRoom(current_room);

                List<Clutter> clock_hands_keep = new List<Clutter>();
                foreach (var item in player.inventory)
                {
                    if(item.clock_handle > 0)
                    {
                        clock_hands_keep.Add(item);
                    }
                }

                player = new Player();
                player.Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "PlayerIdle"), 32, 32);
                player.facing = Directions.Left;
                player.SetAnimations();
                player.Sprite.PlayAnimation(player.AnimIdleLeft);
                player.SetPosCentre(Globals.spawn_location);
                player.active = true;
                player.dead = false;
                respawning = false;
                countdown = Globals.countdown_time;

                foreach(var item in clock_hands_keep)
                {
                    //player.inventory.Add(item);
                }
            }

            if (ripple_timer > 0f) {
                float rippleBounce = Math.Abs(3f - ripple_timer);
                if (ripple_timer < 3f) {
                    Globals.Ripple.Parameters["center"].SetValue(new Vector2(0.5f));
                } else {
                    Globals.Ripple.Parameters["center"].SetValue((player.Centre() + new Vector2(0, -16)) / new Vector2(320, 160));
                }
                Globals.Ripple.Parameters["amplitude"].SetValue((3f - rippleBounce) * 0.003f);
                Globals.Ripple.Parameters["frequency"].SetValue((6f - ripple_timer) * 30f);
                Globals.Ripple.Parameters["size"].SetValue(6f - ripple_timer);

                Globals.Wind.Parameters["wind_strength"].SetValue((3f - rippleBounce) * 0.002f);

                Globals.Ripple.Parameters["phase"].SetValue((float)gameTime.TotalGameTime.TotalMilliseconds / 100f);
                Globals.Wind.Parameters["time"].SetValue((float)gameTime.TotalGameTime.TotalMilliseconds / 1000f);
            }

            // Clutters
            popup_e.draw = false;
            bool delete = false;
            int delete_index = 0;

            for (int i = 0; i < current_room.clutters.Count; i++)
            {

                
                Clutter c = (Clutter)current_room.clutters[i];

                c.Update(gameTime);
                
                if (Entity.Collision(player, c))
                {
                    player.pos = oldpos;
                    player.StopMoving(player.facing);
                }
                if (c.collectable && Entity.Collision(player, c, false)){
                    player.inventory.Add(c);

                    c.Kill(gameTime);
                }

                
                // Popup E button if near enough
                if (c.button && Vector2.Distance(player.Centre(), c.Centre()) < 30)
                {
                    popup_e.SetPosCentre(c.Centre() + (new Vector2(0, -20)));
                    popup_e.draw = true;
                }

                // Clock repair 
                if (c.clock_repair)
                {
                    foreach (var item in player.inventory)
                    {
                        if (item.clock_handle > 0 && Vector2.Distance(player.Centre(), c.Centre()) < 30)
                        {
                            popup_e.SetPosCentre(c.Centre());
                            popup_e.draw = true;
                        }
                    }
                }

                // Delete clockhands if they have been used
                foreach(var item in player.inventory)
                {
                    if ((item.clock_handle == 1 && c.clock_handle == 1) || (item.clock_handle == 2 && c.clock_handle == 2))
                    {
                        delete = true;
                        delete_index = i;
                    }
                }
                if ((c.clock_handle == 1 && clock_repair_big) || (c.clock_handle == 2 && clock_repair_small))
                {
                    delete = true;
                    delete_index = i;
                }

                // Repair clockhands
                if (c.is_big_hand && clock_repair_big)
                {
                    c.draw = true;
                }
                else if (c.is_small_hand && clock_repair_small)
                {
                    c.draw = true;
                }
            }
            if (delete)
                current_room.clutters.RemoveAt(delete_index);


            // Doors
            foreach (var d in current_room.doors)
            {
                if (Entity.Collision(player, d, false) && !d.locked)
                {
                    if (Room.GetRoomByID(all_rooms, d.next_room_id).room_id != -1)
                    {
                        // Transition to next room
                        Bullets.Clear();
                        current_room = Room.GetRoomByID(all_rooms, d.next_room_id);
                        player.SetPos(d.next_posX, d.next_posY);
                        //player.vel = new Vector2(0);
                        //player.ResetMoving();
                        SetRoom(current_room);
                        current_room.OnEntry(gameTime);

                        Console.WriteLine(current_room.room_id);
                    }
                }

                // Draw popup button if door is unlockable
                if (d.locked && Vector2.Distance(player.Centre(), d.Centre()) < 30)
                {
                    if (d.unlocking) break;
                    var i = 0;
                    for (; i < player.inventory.Count; i++)
                    {
                        Clutter c = player.inventory[i];
                        if (c.keyid > 0 && c.keyid == d.unlock_id)
                        {
                            popup_e.SetPosCentre(d.Centre() + (new Vector2(0, 10)));
                            popup_e.draw = true;
                        }
                    }
                }
            }
            if (current_room.trap_room)
            {
                bool enemies_alive = false;
                foreach (var e in current_room.enemies)
                {
                    if (!e.dead)
                    {
                        enemies_alive = true;
                    }
                }
                if (!enemies_alive)
                {
                    current_room.trap_door.Unlock();
                }
            }

            // Enemies
            foreach (var e in current_room.enemies)
            {
                oldpos = e.pos;

                if (!e.dead && e.spawnFinished)
                {
                    //Enemy AI
                    //if (e.enemyType == EnemyType.Caveman) EnemyAI.CaveManAI(e, player, current_room, EnemyBullets, gameTime);
                    //else if (e.enemyType == EnemyType.Cyborg) EnemyAI.CaveManAI(e, player, current_room, EnemyBullets, gameTime);
                    //else if (e.enemyType == EnemyType.CaveBorg) EnemyAI.CaveManAI(e, player, current_room, EnemyBullets, gameTime);

                    EnemyAI.CaveManAI(e, player, current_room, EnemyBullets, gameTime);
                }
                if (e.dead) continue;
                e.pos.X += e.speed * e.vel.X * gameTime.DeltaTime();
                foreach(var e2 in current_room.enemies){
                    if (e2 != e)
                    {
                        if (Entity.Collision(e, e2))
                            {
                            e.pos.X = oldpos.X;
                            }

                    }
                }
                e.pos.Y += e.speed * e.vel.Y * gameTime.DeltaTime();
                foreach (var e2 in current_room.enemies)
                {
                    if (e2 != e)
                    {
                        if (Entity.Collision(e, e2))
                        {
                            e.pos.Y = oldpos.Y;
                        }

                    }
                }

                // Changing the priority for cavemen
                if(e.enemyType == EnemyType.Caveman)
                {
                    var prevVel = e.vel;
                    var newFacing = Directions.None;

                    if (e.moveup)
                    {
                        e.vel.Y = -1;
                        newFacing = Directions.Up;
                    }
                    else if (e.movedown)
                    {
                        e.vel.Y = 1;
                        newFacing = Directions.Down;
                    }

                    if (e.moveleft)
                    {
                        e.vel.X = -1;
                            newFacing = Directions.Left;
                    }
                    else if (e.moveright)
                    {
                        e.vel.X = 1;
                            newFacing = Directions.Right;
                    }

                    if (newFacing != Directions.None)
                        e.facing = newFacing;

                    if (prevVel != e.vel)
                    {
                        if (e.vel == Vector2.Zero)
                            e.PlayIdle();
                        else
                            e.PlayMoving();
                    }

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
                        if (e.hp <= 0)
                        {
                            e.Kill(gameTime);
                            DeathEffects.Add(new DeathEffect(e.Centre(), 1f));
                        }

                        b.Kill(gameTime);
                    }
                }
                foreach(var c in current_room.clutters)
                {
                    if (c.shootable)
                    {
                        if (Entity.Collision(c, b))
                        {
                            if (!c.invulnerable)
                                c.hp -= b.damage;
                            if (c.hp <= 0) c.Kill(gameTime);
                            b.Kill(gameTime);
                        }
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
                    //ModManager.Instance.SoundManager.PlaySound("SFXHurt", (int)SoundType.SoundEffect, false);
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
                    ModManager.Instance.SoundManager.PlaySound("SFXShoot", (int)SoundType.SoundEffect, false);
                }
            }

            // Inventory
            // todo fix inventory being invis on leaving the room you picked it up in
            for (int i = 0; i < player.inventory.Count; i++)
            {
                Clutter c = player.inventory[i];
                c.ignore_collision = true;
                c.draw_height = 16;
                c.draw_width = 16;
                c.draw = true;
                c.pos = new Vector2(300 - i * 20, 162);
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
            foreach (var c in current_room.clutters)
            {
                if (c.draw_height!= 0 && c.draw_width!= 0)
                {
                    DrawEntities.Add(c);
                }
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


            // E popup when near a button
            if (popup_e.draw)
                popup_e.Draw(gameTime, spriteBatch, Color.White);

            spriteBatch.End();

            if (isRipple) {
                graphics.SetRenderTarget(_rippleTarget);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, effect: Globals.Ripple);
                spriteBatch.Draw(_gameTarget, Vector2.Zero, Color.White);
                spriteBatch.End();

                graphics.SetRenderTarget(_windTarget);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, effect: Globals.Wind);
                spriteBatch.Draw(_rippleTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            } else {
                graphics.SetRenderTarget(_windTarget);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
                spriteBatch.Draw(_gameTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            graphics.SetRenderTarget(_finalTarget);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            spriteBatch.Draw(_windTarget, Vector2.Zero, Color.White);
            spriteBatch.End();

            foreach (DeathEffect de in DeathEffects) {
                float rippleBounce = Math.Abs(0.5f - de.TTL);
                Globals.RippleDeath.Parameters["center"].SetValue(de.Pos / new Vector2(320, 160));
                Globals.RippleDeath.Parameters["amplitude"].SetValue((0.5f - rippleBounce) * 1f);
                Globals.RippleDeath.Parameters["frequency"].SetValue((0.5f - rippleBounce) * 120f);
                Globals.RippleDeath.Parameters["size"].SetValue((1f - de.TTL) / 2);

                graphics.SetRenderTarget(_deathTarget);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, effect: Globals.RippleDeath);
                spriteBatch.Draw(_finalTarget, Vector2.Zero, Color.White);
                spriteBatch.End();

                graphics.SetRenderTarget(_finalTarget);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
                spriteBatch.Draw(_deathTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            // Todo draw bottom bar inc health, ammo, inventory, timer
            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: _camera.View());
            
            lowerUI.Draw(gameTime, spriteBatch, Color.White);

            foreach (var i in player.inventory)
            {
                i.Draw(gameTime, spriteBatch, Color.White);
            }

            Vector2 heartPos = new Vector2(10, 160);
            for(int i = 0; i < player.hp; i++)
            {
                heart.pos = heartPos + (new Vector2(20, 0)) * i;
                heart.Draw(gameTime,spriteBatch,Color.White);
            }

            spriteBatch.End();

            graphics.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            spriteBatch.Draw(_finalTarget, Vector2.Zero, Color.White);
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
                //player.hp = -10;
            }

            if (key == Keys.E)
            {
                bool hand_delete = false;
                int hand_delete_index = 0;
                foreach(var c in current_room.clutters)
                {

                    // Activate buttons
                    if (c.button && Vector2.Distance(player.Centre(), c.Centre()) < 30){
                        c.door_unlock.Unlock();
                        c.button = false;
                    }

                    // Repair the clock
                    if (c.clock_repair && Vector2.Distance(player.Centre(), c.Centre()) < 30)
                    {
                        for (int i = 0; i < player.inventory.Count; i++)
                        {
                            Clutter item = player.inventory[i];
                            if (item.clock_handle == 1)
                            {
                                clock_repair_big = true;
                                hand_delete = true;
                                hand_delete_index = i;
                            }
                            else if(item.clock_handle == 2)
                            {
                                clock_repair_small = true;
                                hand_delete = true;
                                hand_delete_index = i;
                            }
                        }
                        
                    }
                }
                if (hand_delete)
                    player.inventory.RemoveAt(hand_delete_index);

                // Unlock doors if you have the key
                bool delete_key = false;
                int delete_index = 0;
                foreach (var d in current_room.doors)
                {
                    if (d.unlocking) break;
                    if (d.locked && Vector2.Distance(player.Centre(), d.Centre()) < 30)
                    {
                        var i = 0;
                        for (; i < player.inventory.Count; i++)
                        {
                            Clutter c = player.inventory[i];
                            if (c.keyid > 0 && c.keyid == d.unlock_id)
                            {
                                d.Unlock();
                                delete_key = true;
                                delete_index = i;
                                break;
                            }
                        }
                        
                    }
                }
                if (delete_key)
                {
                    player.inventory.RemoveAt(delete_index);
                }
                
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
