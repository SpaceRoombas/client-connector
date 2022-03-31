using System;
using System.Threading;
using ClientConnector;
using ClientConnector.messages;


namespace ClientConnectorTest
{
    class TestingRig
    {

        const string PlayerId = "ARoomba";
        static void Main(string[] args)
        {
            ServerConnection serverConnection;
            PlayerDetails details = new PlayerDetails()
            {
                PlayerName = PlayerId,
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
                    Console.WriteLine("\n\nGoing to retry connection");
                    serverConnection.Connect();
                } else
                {
                    Console.WriteLine("Ticking");

                    if(serverConnection.HasMessage)
                    {
                        ICarrierPigeon carrier = serverConnection.DequeueMessage();
                        MapSector mapSector = PayloadExtractor.GetMapSector(carrier);
                        PlayerFirmwareChange firmwareChange = new PlayerFirmwareChange()
                        {
                            Code = "while(true){move_south() move_west()}",
                            PlayerId = PlayerId,
                            RobotId = "r0"
                        };

                        // send code change
                        serverConnection.EnqueueMessage(firmwareChange);

                    }
                }


                serverConnection.Tick();
                Thread.Sleep(250);
            }

        }
    }
}
