using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;

namespace CityAndSeek.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"City & Seek Server (API Version: {CityAndSeekCommon.ApiLevel})");
            Console.WriteLine("Current time: " + DateTime.Now);
            Console.WriteLine();
            
            var server = new CityAndSeekServer();
            server.Run("ws://0.0.0.0:8888");

            Console.ReadLine();
        }
    }
}
