using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using CityAndSeek.Server.RequestHandlers;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CityAndSeek.Server
{
    public class CityAndSeekConnection : WebSocketBehavior
    {
        public CityAndSeekServer Server { get; private set; }

        public ServerPlayer Player { get; set; }
        public ServerGame Game => (ServerGame) RequirePlayer().Game;

        protected List<IRequestHandler> RequestHandlers;

        public CityAndSeekConnection(CityAndSeekServer server)
        {
            Server = server;

            RequestHandlers = new List<IRequestHandler>
            {
                new CreateGameHandler(this),
                new JoinGameHandler(this)
            };
        }

        protected override void OnOpen()
        {
            Debug.LogInfo($"New connection from {Context.UserEndPoint}.");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            //
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Debug.LogWarning($"WebSocket error with {Context.UserEndPoint}: {e.Message}");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            // Deserialise incoming message to a packet
            Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);

            Debug.LogDebug("Received packet with intent: " + packet.Intent);

            // Dispatch packet to handlers
            foreach (IRequestHandler handler in RequestHandlers)
                handler.OnPacket(packet);
        }

        /// <summary>
        /// Get the current player.<br />
        /// If this connection isn't associated with a player, this will throw a <em>CityAndSeekException.UnauthenticatedException</em>.
        /// </summary>
        /// <returns>Player</returns>
        public ServerPlayer RequirePlayer()
        {
            if (Player == null)
                throw new CityAndSeekException.UnauthenticatedException();
            return Player;
        }

        /// <summary>
        /// Send a packet to the client.
        /// </summary>
        /// <param name="packet">Packet</param>
        public void SendPacket(Packet packet)
        {
            SendAsync(JsonConvert.SerializeObject(packet), success =>
            {
                if (!success)
                    Debug.LogError($"Failed to send packet (intent: {packet.Intent})!");
            });
        }

        /// <summary>
        /// Send a packet to the client informing them that an error occured.
        /// </summary>
        /// <param name="requestId">The packet ID the error is related to</param>
        /// <param name="message">The error message</param>
        public void SendError(int requestId, string message)
        {
            SendPacket(new Packet(Intent.Error, new ErrorPayload(message), requestId));
        }
    }
}
