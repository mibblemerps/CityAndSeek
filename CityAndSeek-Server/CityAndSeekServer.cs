using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using Newtonsoft.Json;
using WebSocketSharp.Server;

namespace CityAndSeek.Server
{
    public class CityAndSeekServer
    {
        /// <summary>
        /// Current instance of the server.
        /// </summary>
        public static CityAndSeekServer Instance { get; private set; }

        public WebSocketServer Server;

        public Dictionary<int, Game> Games = new Dictionary<int, Game>();

        public CityAndSeekServer()
        {
            Instance = this;
        }

        public void Run(string url)
        {
            Server = new WebSocketServer(url);

            Debug.LogInfo("Starting server: " + url);

            // Add out of game service
            Server.AddWebSocketService("/", () => new CityAndSeekBehaviour(this));
            Server.Start();
        }

        public void AddGame(Game game)
        {
            Games.Add(game.Id, game);
        }
    }
}
