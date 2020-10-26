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
    Connection();
private:
    CURL* curl = nullptr;
    CURLcode res = CURL_LAST;
    std::string readBuffer;

    void InitCurl();
    void setJsonData(json);
};

