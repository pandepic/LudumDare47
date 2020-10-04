using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;


namespace GameCore.Entities
{
    public class Room
    {

        public int room_width;
        public int room_height;
        public int room_id;
        public List<Enemy> enemies;
        public List<Door> doors;
        public List<Clutter> clutters = new List<Clutter>();
        string name;
        public bool trap_room = false;
        public Door trap_door = new Door();

        public TMXMap tileMap;


        public Room()
        {
            room_width = 320;
            room_height = 180;
            room_id = -1;
            enemies = new List<Enemy>();
            doors = new List<Door>();
            name = "Default";
        }

        public Room(int new_width, int new_height, List<Enemy> new_enemies, List<Door> new_doors, int new_id, string new_name = "Default")
        {
            room_width = new_width;
            room_height = new_height;
            enemies = new_enemies;
            doors = new_doors;
            room_id = new_id;
            name = new_name;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var d in doors)
            {
                d.Update(gameTime);
            }

            foreach (var e in enemies)
            {
                e.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the room
                spriteBatch.Draw(
                        Globals.PlaceholderTexture,
                        new Vector2(0),
                        new Rectangle(0, 0, room_width, room_height),
                        new Color(60, 60, 60),
                        MathHelper.ToRadians(0.0f),
                        new Vector2(0),
                        new Vector2(1),
                        SpriteEffects.None,
                        0.0f
                        );
            if (tileMap != null)
            { 
                tileMap.Draw(spriteBatch);
                
            }
        }

        public void OnEntry(GameTime gameTime)
        {
            if (trap_room)
            {
                trap_door.Lock();
            }
        }

        public static Room GetRoomByName(List<Room> all_rooms, string name)
        {
            // Return the first room with matching name, else return an uninitialised room with id -1
            if (all_rooms.Capacity <= 0)
            {
                return new Room();
            }

            foreach (var r in all_rooms)
            {
                if (r.name == name)
                {
                    return r;
                }
            }

            return new Room();
        }

        public static Room GetRoomByID(List<Room> all_rooms, int id)
        {
            // Return the first room with matching ID else return an uninitialised room with id -1
            if (all_rooms.Capacity <= 0)
            {
                return new Room();
            }

            foreach (var r in all_rooms)
            {
                if (r.room_id == id)
                {
                    return r;
                }
            }

            return new Room();
        }

        public static bool CheckOOB(Room room, Entity entity)
        {
            // Check if entity is within bounds of room
            return (entity.pos.X + entity.draw_width > room.room_width) || (entity.pos.Y < 0) || (entity.pos.Y + entity.draw_height > room.room_height) || (entity.pos.X < 0);
        }

        public static Entity NudgeOOB(Room room, Entity entity)
        {
            // Nudge the entity back into bounds if OOB
            if (entity.pos.X + entity.draw_width > room.room_width)
                entity.pos.X = room.room_width - entity.draw_width;
            if (entity.pos.X < 0)
                entity.pos.X = 0;
            if (entity.pos.Y + entity.draw_height > room.room_height)
                entity.pos.Y = room.room_height - entity.draw_height;
            if (entity.pos.Y < 0)
                entity.pos.Y = 0;
            return entity;
        }
    }

}
