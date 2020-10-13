using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            //var list = new List<string>() { "login", "jsonString" };
            //client.LookupCommand(list);
            while (true)
            {
                client.LookupCommand(Console.ReadLine().Split(' ').ToList());
            }
        }
    }
}
