using Client.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Client.Network
{
    class SignalRConnection
    {
        private readonly HubConnection connection;
        private Client client;

        public SignalRConnection(Client client)
        {
            this.client = client;
            connection = new HubConnectionBuilder()
                .WithUrl(APIUrl.SignalR())
                .Build();
            connectionToServer();
        }

        private void registerEvent()
        {
            connection.On<Player>("ReceiveSomeoneOnline", (player) => Console.WriteLine(player.Name + " just Online."));
            connection.On<Player>("ReceiveOffline", (player) => Console.WriteLine(player.Name +" just Offline."));
            connection.On<Player>("ReceiveSomeoneJoinRoom", (player) => Console.WriteLine(player.Name + " join Room."));
            connection.On<Player>("ReceiveLeaveJoinRoom", (player) => Console.WriteLine(player.Name + " left Room."));
            connection.On<string[],string>("ReceiveAttackResult", client.PrintAttackResult);
            connection.On("ReceiveStartGame", client.ReceiveStartGame);
            connection.On("ReceiveGameSet", client.ReceiveGameSet);
        }

        private async void connectionToServer()
        {
            registerEvent();
            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #region SendMessage
        public async void SendOffline(Player player)
        {
            await connection.InvokeAsync("SomeoneOffline", player);
        }

        public async void SendJoinRoom(Player player, Room room)
        {
            await connection.InvokeAsync("JoinToRoom", player, room);
        }

        public async void SendLeftRoom(Player player, Room room)
        {
            await connection.InvokeAsync("LeaveFromRoom", player, room);
        }
        public async void SendAttack(Room room, int x, int y)
        {
            await connection.InvokeAsync("Attack", room, x, y);
        }
        #endregion
    }
}
