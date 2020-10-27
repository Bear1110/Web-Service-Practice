#include "SignalRConnection.h"
#include <iostream>

#include <future>



void test(const signalr::value& m)
{
    std::cout << std::endl << m.as_array()[0].as_string() << std::endl;
}


void ReceiveSomeoneOnline(const signalr::value& m)
{
    std::cout << std::endl << m.as_array()[0].as_string() << std::endl << "??";
}

SignalRConnection::SignalRConnection()
{

    //connection.on("ReceiveSomeoneOnline", ReceiveSomeoneOnline);
    connection.on("TestForCpp", test);

    connection.start([](std::exception_ptr exception)
        {
            if (exception)
            {
                try
                {
                    std::rethrow_exception(exception);
                }
                catch (const std::exception& ex)
                {
                    std::cout << "exception when starting connection: " << ex.what() << std::endl;
                }
                return;
            }
        });
}
