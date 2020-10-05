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
            newroom.doors.Add(BasicDoor(101,Directions.Left)); // Left door
            newroom.doors.Add(BasicDoor(100, Directions.Right));    // Right door
            newroom.enemies.Add(CaveMan(new Vector2(160, 90)));  
            newroom.clutters.Add(CollisionBox(320, 5, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(44, 15, new Vector2(106,0))); // Left bench
            newroom.clutters.Add(CollisionBox(44, 15, new Vector2(224, 0))); // Right bench
            rooms.Add(newroom);

            // Room 101
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 101, "Caveman 2");
            newroom.doors.Add(BasicDoor(102, Directions.Left)); // Left door
            newroom.doors.Add(BasicDoor(100, Directions.Right));  // Right door
            newroom.doors.Add(BasicDoor(106, Directions.Up));  // Top door
            newroom.enemies.Add(CaveMan(new Vector2(80, 60)));
            newroom.enemies.Add(CaveMan(new Vector2(80, 120)));
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
                col_height = 30,
                collision_offset = new Vector2(11, 0),
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Door"), 32, 32), //todo animate this door, ?add lock symbol
                next_room_id = 107,
                next_posX = 144,
                next_posY = 110,
                ignore_collision = true,
                unlock_id = 1,
                locked = true,
                open_time = 1
            };
            newroom.doors.Add(newdoor);
            newroom.doors.Add(new Door(112, 144, 150, 144, 10));  // Bottom door
            newroom.clutters.Add(CollisionBox(320, 56, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(125, 42, new Vector2(0,114)));  // Bottom left wall
            //newroom.clutters.Add(CollisionBox(320, 40, Vector2.Zero));  // Bottom right wall
            rooms.Add(newroom);

            // Room 103 - Room where a button is hidden in a destructable crate
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 103, "Caveman 4");
            newdoor = BasicDoor(104, Directions.Left, true); // Left door
            newdoor.Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "ForceField"), 32, 32); //todo animate this door
            newdoor.draw = true;
            newdoor.pos += new Vector2(10, 8);
            newdoor.draw_if_unlocked = false;
            newdoor.open_time = 2;
            newroom.doors.Add(newdoor);
            newroom.clutters.Add(Button(new Vector2(55, 119), newdoor));
            newroom.doors.Add(new Door(102, 305, 72, 10, 72));    // Right door
            newroom.clutters.Add(CollisionBox(320, 24, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(46, 70, Vector2.Zero));  // Top left
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(0,114)));  // bottom left            
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(274, 0)));  // Top right
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(274, 114)));  // bottom right
            newroom.clutters.Add(Crate(new Vector2(60, 30)));
            newroom.clutters.Add(Crate(new Vector2(100, 30))); 
            newroom.clutters.Add(Crate(new Vector2(140, 30)));
            newroom.clutters.Add(Crate(new Vector2(180, 30)));
            newroom.clutters.Add(Crate(new Vector2(220, 30)));
            newroom.clutters.Add(Crate(new Vector2(60, 120)));
            newroom.clutters.Add(Crate(new Vector2(100, 120)));
            newroom.clutters.Add(Crate(new Vector2(140, 120)));
            newroom.clutters.Add(Crate(new Vector2(180, 120)));
            newroom.clutters.Add(Crate(new Vector2(220, 120)));
            
            rooms.Add(newroom);

            // Room 104
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 104, "Caveman 5");
            newroom.doors.Add(new Door(105, -24, 72, 275, 72)); // Left door
            newroom.doors.Add(new Door(103, 305, 72, 10, 72));    // Right door
            newroom.doors.Add(new Door(108, 144, -15, 144, 110));  // Top door
            newroom.doors.Add(new Door(114, 144, 150, 144, 10));  // Bottom door
            newroom.clutters.Add(CollisionBox(142, 55, Vector2.Zero));  // Top left
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(0, 114)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 0)));  // Top right
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 114)));  // bottom right
            rooms.Add(newroom);

            // Room 105
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 105, "Caveman 6");
            newroom.trap_room = true;
            newdoor = new Door(104, 305, 72, 10, 72);
            newroom.trap_door = newdoor;
            newroom.doors.Add(newdoor);    // Right door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 40);
            newroom.enemies.Add(newenemy); 
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 80);
            newroom.enemies.Add(newenemy);
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 120);
            newroom.enemies.Add(newenemy);
            newroom.clutters.Add(CollisionBox(80, 80, new Vector2(240, 0)));
            newroom.clutters.Add(CollisionBox(80, 80, new Vector2(240, 113)));
            newroom.clutters.Add(CollisionBox(320, 5, Vector2.Zero));  // Back wall
            rooms.Add(newroom);

            // Room 106
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 106, "Caveman 7");
            newroom.trap_room = true;
            newdoor = new Door(101, 144, 150, 144, 10);
            newroom.trap_door = newdoor;
            newroom.doors.Add(newdoor);  // Bottom door
            newenemy = RoomMaps.CaveMan();
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

        }

        public void LoadTileMaps(GraphicsDevice graphics)
        {
            foreach (Room r in rooms)
            {
                if (r.room_id <= 105)
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

        public static Enemy CaveMan(Vector2 newpos)
        {
            Enemy caveman = CaveMan();
            caveman.SetPosCentre(newpos);
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
                col_height = 27,
                collision_offset = new Vector2(0, 2),
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
                col_width= 15,
                col_height= 9,
                collision_offset = new Vector2(7,11)
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

        // A basic door with no sprite, direction tells you which wall to put it on in the default position
        public static Door BasicDoor(int newroomid, Directions placement, bool locked = false)
        {
            Vector2 doorPos = Vector2.Zero;
            Vector2 playerPos = Vector2.Zero;

            if (placement == Directions.Down){
                doorPos = new Vector2(144, 150);
                playerPos = new Vector2(144, 10);
            }
            else if (placement == Directions.Up)
            {
                doorPos = new Vector2(144, -15);
                playerPos = new Vector2(144, 110);
            }
            else if (placement == Directions.Left)
            {
                doorPos = new Vector2(-24, 72);
                playerPos = new Vector2(275, 72);
            }
            else
            {
                doorPos = new Vector2(305, 72);
                playerPos = new Vector2(10, 72);
            }
            Door door = new Door(newroomid, (int)doorPos.X, (int)doorPos.Y, (int)playerPos.X, (int)playerPos.Y, locked);
            door.draw = false;

            return door;
        }
    }

   
}
