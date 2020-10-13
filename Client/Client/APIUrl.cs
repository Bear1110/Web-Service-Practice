using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    class APIUrl
    {
        private const string host = "https://localhost:44311/api";
        private const string playerURL = host+"/players/";
        private const string roomURL = host + "/rooms/";
        public static string createPlayer()
        {
            return playerURL;
        }

        public static string getPlayers()
        {
            return playerURL;
        }

        public static string getRooms()
        {
            return roomURL;
        }

        public static string joinRoom(string roomid)
        {
            return roomURL + "join/" + roomid;
        }

        public static string createRoom()
        {
            return roomURL + "create";
        }
    }
}
