using System;
using System.Collections.Generic;
using Telepathy;
using ClientConnector.messages;
using ClientConnector.data;


namespace ClientConnector
{
    public class ServerConnection
    {
        const int MESSAGE_HANDLE_MAX = 20;
        const int RETRY_MAX = 3; // adjustable

        /**
         * Connection Modes act as a simple state machine to reflect the behavior of the class logic
         * 
         * General Flow:
         * Disconnected => Connecting => Handshaking => Connected
         */
        public enum ConnectionMode
        {
            Disconnected, // Obvious
            Connecting, // Connection being made - not actually in a state to send data
            Handshaking, // Making sure server and client are on the same page
            Connected // Ready to send/recieve messages
        }
        
        private Client client;
        private MessageSerializer serializer;

        public ConnectionMode connectionMode
        {
            get => this._mode;
        }

        // state
        private PlayerDetails playerDetails;
        private string host;
        private int port;

        // state
        private ConnectionMode _mode;
        private int retryCount = 0;
        private Queue<ArraySegment<byte>> sendQueue;
        private Queue<MappedMessage> recieveQueue;

        public ServerConnection(PlayerDetails playerDetails, string address, int port)
        {
            this.client = new Client(Util.BUFFER_SIZE_56KB)
            {
                OnData = (data) => this.HandleIncoming(data),
                OnConnected = () => this.HandleConnect()
            };

            this.serializer = new MessageSerializer();

            this.playerDetails = playerDetails;
            this.host = address;
            this.port = port;

            // state
            this._mode = ConnectionMode.Disconnected;
            this.sendQueue = new Queue<ArraySegment<byte>>();
            this.recieveQueue = new Queue<MappedMessage>();
        }

        private void HandleIncoming(ArraySegment<byte> data)
        {
            string decoded = Util.SegmentToString(data);
            CarrierPigeon<object> mappedMessage = this.serializer.DeserializeMessage(decoded);
            // map the message here
            if(this._mode == ConnectionMode.Handshaking)
            {
                Console.WriteLine("Get my handshake???");
                // This branch expects that the incoming message should be a handshake
                HandleHandshake((CarrierPigeon<Handshake>) mappedMessage);
                return;
            }
        }

        // Sends handshake to game server
        private void HandleConnect()
        {
            CarrierPigeon<PlayerDetails> handshake = new CarrierPigeon<PlayerDetails>(this.playerDetails, "player_details", "handshake");
            string serializedHandshake;
            ArraySegment<byte> handshakeBuff;
            this._mode = ConnectionMode.Handshaking;

            // send handshake
            serializedHandshake = serializer.SerializeMessage(handshake);
            handshakeBuff = Util.StringToByteSegment(serializedHandshake);
            client.Send(handshakeBuff);
        }

        // This handles the incoming handshake response from server.
        // Something to note: This will be unsecure - the correct way to handle this
        // is it use an asymmetric signature to verify returning messages are indeed from
        // the session server.
        private void HandleHandshake(CarrierPigeon<Handshake> handshake)
        {
            Handshake payload;

            if(handshake.payload == null)
            {
                // TODO throw error that we have a malformed handshake
                
            }

            payload = handshake.payload;

            if(payload.status == Handshake.STATUS_VERIFY_FAILED)
            {
                // TODO throw error that server verification failed
            }

            this._mode = ConnectionMode.Connected;
        }

        private void HandleSendQueue()
        {
            if(sendQueue.Count > 0)
            {
                client.Send(sendQueue.Dequeue());
            }

        }

        // Connection is dropped, so we reset the state of this object
        private void HandleReset()
        {
            sendQueue.Clear();
            recieveQueue.Clear();
            this._mode = ConnectionMode.Disconnected;
            this.retryCount = 0;
        }

        public void Connect()
        {
            if(this._mode == ConnectionMode.Connected)
            {
                return; 
            }

            this._mode = ConnectionMode.Connecting;
            client.Connect(this.host, this.port);
        }

        public void RetryConnection()
        {
            if(retryCount >= RETRY_MAX)
            {
                // TODO throw some sort of exception here
                this._mode = ConnectionMode.Disconnected;
            }
            this.Connect();
        }

        public void Tick()
        {
            if(this._mode != ConnectionMode.Disconnected)
            {
                if (client.Connected)
                {
                    // this needs to be a seperate case so that we ensure that
                    // handshake messages still get pumped through the underlying library
                    if(this._mode == ConnectionMode.Connected)
                    {
                        HandleSendQueue();
                    }
                    client.Tick(MESSAGE_HANDLE_MAX);
                }
                else if (client.Connecting)
                {
                    if (this._mode != ConnectionMode.Connecting)
                    {
                        this._mode = ConnectionMode.Connecting;
                    }
                }
                else
                {
                    if (this._mode == ConnectionMode.Connecting)
                    {
                        this.RetryConnection();
                    } else if(this._mode == ConnectionMode.Connected)
                    {
                        // not no more
                        HandleReset();
                    }
                }
            }
        }



    }
}
