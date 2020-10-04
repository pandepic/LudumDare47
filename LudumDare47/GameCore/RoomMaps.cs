using GameCore.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using System.Collections.Generic;

namespace GameCore
{
    public class RoomMaps
    {
        public List<Room> rooms = new List<Room>();

        public RoomMaps()
        {
            Room newroom;
            Enemy newenemy;
            Door newdoor;
            Clutter newclutter;
           // Room 100
           newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 100, "Caveman 1");
            newroom.doors.Add(new Door(101, -24, 72, 275, 72)); // Left door
            newroom.doors.Add(new Door(0, 305, 72, 10, 72));    // Right door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(160, 90);
            newroom.enemies.Add(newenemy);
            newroom.clutters.Add(CollisionBox(320, 5, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(44, 15, new Vector2(106,0))); // Left bench
            newroom.clutters.Add(CollisionBox(44, 15, new Vector2(224, 0))); // Right bench
            rooms.Add(newroom);

            // Room 101
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 101, "Caveman 2");
            newroom.doors.Add(new Door(102, -24, 72, 275, 72)); // Left door
            newroom.doors.Add(new Door(100, 305, 72, 10, 72));  // Right door
            newroom.doors.Add(new Door(106, 144, -15, 144, 110));  // Top door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 50);
            newroom.enemies.Add(newenemy);
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 100);
            newroom.enemies.Add(newenemy);
            newroom.clutters.Add(CollisionBox(320, 5, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(60, 15, new Vector2(16, 0))); // Left bench
            newroom.clutters.Add(CollisionBox(44, 15, new Vector2(230, 0))); // Right bench
            rooms.Add(newroom);

            // Room 102
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 102, "Caveman 3");
            newroom.doors.Add(new Door(103, -24, 72, 275, 72)); // Left door
            newroom.doors.Add(new Door(101, 305, 72, 10, 72));    // Right door
            newdoor = new Door()
            {
                pos = new Vector2(144, 32),
                draw_width = 32,
                draw_height = 32,
                col_width = 8,
                col_height = 25,
                collision_offset = new Vector2(11, 0),
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Door"), 32, 32),
                next_room_id = 107,
                next_posX = 144,
                next_posY = 110,
                ignore_collision = true,
                unlock_id = 1,
                locked = true
            };
            newroom.doors.Add(newdoor);
            newroom.doors.Add(new Door(112, 144, 150, 144, 10));  // Bottom door
            newroom.clutters.Add(CollisionBox(320, 53, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(125, 42, new Vector2(0,113)));  // Bottom left wall
            //newroom.clutters.Add(CollisionBox(320, 40, Vector2.Zero));  // Bottom right wall
            rooms.Add(newroom);

            // Room 103
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 103, "Caveman 4");
            newdoor = new Door(104, -24, 72, 275, 72, true); // Left door
            newroom.doors.Add(newdoor); 
            newroom.doors.Add(new Door(102, 305, 72, 10, 72));    // Right door
            newroom.clutters.Add(CollisionBox(320, 5, Vector2.Zero));  // Back wall
            newroom.clutters.Add(Crate(new Vector2(20, 30)));
            newroom.clutters.Add(Crate(new Vector2(60, 30)));
            newroom.clutters.Add(Crate(new Vector2(100, 30)));
            newroom.clutters.Add(Crate(new Vector2(140, 30)));
            newroom.clutters.Add(Crate(new Vector2(180, 30)));
            newroom.clutters.Add(Crate(new Vector2(220, 30)));
            newroom.clutters.Add(Crate(new Vector2(260, 30)));
            newroom.clutters.Add(Crate(new Vector2(20, 110)));
            newroom.clutters.Add(Crate(new Vector2(60, 110)));
            newroom.clutters.Add(Crate(new Vector2(100, 110)));
            newroom.clutters.Add(Crate(new Vector2(140, 110)));
            newroom.clutters.Add(Crate(new Vector2(180, 110)));
            newroom.clutters.Add(Crate(new Vector2(220, 110)));
            newroom.clutters.Add(Crate(new Vector2(260, 110)));
            newroom.clutters.Add(Button(new Vector2(100, 109), newdoor));
            rooms.Add(newroom);

