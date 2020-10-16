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
        private T DeserializeJson<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, jsonOption);
        SignalRConnection signalR;

        public Client()
        {
            signalR = new SignalRConnection();
            jsonOption = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            commandHandler = new Dictionary<string, Action<string[]>>();
            commandHandler.Add("listplayer", ListPlayer);
            commandHandler.Add("login", Login);
            commandHandler.Add("listroom", ListRoom);
            commandHandler.Add("join", JoinRoom);
            commandHandler.Add("create", CreateRoom);
            commandHandler.Add("logout", Logout);
            commandHandler.Add("info", ShowMyInfo);
        }


        public void ShowMyInfo(params string[] unused) => Console.WriteLine(myself.ToString());

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

        private async void ListPlayer(params string[] parameters)
        {
            string jsonString = await ConnectionHelper.SendGetRequest(APIUrl.getPlayers());
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
            jsonString = await ConnectionHelper.SendPostRequest(APIUrl.createPlayer(), jsonString);
            if (jsonString.Equals(ConnectionHelper.errorMessage))
                return;
            myself = DeserializeJson<Player>(jsonString);
        }

        private async void Logout(params string[] unused)
        {
            if (myself == null)
            {
                Console.WriteLine("You are already logged out.");
                return;
            }
            signalR.SendOffline(myself);
            myself = null;
        }

        #region room

        private async void JoinRoom(params string[] parameter)
        {
            string jsonString = JsonSerializer.Serialize(myself);
            jsonString = await ConnectionHelper.SendPostRequest(APIUrl.joinRoom(parameter[1]), jsonString);
            if (jsonString.Equals(ConnectionHelper.errorMessage))
                return;
        }

        private async void CreateRoom(params string[] unused)
        {
            string jsonString = JsonSerializer.Serialize(myself);
            jsonString = await ConnectionHelper.SendPostRequest(APIUrl.createRoom(), jsonString);
            ListRoom();
        }

        private async void ListRoom(params string[] unused)
        {
            string jsonString = await ConnectionHelper.SendGetRequest(APIUrl.getRooms());
            var rooms = DeserializeJson<List<Room>>(jsonString);
            Console.WriteLine("There are " + rooms.Count + " rooms.\n");
            rooms.ForEach(room => Console.WriteLine(room.ToString()));
        }
        #endregion
    }
}
