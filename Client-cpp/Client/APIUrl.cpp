#include "APIUrl.h"

const std::string APIUrl::host = "https://localhost:44311";
const std::string APIUrl::playerURL = host+"/api/players/";
const std::string APIUrl::roomURL = host + "/api/players/";
const std::string APIUrl::signalRChannel = host + "/ChatHub";


APIUrl::APIUrl() {}

std::string APIUrl::CreatePlayer()
{
    return playerURL;
}

std::string APIUrl::GetPlayers()
{
    return playerURL;
}

std::string APIUrl::GetRooms()
{
    return roomURL;
}

std::string APIUrl::JoinRoom( std::string roomid )
{
    return roomURL + "join/"+ roomid;
}

std::string APIUrl::CreateRoom()
{
    return roomURL + "create";
}

std::string APIUrl::StartGame(std::string roomId)
{
    return roomURL + "start/" + roomId;
}

std::string APIUrl::SignalR()
{
    return signalRChannel;
}