            // Room 104
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 104, "Caveman 5");
            newroom.doors.Add(new Door(105, -24, 72, 275, 72)); // Left door
            newroom.doors.Add(new Door(103, 305, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(108, 144, -15, 144, 110));  // Top door
            newroom.doors.Add(new Door(114, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 105
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 105, "Caveman 6");
            newroom.doors.Add(new Door(104, 305, 72, 10, 72));    // Right door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 80);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 106
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 106, "Caveman 7");
            newroom.trap_room = true;
            newdoor = new Door(101, 144, 150, 144, 10);
            newroom.trap_door = newdoor;
            newroom.doors.Add(newdoor);  // Bottom doornewenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 30);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 107
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 107, "BOSS ROOM");
            newroom.doors.Add(new Door(102, 144, 150, 144, 50));  // Bottom door
            newroom.doors.Add(new Door(109, 144, -15, 144, 100)); // Top door
            rooms.Add(newroom);

            // Room 108
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 108, "Caveman 8");
            newroom.doors.Add(new Door(104, 144, 150, 144, 10)); // Bottom door
            newroom.doors.Add(new Door(110, 144, -15, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 109
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 109, "PUZZLE ROOM");
            newroom.doors.Add(new Door(107, 144, 150, 144, 10));  // Bottom door
            newroom.clutters.Add(new Clutter()
            {
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Part1"), 16, 16),
                pos = new Vector2(100, 100),
                collectable = true,
                ignore_collision = true,
                draw_height = 16,
                draw_width = 16,
                col_height = 16,
                col_width = 16
            });
            rooms.Add(newroom);

            // Room 110
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 110, "KEY ROOM");
            newroom.doors.Add(new Door(108, 144, 150, 144, 10));  // Bottom door
            newroom.clutters.Add(Key(new Vector2(100, 100), 1));

            rooms.Add(newroom);

            // Room 111
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 111, "PASSWORD ROOM");
            newroom.doors.Add(new Door(115, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 112
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 112, "Caveman 9");
            newroom.doors.Add(new Door(116, 144, 150, 144, 10));  // Bottom door
            newroom.doors.Add(new Door(102, 144, -15, 144, 110)); // Top door
            newroom.doors.Add(new Door(113, -24, 72, 250, 72));   // Left door
            rooms.Add(newroom);

            // Room 113
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 113, "Caveman 10");
            newroom.doors.Add(new Door(112, 305, 72, 10, 72));    // Right door
            rooms.Add(newroom);

            // Room 114
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 114, "Caveman 11");
            newroom.doors.Add(new Door(104, 144, -15, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 115
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 115, "Caveman 12");
            newroom.doors.Add(new Door(116, -15, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(111, 144, -15, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 116
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 116, "Caveman 13");
            newroom.doors.Add(new Door(117, 144, 150, 144, 10));  // Bottom door
            newroom.doors.Add(new Door(112, 144, -15, 144, 110)); // Top door
            newroom.doors.Add(new Door(115, 305, 72, 10, 72));    // Right door
            rooms.Add(newroom);

            // Room 117
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 117, "Caveman 14");
            newroom.doors.Add(new Door(116, 144, -15, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 200
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 200, "Viking 1");
            newroom.doors.Add(new Door(201, 305, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(5, -24, 72, 250, 72));   // Left door
            rooms.Add(newroom);

            // Room 201
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 201, "THE MAZE");
            newroom.doors.Add(new Door(202, 305, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(200, -24, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(206, 144, -15, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 202
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 202, "Viking 2");
            newroom.doors.Add(new Door(203, 305, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(201, -24, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(207, 144, -24, 144, 110)); // Top door
            newroom.doors.Add(new Door(210, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 203
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 203, "Viking 3");
            newroom.doors.Add(new Door(204, 305, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(203, -24, 72, 250, 72));   // Left door
            rooms.Add(newroom);

            // Room 204
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 204, "Viking 4");
            newroom.doors.Add(new Door(205, 305, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(203, -24, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(208, 144, -24, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 206
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 204, "Viking 5");
            newroom.doors.Add(new Door(201, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 207
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 206, "Viking 7");
            newroom.doors.Add(new Door(206, 144, 150, 144, 10));  // Bottom door
            newroom.doors.Add(new Door(208, 305, 72, 10, 72));    // Right door
            rooms.Add(newroom);

            // Room 208
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 208, "Viking 8");
            newroom.doors.Add(new Door(207, -24, 72, 250, 72));   // Left door
            rooms.Add(newroom);

            // Room 209
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 209, "Viking 9");
            newroom.doors.Add(new Door(210, -24, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(204, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 210
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 209, "Viking 10");
            newroom.doors.Add(new Door(209, -24, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(205, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

        }

        public void LoadTileMaps(GraphicsDevice graphics)
        {
            foreach (Room r in rooms)
            {
                if (r.room_id <= 103)
                {
                    TMXMap newmap = new TMXMap(graphics, "TileMap" + r.room_id.ToString());
                    r.tileMap = newmap;
                }
            }
        }

        public static Enemy CaveMan()
        {
            Enemy caveman = new Enemy
            {
                hp = 5,
                speed = 20,
                range = 50,
                enemyType = EnemyType.Caveman,
                attack_cooldown = 2000
            };
            caveman.SetSprite();
            caveman.SetAnimations();
            return caveman;
        }

        public static Clutter CollisionBox(int width, int height, Vector2 position)
        {
            Clutter collisionBox = new Clutter()
            {
                col_width = width,
                col_height = height,
                draw_height = 0,
                draw_width = 0,
                ignore_collision = false,
                pos = position,
                shootable = true,
                invulnerable = true
            };

            return collisionBox;
        }

        public static Clutter Crate(Vector2 position)
        {
            Clutter crate = new Clutter()
            {
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Crate"), 32, 32),
                pos = position,
                draw_width = 32,
                col_width = 32,
                draw_height = 32,
                col_height = 32,
                hp = 3,
                dead = false,
                shootable = true,
                ignore_collision = false
            };
            return crate;
        }

        public static Clutter Button (Vector2 position, Door unlockdoor)
        {
            Clutter button = new Clutter()
            {
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Button"), 32, 32),
                button = true,
                pos = position,
                door_unlock = unlockdoor,
                col_width= 20,
                col_height=25,
                collision_offset = new Vector2(5,7)
            };

            return button;
        }

        public static Clutter Key (Vector2 position, int new_id)
        {
            Clutter key = new Clutter()
            {
                pos = position,
                collectable = true,
                ignore_collision = true,
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Key"), 16, 16),
                draw_height = 16,
                keyid = new_id,
                draw_width = 16
            };
            return key;
        }
    }

   
}
