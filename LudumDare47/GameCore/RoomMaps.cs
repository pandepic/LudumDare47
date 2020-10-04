using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using GameCore.Entities;

namespace GameCore
{
    public class RoomMaps
    {
        public List<Room> rooms = new List<Room>();

        public RoomMaps()
        {
            Room newroom;
            Enemy newenemy;

            // Room 100
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 100, "Caveman 1");
            newroom.doors.Add(new Door(101, -24, 72, 250, 72)); // Left door
            newroom.doors.Add(new Door(0, 312, 72, 40, 72));    // Right door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(160, 90);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 101
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 101, "Caveman 2");
            newroom.doors.Add(new Door(102, -24, 72, 250, 72)); // Left door
            newroom.doors.Add(new Door(100, 312, 72, 40, 72));  // Right door
            newroom.doors.Add(new Door(106, 144, -24, 144, 130));  // Top door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 50);
            newroom.enemies.Add(newenemy);
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 80);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 102
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 102, "Caveman 3");
            newroom.doors.Add(new Door(103, -24, 72, 250, 72)); // Left door
            newroom.doors.Add(new Door(101, 312, 72, 40, 72));    // Right door
            newroom.doors.Add(new Door(107, 144, -24, 144, 130));  // Top door
            newroom.doors.Add(new Door(112, 144, 170, 144, 40));  // Bottom door
            rooms.Add(newroom);

            // Room 103
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 103, "Caveman 4");
            newroom.doors.Add(new Door(104, -24, 72, 250, 72)); // Left door
            newroom.doors.Add(new Door(102, 312, 72, 40, 72));    // Right door
            rooms.Add(newroom);

            // Room 104
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 104, "Caveman 5");
            newroom.doors.Add(new Door(105, -24, 72, 250, 72)); // Left door
            newroom.doors.Add(new Door(103, 312, 72, 40, 72));    // Right door
            newroom.doors.Add(new Door(108, 144, -24, 144, 130));  // Top door
            newroom.doors.Add(new Door(114, 144, 170, 144, 40));  // Bottom door
            rooms.Add(newroom);

            // Room 105
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 105, "Caveman 6");
            newroom.doors.Add(new Door(104, 312, 72, 40, 72));    // Right door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 80);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 106
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 106, "Caveman 7");
            newroom.doors.Add(new Door(101, 144, 170, 144, 40));  // Bottom door
            newenemy = RoomMaps.CaveMan();
            newenemy.SetPosCentre(80, 30);
            newroom.enemies.Add(newenemy);
            rooms.Add(newroom);

            // Room 107
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 107, "BOSS ROOM");
            newroom.doors.Add(new Door(102, 144, 170, 144, 40));  // Bottom door
            newroom.doors.Add(new Door(109, 144, -24, 144, 100)); // Top door
            rooms.Add(newroom);

            // Room 108
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 108, "Caveman 8");
            newroom.doors.Add(new Door(104, 144, 170, 144, 40));  // Bottom door
            newroom.doors.Add(new Door(110, 144, -24, 144, 130)); // Top door
            rooms.Add(newroom);

            // Room 109
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 109, "PUZZLE ROOM");
            newroom.doors.Add(new Door(107, 144, 170, 144, 40));  // Bottom door
            rooms.Add(newroom);

            // Room 110
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 110, "KEY ROOM");
            newroom.doors.Add(new Door(108, 144, 170, 144, 40));  // Bottom door
            rooms.Add(newroom);

            // Room 111
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 111, "PASSWORD ROOM");
            newroom.doors.Add(new Door(115, 144, 170, 144, 40));  // Bottom door
            rooms.Add(newroom);

            // Room 112
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 112, "Caveman 9");
            newroom.doors.Add(new Door(116, 144, 170, 144, 40));  // Bottom door
            newroom.doors.Add(new Door(102, 144, -24, 144, 130)); // Top door
            newroom.doors.Add(new Door(113, -24, 72, 250, 72));   // Left door
            rooms.Add(newroom);

            // Room 113
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 113, "Caveman 10");
            newroom.doors.Add(new Door(112, 312, 72, 40, 72));    // Right door
            rooms.Add(newroom);

            // Room 114
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 114, "Caveman 11");
            newroom.doors.Add(new Door(104, 144, -24, 144, 130)); // Top door
            rooms.Add(newroom);

            // Room 115
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 115, "Caveman 12");
            newroom.doors.Add(new Door(116, -24, 72, 250, 72));   // Left door
            newroom.doors.Add(new Door(111, 144, -24, 144, 130)); // Top door
            rooms.Add(newroom);

            // Room 116
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 116, "Caveman 13");
            newroom.doors.Add(new Door(117, 144, 170, 144, 40));  // Bottom door
            newroom.doors.Add(new Door(112, 144, -24, 144, 130)); // Top door
            newroom.doors.Add(new Door(115, 312, 72, 40, 72));    // Right door
            rooms.Add(newroom);

            // Room 117
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 117, "Caveman 14");
            newroom.doors.Add(new Door(116, 144, -24, 144, 130)); // Top door
            rooms.Add(newroom);
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
            return caveman;
        }

        public static Clutter Wall()
        {
            Clutter wall = new Clutter();

            return wall;
        }

    }

   
}
