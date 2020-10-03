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
        string name;
        public Texture2D FloorPlaceHolderTexture = null;
        

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

        public void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            // Placeholder texture
            if (FloorPlaceHolderTexture == null)
            {
                FloorPlaceHolderTexture = new RenderTarget2D(graphics, 1, 1);
                graphics.SetRenderTarget((RenderTarget2D)FloorPlaceHolderTexture);
                graphics.Clear(new Color(120, 120, 120, 120));
                graphics.SetRenderTarget(null);
            }

            // Draw the room
            spriteBatch.Draw(
                        FloorPlaceHolderTexture,
                        new Vector2(0),
                        new Rectangle(0, 0, room_width, room_height),
                        Color.Gray,
                        MathHelper.ToRadians(0.0f),
                        new Vector2(0),
                        new Vector2(1),
                        SpriteEffects.None,
                        0.0f
                        );

            /*//Draw enemies and doors
            foreach (var e in enemies)
            {
                e.Draw(gameTime, graphics, spriteBatch);
            }
            foreach (var d in doors)
            {
                d.Draw(gameTime, graphics, spriteBatch);
            }
            */

        }

        public void OnEntry()
        {
            
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
            return (entity.posX + entity.width > room.room_width) || (entity.posY < 0) || (entity.posY + entity.height > room.room_height) || (entity.posX < 0);
        }

        public static Entity NudgeOOB(Room room, Entity entity)
        {
            // Nudge the entity back into bounds if OOB
            if (entity.posX + entity.width > room.room_width)
                entity.posX = room.room_width - entity.width;
            if (entity.posX < 0)
                entity.posX = 0;
            if (entity.posY + entity.height > room.room_height)
                entity.posY = room.room_height - entity.height;
            if (entity.posY < 0)
                entity.posY = 0;
            return entity;
        }
    }

}
