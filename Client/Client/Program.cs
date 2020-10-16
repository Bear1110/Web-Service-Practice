using System;
using System.Linq;

namespace Client
{
    class Program
    {

        static void Main(string[] args)
        {
            Client client = new Client();
            //var list = new List<string>() { "login", "jsonString" };
            //client.LookupCommand(list);
            Console.WriteLine("Please input command.");
            while (true)
            {
                client.LookupCommand(Console.ReadLine().Split(' '));
            }
        }
    }
}
