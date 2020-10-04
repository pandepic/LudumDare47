using Microsoft.Xna.Framework;
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
        public const float PlayerAttackCooldown = 500.0f;

        protected GameStateType _nextState = GameStateType.None;
        protected PUIMenu _menu = new PUIMenu();
        protected Camera _camera = new Camera(new Vector2(160, 90), (320, 160));

        public static GraphicsDevice Graphics;

        public List<Room> all_rooms;
        public Room current_room;
        public Player player;

        public List<Bullet> Bullets = new List<Bullet>();
        public List<Bullet> EnemyBullets = new List<Bullet>();

        public List<Clutter> Clutter = new List<Clutter>();

        Texture2D playerIdle;

        readonly RoomMaps roomMaps = new RoomMaps();

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
            // Assets
            playerIdle = ModManager.Instance.AssetManager.LoadTexture2D(graphics, "PlayerIdle");

            // Rooms
            all_rooms = new List<Room>();
            foreach(var r in roomMaps.rooms)
            {
                all_rooms.Add(r);
            }
            current_room = Room.GetRoomByID(all_rooms, 100);

            // Load the player
            player = new Player();
            player.SetPosCentre(current_room.room_width / 2, current_room.room_height / 2);
            player.playerIdle = playerIdle;
            player.facing = Directions.Left;
            player.SetPosCentre(280, 90);

            _menu.Load(graphics, "GameplayMenuDefinition", "UITemplates");
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
                        player.vel = new Vector2(0);
                        Console.WriteLine(current_room.room_id);
                    }
                }
            }


            foreach (var e in current_room.enemies)
            {
                if (!e.dead)
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


            // Shoot a bullet
            if (player.shooting == true)
            {
                player.attack_cooldown -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (player.attack_cooldown <= 0)
                {
                    player.attack_cooldown = PlayerAttackCooldown;
                    Bullets.Add(new Bullet(player));
                }
            }

            return (int)_nextState;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
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
            current_room.Draw(gameTime, graphics, spriteBatch);
            foreach (var e in DrawEntities)
            {
                e.Draw(gameTime, graphics, spriteBatch);
            }

            foreach (var b in Bullets)
            {
                b.Draw(gameTime, graphics, spriteBatch);
            }
            foreach (var b in EnemyBullets)
            {
                b.Draw(gameTime, graphics, spriteBatch);
            }
            spriteBatch.End();



            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _menu.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void OnKeyPressed(Keys key, GameTime gameTime, CurrentKeyState currentKeyState)
        {
            _menu.OnKeyPressed(key, gameTime, currentKeyState);

            if (key == Keys.W || key == Keys.Up)
            {
                player.moveup = true;
            }
            else if (key == Keys.S || key == Keys.Down)
            {
                player.movedown = true;
            }
            else if (key == Keys.A || key == Keys.Left)
            {
                player.moveleft = true;
            }
            else if (key == Keys.D || key == Keys.Right)
            {
                player.moveright = true;
            }
            else if (key == Keys.Space)
            {
                player.shooting = true;
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
                player.moveup = false;
            }
            else if (key == Keys.S || key == Keys.Down)
            {
                player.movedown = false;
            }
            else if (key == Keys.A || key == Keys.Left)
            {
                player.moveleft = false;
            }
            else if (key == Keys.D || key == Keys.Right)
            {
                player.moveright = false;
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
