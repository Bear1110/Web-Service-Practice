#pragma once
#include <vector>
#include <unordered_map>
#include "Connection.h"
#include "APIUrl.h"
#include "Player.h"
#include "SignalRConnection.h"

class Client
{
typedef void (Client::* ScriptFunction)(std::vector<std::string>); // function pointer type
public:
    void lookupCommand(std::vector<std::string>);
    Client();
private:
    Player *myself;
    std::unordered_map<std::string, ScriptFunction>unordered_dict;

    Connection netHelper;
    APIUrl url;
    SignalRConnection e;

#pragma region Command
    void listAllCommand();
    void listPlayer(std::vector<std::string> unused = std::vector<std::string>());
    void showInfo(std::vector<std::string> unused = std::vector<std::string>());
    void listRoom(std::vector<std::string> unused = std::vector<std::string>());
    void createRoom(std::vector<std::string> unused = std::vector<std::string>());

    void join(std::vector<std::string>);
    void login(std::vector<std::string>);
#pragma endregion

};

