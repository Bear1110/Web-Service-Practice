#pragma once
#include <string>

class APIUrl
{
public:
    APIUrl();

    //constexpr auto roomURL = "api/rooms";
    //constexpr auto host = "https://localhost:44311/api";
    std::string createPlayer();
    std::string getPlayers();
    std::string getRooms();
    std::string joinRoom(std::string roomid);
    std::string createRoom();
private:
    static const std::string host;
    static const std::string playerURL;
    static const std::string roomURL;
};
