using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using CityAndSeek.Server.RequestHandlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CityAndSeek.Server
{
    public class CityAndSeekBehaviour : WebSocketBehavior
    {
        public CityAndSeekServer CsServer { get; private set; }

        protected List<IRequestHandler> RequestHandlers;

        /// <summary>
        /// The player this session is associated with.<br />
        /// If the session is unauthenticated this will be null.
        /// </summary>
        public Player Player
        {
            get
            {
                if (!CsServer.ClientToPlayer.ContainsKey(ID))
                    return null;

                return CsServer.ClientToPlayer[ID];
            }
            set
            {
                if (CsServer.ClientToPlayer.ContainsKey(ID))
                    CsServer.ClientToPlayer.Remove(ID);

                Debug.LogInfo($"Session \"{ID}\" now associated with player {value.Id} in game {value.Game.Id}.");

                CsServer.ClientToPlayer.Add(ID, value);
            }
        }

        public CityAndSeekBehaviour(CityAndSeekServer csServer)
        {
            CsServer = csServer;

            RequestHandlers = new List<IRequestHandler>()
            {
                new CreateGameHandler(this),
                new JoinGameHandler(this),
                new PositionUpdateHandler(this)
            };
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);

            // Dispatch to handlers
            foreach (IRequestHandler handler in RequestHandlers)
            {
                try
                {
                    handler.OnPacket(packet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Exception whilst handling packet (intent: \"{packet.Intent.ToString()}\") with handler {handler.GetType()}!");
                    
                }
            }
        }

        /// <summary>
        /// Send a packet to the client.
        /// </summary>
        /// <param name="packet">Packet</param>
        public void SendPacket(Packet packet)
        {
            SendAsync(JsonConvert.SerializeObject(packet), b => {});
        }

        /// <summary>
        /// Send a packet to the client informing them that an error occured.
        /// </summary>
        /// <param name="requestId">The packet ID the error is related to</param>
        /// <param name="message">The error message</param>
        /// todo: this should use request Ids
        public void SendError(int requestId, string message)
        {
            SendPacket(new Packet(Intent.Error, new ErrorPayload(message), requestId));
        }
    }
}
