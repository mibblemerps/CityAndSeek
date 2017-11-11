using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using CityAndSeek.Server.Helpers;

namespace CityAndSeek.Server.RequestHandlers
{
    public class JoinGameHandler : RequestHandler
    {
        public JoinGameHandler(CityAndSeekConnection connection) : base(connection)
        {
        }

        public override void OnPacket(Packet packet)
        {
            if (packet.Intent != Intent.JoinGame)
                return;

            JoinGamePayload joinGame = packet.GetPayload<JoinGamePayload>();

            // Check game exists
            if (!Connection.Server.Games.ContainsKey(joinGame.GameId))
                throw new CityAndSeekException("Game doesn't exist!");

            ServerGame game = Connection.Server.Games[joinGame.GameId];

            // Check password (not case sensitive)
            if (!joinGame.GamePassword.Equals(game.Password, StringComparison.OrdinalIgnoreCase))
                throw new CityAndSeekException("Incorrect password!");

            // Check game state
            if (game.GameState != GameState.Setup)
            {
                string message = "Game cannot accept new players right now.";
                
                switch (game.GameState)
                {
                    case GameState.Starting:
                    case GameState.Running:
                    case GameState.Paused:
                        message = "Game is already running.";
                        break;

                    case GameState.Ended:
                        message = "Game has already ended.";
                        break;
                }

                throw new CityAndSeekException(message);
            }

            int newId = game.Players.Count;

            var player = new ServerPlayer
            {
                Id = newId,
                Name = joinGame.Username,
                Token = SecureToken.Generate(),
                Game = game
            };

            // Associate player with this connection
            Connection.Player = player;

            // Add player to the game
            game.AddPlayer(player);

            // Send welcome
            var welcome = new WelcomePayload(game, player, player.Token);
            Connection.SendPacket(new Packet(Intent.Welcome, welcome, packet.Id));
        }
    }
}
