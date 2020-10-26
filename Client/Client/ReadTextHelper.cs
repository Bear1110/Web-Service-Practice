using System;
using System.Linq;

namespace Client
{
    internal static class ReadTextHelper
    {
        public static string[] ReadTextToMap()
        {
            string[] map = new string[10];
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"BattleShipMap.txt");
                System.Console.WriteLine("Map: 1 Represent Ship ");
                int i = 0;
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    line.Split(' ').ToList().ForEach(e => {
                        Console.Write(e+" ");
                        map[i] += e;
                    });
                    i++;
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when read file.");
            }
            return map;
        }
    }
}
