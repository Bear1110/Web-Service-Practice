#pragma once
#include <string>

class APIUrl
{
public:
    APIUrl();

    //constexpr auto roomURL = "api/rooms";
    //constexpr auto host = "https://localhost:44311/api";
    std::string CreatePlayer();
    std::string GetPlayers();
    std::string GetRooms();
    std::string JoinRoom(std::string roomid);
    std::string CreateRoom();
    std::string StartGame(std::string roomId);
    std::string SignalR();
private:
    static const std::string host;
    static const std::string playerURL;
    static const std::string roomURL;
    static const std::string signalRChannel;
};
