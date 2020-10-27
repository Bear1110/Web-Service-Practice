#include "Client.h"
#include <iostream>

using namespace std;

Client::Client()
{
    myself = new Player();
    myself->Name = "undefined";
    myself->Ip = "255.255.255.255";
    myself->Id = 0;
    unordered_dict.emplace("login", &Client::login);
    unordered_dict.emplace("listPlayer", &Client::listPlayer);
    unordered_dict.emplace("listRoom", &Client::listRoom);
    unordered_dict.emplace("info", &Client::showInfo);
    unordered_dict.emplace("join", &Client::join);
    unordered_dict.emplace("create", &Client::createRoom);
}

void Client::lookupCommand(std::vector<std::string> parameters)
{
    unordered_map<string, ScriptFunction>::iterator it = unordered_dict.find(parameters[0]);
    if (it != unordered_dict.end()) {
        (this->*(it->second))(parameters); // (this->*unordered_dict[parameters[0]])(parameters);
    }
    else {
        cout << parameters[0] << " is not a command." << endl;
        listAllCommand();
    }
}

void Client::listAllCommand()
{
    cout << "All comand is below." << endl;
    for (auto i : unordered_dict)
        cout <<" - " <<i.first << endl;
}

void Client::join(std::vector<std::string> parameter)
{
    json jsonData = { {"id", myself->Id} };
    jsonData = netHelper.sendPostRequest(url.JoinRoom(parameter[1]), jsonData);
}

void Client::login(std::vector<string> parameter)
{
    json jsonData = {
        {"name", parameter[1]}
     };
    jsonData = netHelper.sendPostRequest(url.CreatePlayer(),jsonData);

    if (jsonData.type() == nlohmann::detail::value_t::null)
        return;
    myself->Name = jsonData["name"];
    myself->Ip = jsonData["ip"];
    myself->Id = jsonData["id"];
    showInfo();
}

void Client::listPlayer(vector<string> command) {
    json jsonData = netHelper.sendGetRequest(url.GetPlayers());
    cout << "There are " << jsonData.size() << " plays.\n";
    for (auto& player : jsonData)
    {
        cout << player << endl;
    }
}

void Client::showInfo(std::vector<std::string> s)
{
    cout << "ID: " << myself->Id << endl;
    cout << "Name: " + myself->Name << endl;
    cout << "IP: " + myself->Ip << endl;
}

void Client::listRoom(std::vector<std::string> unused)
{
    auto jsonData = netHelper.sendGetRequest(url.GetRooms());
    cout << "There are " << jsonData.size() << " rooms.\n";
    for (auto& room : jsonData)
    {
        cout << room << endl;
    }
}

void Client::createRoom(std::vector<std::string> unused)
{
    json jsonData = { {"id", myself->Id} };
    jsonData = netHelper.sendPostRequest(url.CreateRoom(), jsonData);
    listRoom();
}