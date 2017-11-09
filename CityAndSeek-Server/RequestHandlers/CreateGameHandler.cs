using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;

namespace CityAndSeek.Server.RequestHandlers
{
    public class CreateGameHandler : RequestHandler
    {
        public CreateGameHandler(CityAndSeekBehaviour csBehaviour) : base(csBehaviour)
        {
        }

        public override bool OnPacket(Packet packet)
        {
            if (packet.Intent != Intent.CreateGame)
                return false;

            Game newGame = new Game();
            Game receivedGame = packet.GetPayload<Game>();

            // Find un-used game ID to use
            int id = 1;
            while (CsBehaviour.CsServer.Games.ContainsKey(id))
                id++;

            // Construct new game
            newGame.Id = id;
            newGame.Name = receivedGame.Name;
            newGame.Password = receivedGame.Password;
            newGame.GameMode = receivedGame.GameMode;

            // Add to games list
            CsBehaviour.CsServer.AddGame(newGame);

            // Send game state to client
            var response = new Packet(Intent.GameCreated, newGame, packet.Id);
            CsBehaviour.SendPacket(response);

            Debug.LogInfo($"Game created: \"{newGame.Name}\" ({newGame.Id})!");

            return true;
        }
    }
}
