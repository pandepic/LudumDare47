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

namespace GameCore
{
    public class GamePlayState : GameState
    {
        protected GameStateType _nextState = GameStateType.None;
        protected PUIMenu _menu = new PUIMenu();

        public static GraphicsDevice Graphics;

        public List<Room> all_rooms;
        public Room current_room;
        public Player player;
        public Double last_frame_time;

        public List<Bullet> Bullets = new List<Bullet>();

        Texture2D playerIdle;

        public override void Load(ContentManager Content, GraphicsDevice graphics)
        {
            // Assets
            playerIdle = ModManager.Instance.AssetManager.LoadTexture2D(graphics, "PlayerIdle");

            // Placeholder rooms
            all_rooms = new List<Room>();
            all_rooms.Add(new Room(320, 180, new List<Enemy>(), new List<Door>(), 0, "Test Room 1"));
            all_rooms[0].enemies.Add(new Enemy(5, 100, 100));
            all_rooms[0].enemies.Add(new Enemy(5, 20, 20));
            all_rooms[0].doors.Add(new Door(1,-24, 72, 280, 72));
            // all_rooms[0].doors.Add(new Door(0, -15, 0));
            current_room = all_rooms[0];
            all_rooms.Add(new Room(320, 180, new List<Enemy>(), new List<Door>(), 1, "Test Room 2"));
            all_rooms[1].doors.Add(new Door(0, 312, 72, 40, 72));

            // Load the player
            player = new Player();
            player.SetPosCentre(current_room.room_width / 2, current_room.room_height / 2);
            player.playerIdle = playerIdle;
            
            _menu.Load(graphics, "GameplayMenuDefinition", "UITemplates");
        }

        public override int Update(GameTime gameTime)
        {
            // 128 per second update limit
            //if (gameTime.TotalGameTime.TotalMilliseconds - last_frame_time < (double)1000/128)
            //   return (int)_nextState;
            //last_frame_time = gameTime.TotalGameTime.TotalMilliseconds;
            

            current_room.Update(gameTime);

            // Player
            player.Update(gameTime);
            Room.NudgeOOB(current_room, player);
            // Check for touching doors
            foreach (var d in current_room.doors)
            {
                if (Entity.Collision(player, d))
                {
                    if (Room.GetRoomByID(all_rooms, d.next_room_id).room_id != -1)
                    {
                        // Transition to next room
                        Bullets.Clear();
                        current_room = Room.GetRoomByID(all_rooms, d.next_room_id);
                        player.SetPos(d.next_posX, d.next_posY);
                        player.vel = new Vector2(0);
                    }
                }
            }


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


            // Shoot a bullet - to add shooting cooldown?
            if (player.shooting == true)
            {
                player.shooting = false;
                Bullets.Add(new Bullet(player));
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
                if (a.pos.Y + a.height > b.pos.Y + b.height) return 1;
                else return -1;
            });

            // Draw
            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
            current_room.Draw(gameTime, graphics, spriteBatch);
            foreach (var e in DrawEntities)
            {
                e.Draw(gameTime, graphics, spriteBatch);
            }
            
            foreach (var b in Bullets)
            {
                b.Draw(gameTime, graphics, spriteBatch);
            }
            spriteBatch.End();


            /* graphics.Clear(Color.Black);
             spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
             current_room.Draw(gameTime, graphics, spriteBatch);
             player.Draw(gameTime, graphics, spriteBatch);
             foreach(var b in Bullets)
             {
                 b.Draw(gameTime, graphics, spriteBatch);
             }
             spriteBatch.End(); */

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
                player.shooting = true;
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
