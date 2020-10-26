using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.GameClasses
{
    public class MapInfo
    {
        private Room room;
        private bool[,] shipMap = new bool[10, 10];
        private int[,] gameMap = new int[10, 10]; // 0 = '?' , 1 = '-' , 2 = 'O'
        private int shipCount;
        private int hitCount;
        private readonly char[] icon = new char[3] { '?', '-', 'O' };


        public MapInfo(Room room, string[] map)
        {
            this.room = room;
            hitCount = 0;
            shipCount = 0;
            int i = 0;
            foreach( var row in map)
            {
                for(int j = 0; j < 10; j++)
                {
                    shipMap[i,j] = row[j] == '1';
                    if(shipMap[i,j])
                        shipCount++;
                }
                i++;
            }
        }

        public string Attack(int x, int y)
        {
            string message = $"X:{x} Y:{y} ";
            if (x > 9 || x < 0 || y > 9 || y < 0)
                return message + "already beyond border.";
            if (gameMap[x,y] == 0) // unknow area
            {
                if(shipMap[x, y])
                {
                    gameMap[x, y] = 2;
                    hitCount++;
                    message += "You hit the ship.";
                }else
                {
                    gameMap[x, y] = 1;
                    message += "Nothing here.";
                }
            }
            else //already attacked
            {
                message += "You already fired this place.";
            }
            return message + BoatInfo();
        }

        public string[] VisualizeMap()
        {
            string[] map = new string[10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    map[i] += icon[gameMap[i,j]];
            return map;
        }

        public bool GameSet()
        {
            return shipCount == hitCount;
        }

        private string BoatInfo()
        {
            return $" Hit {hitCount} left ${shipCount - hitCount} boat.";
        }


    }
}
