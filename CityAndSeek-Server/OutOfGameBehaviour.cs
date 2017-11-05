using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CityAndSeek.Server
{
    /// <summary>
    /// Handles websocket requests outside of City & Seek games. Things such as
    /// creating/joining games, spectating (maybe?) and global statistics.
    /// </summary>
    public class OutOfGameBehaviour : WebSocketBehavior
    {
        protected CityAndSeekServer CsServer;

        public OutOfGameBehaviour(CityAndSeekServer csServer)
        {
            CsServer = csServer;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);

            switch (packet.Intent)
            {
                case Intent.CreateGame:
                    OnCreateGameIntent(packet);
                    break;

                default:
                    // The intent wasn't handled, send error
                    SendError(packet.Intent, "Unexpected intent: " + packet.Intent);
                    break;
            }
        }

        /// <summary>
        /// Send a packet to the client informing them that an error occured.
        /// </summary>
        /// <param name="forIntent">The intent the error relates to</param>
        /// <param name="message">The error message</param>
        protected void SendError(Intent? forIntent, string message)
        {
            Send(JsonConvert.SerializeObject(new ErrorPayload(forIntent, message)));
        }

        protected void OnCreateGameIntent(Packet packet)
        {
            Game newGame = new Game();
            Game receivedGame = packet.GetPayload<Game>();

            // Find un-used game ID to use
            int id = 1;
            while (CsServer.Games.ContainsKey(id))
                id++;

            // Construct new game
            newGame.Id = id;
            newGame.Name = receivedGame.Name;
            newGame.Password = receivedGame.Password;
            newGame.GameMode = receivedGame.GameMode;

            // Add to games list
            CsServer.AddGame(newGame);

            // Send game state to client
            var response = new Packet(Intent.GameCreated, newGame);
            Send(JsonConvert.SerializeObject(response));
        }
    }
}
