namespace Client
{
    class APIUrl
    {
        private const string host = "https://localhost:44311";
        private const string playerURL = host+ "/api/players/";
        private const string roomURL = host + "/api/rooms/";
        private const string signalRChannel = host +"/ChatHub";

        public static string CreatePlayer()
        {
            return playerURL;
        }

        public static string GetPlayers()
        {
            return playerURL;
        }

        public static string GetRooms()
        {
            return roomURL;
        }

        public static string JoinRoom(string roomid)
        {
            return roomURL + "join/" + roomid;
        }

        public static string CreateRoom()
        {
            return roomURL + "create";
        }

        public static string StartGame(int roomId)
        {
            return roomURL + "Start/"+ roomId.ToString();
        }

        public static string SignalR()
        {
            return signalRChannel;
        }
    }
}
