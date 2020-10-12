#pragma once
#include <vector>
#include <unordered_map>
#include "Connection.h"
#include "APIUrl.h"

class Client
{
typedef void (Client::* ScriptFunction)(std::vector<std::string>); // function pointer type
public:
    void lookupCommand(std::vector<std::string>);
    Client();
private:
    std::string playerName;
    std::string myIP;
    unsigned int ID;
    std::unordered_map<std::string, ScriptFunction>unordered_dict;

    Connection netHelper;
    APIUrl url;

#pragma region Command
    void listPlayer(std::vector<std::string> unused = std::vector<std::string>());
    void showInfo(std::vector<std::string> unused = std::vector<std::string>());
    void listRoom(std::vector<std::string> unused = std::vector<std::string>());
    void createRoom(std::vector<std::string> unused = std::vector<std::string>());

    void join(std::vector<std::string>);
    void login(std::vector<std::string>);
#pragma endregion

};

