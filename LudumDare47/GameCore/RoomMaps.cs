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
           // Room 100
           newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 100, "Caveman 1");
            newroom.doors.Add(new Door(101, -24, 72, 275, 72)); // Left door
            newroom.doors.Add(new Door(0, 312, 72, 10, 72));    // Right door
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
            newroom.doors.Add(new Door(100, 312, 72, 10, 72));  // Right door
            newroom.doors.Add(new Door(106, 144, -24, 144, 110));  // Top door
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
            newroom.doors.Add(new Door(101, 312, 72, 10, 72));    // Right door
            newdoor = new Door()
            {
                pos = new Vector2(144, 32),
                draw_width = 32,
                draw_height = 32,
                col_width = 10,
                col_height = 10,
                collision_offset = new Vector2(10, 0),
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Door"), 32, 32),
                next_room_id = 107,
                next_posX = 144,
                next_posY = 110,
                ignore_collision = true,
                locked = true
            };

            newroom.doors.Add(newdoor);
            newroom.doors.Add(new Door(112, 144, 150, 144, 10));  // Bottom door
            newroom.clutters.Add(CollisionBox(320, 40, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(125, 42, new Vector2(0,113)));  // Bottom left wall
            //newroom.clutters.Add(CollisionBox(320, 40, Vector2.Zero));  // Bottom right wall
            rooms.Add(newroom);

            // Room 103
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 103, "Caveman 4");
            newdoor = new Door(104, -24, 72, 275, 72, true); // Left door
            newroom.doors.Add(newdoor); 
            newroom.doors.Add(new Door(102, 312, 72, 10, 72));    // Right door
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
            newroom.doors.Add(new Door(103, 312, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(108, 144, -24, 144, 110));  // Top door
            newroom.doors.Add(new Door(114, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 105
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 105, "Caveman 6");
            newroom.doors.Add(new Door(104, 312, 72, 10, 72));    // Right door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 80);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 106
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 106, "Caveman 7");
            newroom.doors.Add(new Door(101, 144, 150, 144, 10));  // Bottom door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 30);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 107
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 107, "BOSS ROOM");
            newroom.doors.Add(new Door(102, 144, 150, 144, 60));  // Bottom door
            newroom.doors.Add(new Door(109, 144, -24, 144, 100)); // Top door
            rooms.Add(newroom);

            // Room 108
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 108, "Caveman 8");
            newroom.doors.Add(new Door(104, 144, 150, 144, 10));  // Bottom door
            newroom.doors.Add(new Door(110, 144, -24, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 109
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 109, "PUZZLE ROOM");
            newroom.doors.Add(new Door(107, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 110
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 110, "KEY ROOM");
            newroom.doors.Add(new Door(108, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 111
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 111, "PASSWORD ROOM");
            newroom.doors.Add(new Door(115, 144, 150, 144, 10));  // Bottom door
            rooms.Add(newroom);

            // Room 112
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 112, "Caveman 9");
            newroom.doors.Add(new Door(116, 144, 150, 144, 10));  // Bottom door
            newroom.doors.Add(new Door(102, 144, -24, 144, 110)); // Top door
            newroom.doors.Add(new Door(113, -24, 72, 250, 72));   // Left door
            rooms.Add(newroom);

            // Room 113
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 113, "Caveman 10");
            newroom.doors.Add(new Door(112, 312, 72, 10, 72));    // Right door
            rooms.Add(newroom);

            // Room 114
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 114, "Caveman 11");
            newroom.doors.Add(new Door(104, 144, -24, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 115
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 115, "Caveman 12");
            newroom.doors.Add(new Door(116, -24, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(111, 144, -24, 144, 110)); // Top door
            rooms.Add(newroom);

            // Room 116
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 116, "Caveman 13");
            newroom.doors.Add(new Door(117, 144, 150, 144, 10));  // Bottom door
            newroom.doors.Add(new Door(112, 144, -24, 144, 110)); // Top door
            newroom.doors.Add(new Door(115, 312, 72, 10, 72));    // Right door
            rooms.Add(newroom);

            // Room 117
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 117, "Caveman 14");
            newroom.doors.Add(new Door(116, 144, -24, 144, 110)); // Top door
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

        public static Clutter Wall()
        {
            Clutter wall = new Clutter();

            return wall;
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
                hp = 10,
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
    }

   
}
