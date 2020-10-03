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
            var caveman = new Enemy();
            caveman.hp = 5;
            caveman.speed = 20;
            caveman.range = 50;
            caveman.enemyType = EnemyType.Caveman;


            newroom = new Room(320, 180, new List<Enemy>(), new List<Door>(), 100, "Caveman 1");
            newroom.doors.Add(new Door(101, -24, 72, 280, 72)); // Left door
            newroom.doors.Add(new Door(0, 312, 72, 40, 72));    // Right door
            newenemy = caveman;
            newenemy.SetPosCentre(160, 90);
            newroom.enemies.Add(caveman);
            rooms.Add(newroom);

        }

    }
}
