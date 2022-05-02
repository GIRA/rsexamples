using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using RSProxy;

namespace RSExample
{
    class Program
    {
        static void Setup() 
        {
            Console.WriteLine("SETUP!");
        }

        static ResponseData Loop(SnapshotData snapshot) 
        {
            if (snapshot.team != null) 
            {
                Console.WriteLine("====");
                foreach (var msg in snapshot.team)
                {
                    Console.WriteLine(msg.ToString());
                }
            }

            return new ResponseData
            {
                team = new [] 
                { 
                    "Hola, soy " + snapshot.robot.name
                },
                L = 10,
                R = -10
            };
        }

        static void Main(string[] args)
        {
            int port = int.Parse(args[0]);
            var server = new Server(Setup, Loop);
            server.Start(port);
        }
    }
}
