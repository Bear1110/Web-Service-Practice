using Client.Models;
using Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Client
{
    class Client
    {
        private readonly JsonSerializerOptions jsonOption;
        private readonly Dictionary<string, Action<string[]>> commandHandler;
        private Player myself;
        private Room myroom;
        private string[] map;
        private SignalRConnection signalR;
        private bool isPlaying = false;

        private T DeserializeJson<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, jsonOption);
        public Client()
        {
            signalR = new SignalRConnection(this);
            jsonOption = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            commandHandler = new Dictionary<string, Action<string[]>>();
            commandHandler.Add("listplayer", ListPlayer);
            commandHandler.Add("login", Login);
            commandHandler.Add("listroom", ListRoom);
            commandHandler.Add("join", JoinRoom);
            commandHandler.Add("create", CreateRoom);
            commandHandler.Add("logout", Logout);
            commandHandler.Add("info", ShowMyInfo);
            commandHandler.Add("setup", SendStartGame);
            commandHandler.Add("attack", Attack);
        }

        #region command
        public void LookupCommand(string[] parameters)
        {
            string command = parameters[0];
            Action<string[]> outFunction = null;
            if (commandHandler.TryGetValue(command, out outFunction))
            {
                outFunction(parameters);
            }
            else
            {
                Console.WriteLine(command + " is not a command.");
                ListAllCommand();
            }
        }

        private void ListAllCommand()
        {
            Console.WriteLine("All comand is below.");
            foreach (var item in commandHandler.Keys)
            {
                Console.WriteLine(" - " + item);
            }
        }

        #endregion
        #region Player
        public void ShowMyInfo(params string[] unused) => Console.WriteLine(myself.ToString());

        private async void ListPlayer(params string[] parameters)
        {
            string jsonString = await ConnectionHelper.SendGetRequest(APIUrl.GetPlayers());
            var players = DeserializeJson<List<Player>>(jsonString);

            Console.WriteLine("There are " + players.Count + " plays.\n");
            players.ForEach(player => Console.WriteLine(player.ToString()));
        }


        private async void Login(params string[] parameters)
        {
            if(myself != null)
            {
                Console.WriteLine("You already logged in.");
                return;
            }
            var player = new Player { Name = parameters[1] };
            string jsonString = JsonSerializer.Serialize(player);
            jsonString = await ConnectionHelper.SendPostRequest(APIUrl.CreatePlayer(), jsonString);
            if (jsonString.Equals(ConnectionHelper.errorMessage)) return;
            myself = DeserializeJson<Player>(jsonString);
        }

        private void Logout(params string[] unused)
        {
            if (!CheckPlayer()) return;
            if (isPlaying)
            {
                Console.WriteLine("You cannot logout when playing.");
                return;
            }
            if (myroom != null)
                signalR.SendLeftRoom(myself,myroom);
            signalR.SendOffline(myself);
            myself = null;
            myroom = null;
        }

        private bool CheckPlayer()
        {
            if (myself == null)
            {
                Console.WriteLine("Login first!");
                return false;
            }
            return true;
        }
        #endregion
        #region room

        private async void JoinRoom(params string[] parameter)
        {
            if (!CheckPlayer()) return;
            if (CheckInRoom()) return;
            string jsonString = JsonSerializer.Serialize(myself);
            jsonString = await ConnectionHelper.SendPostRequest(APIUrl.JoinRoom(parameter[1]), jsonString);
            if (jsonString.Equals(ConnectionHelper.errorMessage)) return;
            myroom = DeserializeJson<Room>(jsonString);
            signalR.SendJoinRoom(myself,myroom);
        }

        private async void CreateRoom(params string[] unused)
        {
            if (!CheckPlayer()) return;
            if (CheckInRoom()) return;
            string jsonString = JsonSerializer.Serialize(myself);
            jsonString = await ConnectionHelper.SendPostRequest(APIUrl.CreateRoom(), jsonString);
            if (jsonString.Equals(ConnectionHelper.errorMessage)) return;
            myroom = DeserializeJson<Room>(jsonString);
            ListRoom();
            signalR.SendJoinRoom(myself, myroom);
        }

        private async void ListRoom(params string[] unused)
        {
            string jsonString = await ConnectionHelper.SendGetRequest(APIUrl.GetRooms());
            var rooms = DeserializeJson<List<Room>>(jsonString);
            Console.WriteLine("There are " + rooms.Count + " rooms.\n");
            rooms.ForEach(room => Console.WriteLine(room.ToString()));
        }

        private bool CheckInRoom() => myroom != null;
        #endregion
        #region game
        private async void SendStartGame(params string[] unused)
        {
            if (!CheckInRoom()) return;
            if (isPlaying) return;
            map = ReadTextHelper.ReadTextToMap();
            string jsonString = JsonSerializer.Serialize(map);
            await ConnectionHelper.SendPostRequest(APIUrl.StartGame(myroom.Id), jsonString);
        }

        public void ReceiveStartGame()
        {
            Console.WriteLine("Game Strat!!!");
            isPlaying = true;
        }

        public void ReceiveGameSet()
        {
            Console.WriteLine("Game Finish!!!");
            isPlaying = false;
        }

        private void Attack(params string[] xy)
        {
            if (!isPlaying) return;
            int x = Convert.ToInt32(xy[1]);
            int y = Convert.ToInt32(xy[2]);
            signalR.SendAttack(myroom, x, y);
        }

        public void PrintAttackResult(string[] map,string message)
        {
            foreach (var row in map)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine(message);
        }
        #endregion
    }
}
