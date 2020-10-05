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

            // Room 2
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 2, "Time Machine");
            newroom.doors.Add(BasicDoor(100, Directions.Left));
            newclutter = new Clutter()
            {
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "TimeMachine"), 128, 112),
                pos = new Vector2(96, 0),
                draw_width = 128,
                col_width = 125,
                draw_height = 110,
                col_height = 15,
                collision_offset = new Vector2(0, 85),
                invulnerable = true,
                hp = 1,
                dead = false,
                draw = true,
                shootable = true,
                ignore_collision = false
            };
            newroom.clutters.Add(newclutter);
            newroom.clutters.Add(FloorCollisionBox(96, 20, new Vector2(96, 128)));
            newroom.clutters.Add(FloorCollisionBox(1, 30, new Vector2(96, 100)));
            newroom.clutters.Add(FloorCollisionBox(1, 43, new Vector2(222, 100)));
            rooms.Add(newroom);

            // Room 100
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 100, "Caveman 1");
            newroom.doors.Add(BasicDoor(101,Directions.Left)); // Left door
            newroom.doors.Add(BasicDoor(2, Directions.Right));    // Right door
            newroom.enemies.Add(CaveMan(new Vector2(160, 90)));
            newroom.clutters.Add(CollisionBox(320, 24, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(46, 70, Vector2.Zero));  // Top left
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(0, 114)));  // bottom left            
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(274, 0)));  // Top right
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(274, 114)));  // bottom right
            newroom.clutters.Add(FloorCollisionBox(61, 15, new Vector2(82, 30))); // Left bench
            newroom.clutters.Add(FloorCollisionBox(61, 15, new Vector2(177, 30))); // Right bench
            rooms.Add(newroom);

            // Room 101
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 101, "Caveman 2");
            newroom.doors.Add(BasicDoor(102, Directions.Left)); // Left door
            newroom.doors.Add(BasicDoor(100, Directions.Right));  // Right door
            newroom.doors.Add(BasicDoor(106, Directions.Up));  // Top door
            newroom.enemies.Add(CaveMan(new Vector2(80, 60)));
            newroom.enemies.Add(CaveMan(new Vector2(80, 120)));
            newroom.clutters.Add(CollisionBox(140, 24, Vector2.Zero));  // Back wall 1
            newroom.clutters.Add(CollisionBox(140, 24, new Vector2(180,0)));  // Back wall 2
            newroom.clutters.Add(CollisionBox(46, 70, Vector2.Zero));  // Top left
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(0, 114)));  // bottom left            
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(274, 0)));  // Top right
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(274, 114)));  // bottom right
            newroom.clutters.Add(FloorCollisionBox(10, 15, new Vector2(66, 30))); // Left washer
            newroom.clutters.Add(FloorCollisionBox(9, 15, new Vector2(114, 30))); // Right washer
            newroom.clutters.Add(FloorCollisionBox(61, 15, new Vector2(193, 30))); // Right bench

            rooms.Add(newroom);

            // Room 102
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 102, "Caveman 3");
            newroom.doors.Add(new Door(103, -24, 72, 275, 72)); // Left door
            newroom.doors.Add(new Door(101, 305, 72, 10, 72));    // Right door
            newdoor = new Door()
            {
                pos = new Vector2(144, 0),
                draw_width = 32,
                draw_height = 32,
                col_width = 8,
                col_height = 30,
                collision_offset = new Vector2(11, 0),
                Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "Door"), 32, 32), //todo animate this door, ?add lock symbol
                openAnimation = new Animation(18, 18, 1),
                unlockAnimation = new Animation(1, 18, 2000),
                closedAnimation = new Animation(1, 1, 1),
                lockAnimation = new Animation(1, 18, 2000),
                anim_is_fade = false,
                next_room_id = 107,
                next_posX = 144,
                next_posY = 110,
                ignore_collision = true,
                unlock_id = 1,
                locked = false,
                animated = true,
                open_time = 2
            };
            newroom.doors.Add(newdoor);
            newroom.doors.Add(new Door(112, 144, 150, 144, 10));  // Bottom door
            newroom.clutters.Add(CollisionBox(127, 54, Vector2.Zero));  // Top left wall
            newroom.clutters.Add(CollisionBox(140, 54, new Vector2(193, 0)));  // Top right wall
            newroom.clutters.Add(CollisionBox(320, 24, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(125, 42, new Vector2(0,114)));  // Bottom left wall
            newroom.clutters.Add(CollisionBox(125, 42, new Vector2(193,114)));  // Bottom right wall
            rooms.Add(newroom);

            // Room 103 - Room where a button is hidden in a destructable crate
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 103, "Caveman 4");
            newdoor = BasicDoor(104, Directions.Left, true); // Left door
            newdoor.Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "ForceField"), 16, 32); //todo animate this door
            newdoor.openAnimation = new Animation(1, 7, 1000);
            newdoor.animated = true;
            newdoor.anim_is_fade = true;
            newdoor.draw = true;
            newdoor.pos += new Vector2(25, 8);
            newdoor.draw_if_unlocked = true;
            newdoor.open_time = 1;
            newdoor.col_width = 15;
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
            newroom.doors.Add(BasicDoor(103,Directions.Right)); 
            newroom.doors.Add(BasicDoor(105,Directions.Left));  
            newroom.doors.Add(BasicDoor(108,Directions.Up)); 
            newroom.doors.Add(BasicDoor(114,Directions.Down));
            newroom.clutters.Add(CollisionBox(142, 55, Vector2.Zero));  // Top left
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(0, 114)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 0)));  // Top right
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 114)));  // bottom right
            rooms.Add(newroom);

            // Room 105
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 105, "Caveman 6");
            newroom.trap_room = true;
            newdoor = TrapDoor(104, Directions.Right);
            newroom.trap_door = newdoor;
            newdoor.pos.Y += 8;
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
            newroom.clutters.Add(CollisionBox(80, 72, new Vector2(242, 0))); //topright
            newroom.clutters.Add(CollisionBox(80, 80, new Vector2(242, 113)));
            newroom.clutters.Add(CollisionBox(320, 24, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(15, 180, Vector2.Zero));  //  left wall
            newroom.clutters.Add(CollisionBox(320, 5, new Vector2(0, 146)));  // bottom    
            rooms.Add(newroom);

            // Room 106
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 106, "Caveman 7");
            newroom.trap_room = true;
            newdoor = TrapDoor(101, Directions.Down);
            newroom.trap_door = newdoor;
            newroom.doors.Add(newdoor);  // Bottom door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 30);
            newroom.enemies.Add(newenemy);
            newroom.clutters.Add(CollisionBox(320, 39, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(95, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(100, 180, new Vector2(224, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(0, 130)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 130)));  // bottom right
            rooms.Add(newroom);

            // Room 107
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 107, "BOSS ROOM");
            newroom.doors.Add(new Door(102, 144, 150, 144, 50));  // Bottom door
            newroom.enemies.Add(CaveBorg(new Vector2(160, 90)));
            newroom.trap_room = true;
            newdoor = TrapDoor(109, Directions.Up);
            newroom.trap_door = newdoor;
            newroom.doors.Add(newdoor);
            newroom.clutters.Add(CollisionBox(140, 24, Vector2.Zero));  // Back wall 1
            newroom.clutters.Add(CollisionBox(140, 24, new Vector2(180, 0)));  // Back wall 2
            newroom.clutters.Add(CollisionBox(15, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(5, 180, new Vector2(306, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 5, new Vector2(0, 146)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 5, new Vector2(178, 146)));  // bottom right
            rooms.Add(newroom);

            // Room 108
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 108, "Caveman 9");
            newroom.doors.Add(BasicDoor(104,Directions.Down));  // Bottom door
            newroom.enemies.Add(CaveMan(new Vector2(160, 90)));
            newroom.doors.Add(BasicDoor(110,Directions.Up));
            newroom.clutters.Add(CollisionBox(140, 24, Vector2.Zero));  // Back wall 1
            newroom.clutters.Add(CollisionBox(140, 24, new Vector2(180, 0)));  // Back wall 2
            newroom.clutters.Add(CollisionBox(15, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(5, 180, new Vector2(306, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 5, new Vector2(0, 146)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 5, new Vector2(178, 146)));  // bottom right
            rooms.Add(newroom);

            // Room 109
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 109, "PRIZE ROOM");
            newroom.doors.Add(BasicDoor(107, Directions.Down));  // Bottom door
            newroom.clutters.Add(CollisionBox(320, 39, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(95, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(100, 180, new Vector2(224, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(0, 130)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 130)));  // bottom right
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
            newroom.doors.Add(BasicDoor(108, Directions.Down));  // Bottom door
            newroom.clutters.Add(CollisionBox(320, 39, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(95, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(100, 180, new Vector2(224, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(0, 130)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 130)));  // bottom right
            newroom.clutters.Add(Key(new Vector2(100, 100), 1));

            rooms.Add(newroom);

            // Room 111
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 111, "PASSWORD ROOM");
            newroom.doors.Add(BasicDoor(115, Directions.Down));  // Bottom door
            newroom.clutters.Add(CollisionBox(320, 39, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(95, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(100, 180, new Vector2(224, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(0, 130)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(178, 130)));  // bottom right
            rooms.Add(newroom);

            // Room 112
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 112, "Caveman 9");
            newroom.doors.Add(BasicDoor(113, Directions.Left));
            newdoor = BasicDoor(102, Directions.Up);
            newdoor.col_width += 5;
            newroom.doors.Add(newdoor);
            newdoor = BasicDoor(116, Directions.Down);
            newdoor.col_width += 5;
            newroom.doors.Add(newdoor);
            newroom.clutters.Add(CollisionBox(142, 55, Vector2.Zero));  // Top left
            newroom.clutters.Add(CollisionBox(142, 55, new Vector2(0, 114)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 180, new Vector2(194, 0)));  // Top right
            rooms.Add(newroom);

            // Room 113
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 113, "Caveman 10");
            newroom.trap_room = true;
            newdoor = TrapDoor(112, Directions.Right);
            newroom.trap_door = newdoor;
            newdoor.pos.Y += 8;
            newroom.doors.Add(newdoor);    // Right door
            newenemy = RoomMaps.Cyborg();
            newenemy.SetPosCentre(80, 40);
            newroom.enemies.Add(newenemy);
            newenemy = RoomMaps.Cyborg();
            newenemy.SetPosCentre(80, 80);
            newroom.enemies.Add(newenemy);
            newenemy = RoomMaps.Cyborg(); 
            newenemy.SetPosCentre(80, 120);
            newroom.enemies.Add(newenemy);
            newroom.clutters.Add(CollisionBox(80, 72, new Vector2(242, 0))); //topright
            newroom.clutters.Add(CollisionBox(80, 80, new Vector2(242, 113)));
            newroom.clutters.Add(CollisionBox(320, 24, Vector2.Zero));  // Back wall
            newroom.clutters.Add(CollisionBox(15, 180, Vector2.Zero));  //  left wall
            newroom.clutters.Add(CollisionBox(320, 5, new Vector2(0, 146)));  // bottom    
            rooms.Add(newroom);

            // Room 114
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 114, "Caveman 11");
            newroom.trap_room = true;
            newdoor = TrapDoor(104, Directions.Up);
            newroom.trap_door = newdoor;
            newroom.doors.Add(newdoor);  // Bottom door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 30);
            newroom.enemies.Add(newenemy);
            newroom.clutters.Add(CollisionBox(320, 39, new Vector2(0, 130)));  // Back wall
            newroom.clutters.Add(CollisionBox(95, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(100, 180, new Vector2(224, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 39, new Vector2(0, 0)));  // top left            
            newroom.clutters.Add(CollisionBox(142, 39, new Vector2(178, 0)));  // top right
            rooms.Add(newroom);

            // Room 115
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 115, "Caveman 12");
            newroom.doors.Add(BasicDoor(116, Directions.Left)); // Left door
            newroom.doors.Add(BasicDoor(111, Directions.Up));  // Top door
            newroom.enemies.Add(CaveMan(new Vector2(80, 60)));
            newroom.enemies.Add(CaveMan(new Vector2(80, 120)));
            newroom.clutters.Add(CollisionBox(140, 24, Vector2.Zero));  // Back wall 1
            newroom.clutters.Add(CollisionBox(140, 24, new Vector2(180, 0)));  // Back wall 2
            newroom.clutters.Add(CollisionBox(46, 70, Vector2.Zero));  // Top left
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(0, 114)));  // bottom left            
            newroom.clutters.Add(CollisionBox(46, 180, new Vector2(274, 0)));  // Top right
            //newroom.clutters.Add(FloorCollisionBox(10, 15, new Vector2(66, 30))); // Left washer
            //newroom.clutters.Add(FloorCollisionBox(9, 15, new Vector2(114, 30))); // Right washer
            //newroom.clutters.Add(FloorCollisionBox(61, 15, new Vector2(193, 30))); // Right bench

            // Room 116
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 116, "Caveman 13");
            newroom.doors.Add(BasicDoor(117, Directions.Down));  // Bottom door
            newroom.doors.Add(BasicDoor(112, Directions.Up));
            newroom.clutters.Add(CollisionBox(140, 24, Vector2.Zero));  // Back wall 1
            newroom.clutters.Add(CollisionBox(140, 24, new Vector2(180, 0)));  // Back wall 2
            newroom.clutters.Add(CollisionBox(15, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(306, 0)));  // Top right
            newroom.clutters.Add(CollisionBox(46, 70, new Vector2(306, 114)));  // bottom right
            newroom.clutters.Add(CollisionBox(142, 5, new Vector2(0, 146)));  // bottom left            
            newroom.clutters.Add(CollisionBox(142, 5, new Vector2(178, 146)));  // bottom right
            rooms.Add(newroom);

            // Room 117
            newroom = new Room(320, 160, new List<Enemy>(), new List<Door>(), 117, "Caveman 14");
            newroom.trap_room = true;
            newdoor = TrapDoor(116, Directions.Up);
            newroom.trap_door = newdoor;
            newroom.doors.Add(newdoor);  // Bottom door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 30);
            newroom.enemies.Add(newenemy);
            newroom.clutters.Add(CollisionBox(320, 39, new Vector2(0, 130)));  // Back wall
            newroom.clutters.Add(CollisionBox(95, 180, Vector2.Zero));  //  left
            newroom.clutters.Add(CollisionBox(100, 180, new Vector2(224, 0)));  //  right
            newroom.clutters.Add(CollisionBox(142, 39, new Vector2(0, 0)));  // top left            
            newroom.clutters.Add(CollisionBox(142, 39, new Vector2(178, 0)));  // top right
            rooms.Add(newroom);

        }

        public void LoadTileMaps(GraphicsDevice graphics)
        {
            foreach (Room r in rooms)
            {
                if (r.room_id <= 117)
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

        public static Enemy Cyborg()
        {
            Enemy cyborg = new Enemy
            {
                hp = 5,
                speed = 20,
                range = 75,
                enemyType = EnemyType.Cyborg,
                attack_cooldown = 2000
            };
            cyborg.SetSprite();
            cyborg.SetAnimations();
            return cyborg;
        }

        public static Enemy CaveBorg()
        {
            Enemy caveborg = new Enemy
            {
                hp = 20,
                speed = 30,
                range = 75,
                enemyType = EnemyType.CaveBorg,
                attack_cooldown = 1000
            };
            caveborg.SetSprite();
            caveborg.SetAnimations();
            return caveborg;
        }

        public static Enemy CaveMan(Vector2 newpos)
        {
            Enemy caveman = CaveMan();
            caveman.SetPosCentre(newpos);
            return caveman;
        }

        public static Enemy Cyborg(Vector2 newpos)
        {
            Enemy cyborg = Cyborg();
            cyborg.SetPosCentre(newpos);
            return cyborg;
        }

        public static Enemy CaveBorg(Vector2 newpos)
        {
            Enemy caveborg = CaveBorg();
            caveborg.SetPosCentre(newpos);
            return caveborg;
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

        // Collision box that can only collide with player's feet
        public static Clutter FloorCollisionBox(int width, int height, Vector2 position)
        {
            Clutter collisionBox = new Clutter()
            {
                col_width = width,
                col_height = height,
                draw_height = 0,
                draw_width = 0,
                ignore_collision = false,
                pos = position,
                shootable = false,
                floor_collision = true,
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
        
        public static Door TrapDoor(int newroomid, Directions placement, bool locked = false)
        {
            Door newdoor;
            newdoor = BasicDoor(newroomid, placement, false);

            if (placement == Directions.Right || placement == Directions.Left)
                newdoor.Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "ForceField"), 16, 32); //todo animate this door
            else
                newdoor.Sprite = new AnimatedSprite(ModManager.Instance.AssetManager.LoadTexture2D(Globals.GraphicsDevice, "ForceFieldRot"), 32, 16); //todo animate this door


            newdoor.openAnimation = new Animation(1, 8, 500);
            newdoor.animated = true;
            newdoor.anim_is_fade = true;
            newdoor.draw = true;
            newdoor.draw_if_unlocked = true;
            newdoor.open_time = 2;


            if (placement == Directions.Up)
            {
                newdoor.pos.Y += 20;
                newdoor.col_height -= 15;
            }
                return newdoor;
        }
    
    }
   
}
