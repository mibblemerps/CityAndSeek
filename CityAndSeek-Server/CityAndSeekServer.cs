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

        public Dictionary<int, ServerGame> Games = new Dictionary<int, ServerGame>();

        public CityAndSeekServer()
        {
            Instance = this;
        }

        public void Run(string url)
        {
            Server = new WebSocketServer(url);

            Debug.LogInfo("Starting server: " + url);
            
            Server.AddWebSocketService("/", () => new CityAndSeekConnection(this));

            Server.Start();
        }

        public void AddGame(ServerGame game)
        {
            Games.Add(game.Id, game);
        }
    }
}
