#pragma once
#include <curl/curl.h>
#include <string>

#include "nlohmann/json.hpp"

// for convenience
using json = nlohmann::json;

class Connection
{
public:
    json sendPostRequest(std::string api, json data = {});
    json sendGetRequest(std::string api, json data = {});
    Connection(std::string host);
private:
    CURL* curl;
    CURLcode res;
    std::string hostURL;
    std::string readBuffer;

    void InitCurl();
    void setJsonData(json);
};

