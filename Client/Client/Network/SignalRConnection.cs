using Client.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Client.Network
{
    class SignalRConnection
    {
        private readonly HubConnection connection;
        public SignalRConnection()
        {
            connection = new HubConnectionBuilder().WithUrl(APIUrl.signalR()).Build();
            connectionToServer();
        }

        private void registerEvent()
        {
            connection.On<Player>("ReceiveSomeoneOnline", (player) => Console.WriteLine(player.Name + " just Online."));
            connection.On<Player>("ReceiveOffline", (player) => Console.WriteLine(player.Name +" just Offline."));
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

        public async void SendOffline(Player player)
        {
            await connection.InvokeAsync("SomeoneOffline", player);
        }
    }
}
