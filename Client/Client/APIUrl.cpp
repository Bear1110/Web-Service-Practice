#include "APIUrl.h"

const std::string APIUrl::host = "https://localhost:44311/api";
const std::string APIUrl::playerURL = host+"/players/";
const std::string APIUrl::roomURL = host + "/rooms/";


APIUrl::APIUrl() {}

std::string APIUrl::createPlayer()
{
    return playerURL;
}

std::string APIUrl::getPlayers()
{
    return playerURL;
}

std::string APIUrl::getRooms()
{
    return roomURL;
}

std::string APIUrl::joinRoom( std::string roomid )
{
    return roomURL + "join/"+ roomid;
}

std::string APIUrl::createRoom()
{
    return roomURL + "create";
}


