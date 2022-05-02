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
        static Robot[] robots;

        static void Setup() 
        {
            robots = new[]
            {
                new Robot(new Goalkeeper()),
                new Robot(new BallFollower()),
                new Robot(new BallFollower())
            };
        }

        static ResponseData Loop(SnapshotData snapshot) 
        {
            var robot = robots[snapshot.robot.index];
            return robot.Loop(snapshot);
        }

        static void Main(string[] args)
        {
            int port = int.Parse(args[0]);
            var server = new Server(Setup, Loop);
            server.Start(port);
        }
    }
}
