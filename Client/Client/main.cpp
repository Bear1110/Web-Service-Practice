#include <iostream>
#include <sstream>
#include "Client.h"

using namespace std;
vector<string> prepocessCommand(string& rawCommand);

int main()
{
    Client client;
    cout << "Please input command.\n";
    while (true)
    {
        string inputString;
        getline(cin, inputString);
        if (inputString == "")
            continue;
        vector<string> parameters = prepocessCommand(inputString);
        client.lookupCommand(parameters);
    }
    return 0;
}

vector<string> prepocessCommand(string& rawCommand)
{
    istringstream in(rawCommand);
    vector<string> v;
    string t;
    while (in >> t) {
        v.push_back(t);
    }
    return v;
}