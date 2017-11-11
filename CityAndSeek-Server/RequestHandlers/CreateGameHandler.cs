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
        public CreateGameHandler(CityAndSeekConnection connection) : base(connection)
        {
        }

        public override void OnPacket(Packet packet)
        {
            if (packet.Intent != Intent.CreateGame)
                return;

            Game requestedGame = packet.GetPayload<Game>();

            var newGame = new ServerGame()
            {
                Id = Connection.Server.Games.Count,
                GameMode = requestedGame.GameMode,
                Name = requestedGame.Name,
                Password = requestedGame.Password
            };

            Connection.Server.Games.Add(newGame.Id, newGame);

            // Send created game back
            Connection.SendPacket(new Packet(Intent.GameCreated, newGame));
        }
    }
}
