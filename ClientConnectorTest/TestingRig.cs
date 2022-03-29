using System;
using System.Threading;
using ClientConnector;
using ClientConnector.data;


namespace ClientConnectorTest
{
    class TestingRig
    {
        static void Main(string[] args)
        {
            ServerConnection serverConnection;
            PlayerDetails details = new PlayerDetails()
            {
                PlayerName = "ARoomba",
                ServerAddress = "localhost",
                MatchEndTimeMillis = 334563456,
                TokenTimeMillis = 329923929,
                HMACString = "L3KM45LQK234M5LQ2K34M"

            };

            serverConnection = new ServerConnection(details, "localhost", 9001);

            serverConnection.Connect();
            for(; ; )
            {
                if(serverConnection.connectionMode == ServerConnection.ConnectionMode.Connecting)
                {
                    Console.WriteLine("Establishing Connection...");
                } else if(serverConnection.connectionMode == ServerConnection.ConnectionMode.Handshaking)
                {
                    Console.WriteLine("Handshaking..");
                } else if(serverConnection.connectionMode == ServerConnection.ConnectionMode.Disconnected)
                {
                    Console.WriteLine("Disconnected..");
                } else
                {
                    Console.WriteLine("Ticking");
                }


                serverConnection.Tick();
                Thread.Sleep(250);
            }

        }
    }
}
