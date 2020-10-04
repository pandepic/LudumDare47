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
            newroom.doors.Add(new Door(111, 144, 170, 144, 40));  // Bottom door
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
            newroom.doors.Add(new Door(111, 144, -24, 144, 130, true));  // Top door
            newroom.doors.Add(new Door(111, 144, 170, 144, 40));  // Bottom door
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
            newroom.doors.Add(new Door(111, 144, -24, 144, 130)); // Top door
            // BOSS
            rooms.Add(newroom);

            // Room 108
            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 107, "BOSS ROOM");
            newroom.doors.Add(new Door(102, 144, 170, 144, 40));  // Bottom door
            newroom.doors.Add(new Door(111, 144, -24, 144, 130)); // Top door
            // BOSS
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
