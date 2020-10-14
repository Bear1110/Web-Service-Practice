namespace Client
{
    class APIUrl
    {
        private const string host = "https://localhost:44311";
        private const string playerURL = host+ "/api/players/";
        private const string roomURL = host + "/api/rooms/";
        private const string signalRChannel = host +"/ChatHub";

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

        public static string signalR()
        {
            return signalRChannel;
        }
    }
}
