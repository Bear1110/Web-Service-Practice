#include "Connection.h"
#include <iostream>
#include <sstream>

using namespace std;

Connection::Connection(std::string host) : hostURL(host), curl(nullptr),res(CURL_LAST) {}

json Connection::sendPostRequest(string api, json data)
{
    InitCurl();
    if (!curl) return json();
    curl_easy_setopt(curl, CURLOPT_URL, (hostURL + api).c_str());
    setJsonData(data);
    res = curl_easy_perform(curl);

    if (res != CURLE_OK)
        fprintf(stderr, "curl_easy_perform() failed: %s\n",
            curl_easy_strerror(res));
    curl_easy_cleanup(curl);
    return json::parse(readBuffer);
}

json Connection::sendGetRequest(string api, json data)
{
    InitCurl();
    if (!curl) return json();
    curl_easy_setopt(curl, CURLOPT_URL, (hostURL+ api).c_str());
    res = curl_easy_perform(curl);

    if (res != CURLE_OK)
        fprintf(stderr, "curl_easy_perform() failed: %s\n",
            curl_easy_strerror(res));
    curl_easy_cleanup(curl);
    try {
        return json::parse(readBuffer);
    }
    catch (...) {
        cout << readBuffer << endl;
        return json();
    }
}

size_t WriteCallback(char* contents, size_t size, size_t nmemb, void* userp)
{
    ((string*)userp)->append((char*)contents, size * nmemb);
    return size * nmemb;
}

void Connection::InitCurl()
{
    curl = nullptr;
    res = CURLE_NOT_BUILT_IN;
    curl = curl_easy_init();
    curl_easy_setopt(curl, CURLOPT_SSL_VERIFYPEER, 0L);
    //curl_easy_setopt(curl, CURLOPT_SSL_VERIFYHOST, 0L);
    readBuffer = "";
    curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
    curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
}

void Connection::setJsonData(json jsonData)
{
    struct curl_slist* hs = NULL;
    hs = curl_slist_append(hs, "Content-Type: application/json");
    curl_easy_setopt(curl, CURLOPT_HTTPHEADER, hs);
    // ***send data from the local stack vs CURLOPT_POSTFIELDS
    curl_easy_setopt(curl, CURLOPT_COPYPOSTFIELDS, jsonData.dump().c_str());
}

