#pragma once
#include "APIUrl.h"
#include <signalrclient\hub_connection.h>
#include "signalrclient/hub_connection_builder.h"
class SignalRConnection
{
public:
    SignalRConnection();
private:
    APIUrl url;
    // question
    signalr::hub_connection connection = signalr::hub_connection_builder::create(url.SignalR()).build();
};
