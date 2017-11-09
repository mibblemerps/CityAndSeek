using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;

namespace CityAndSeek.Server.RequestHandlers
{
    public class JoinGameHandler : RequestHandler
    {
        public JoinGameHandler(CityAndSeekBehaviour csBehaviour) : base(csBehaviour)
        {
        }

        public override bool OnPacket(Packet packet)
        {
            JoinGamePayload payload = packet.GetPayload<JoinGamePayload>();

            // Check game exists
            if (!CsBehaviour.CsServer.Games.ContainsKey(payload.GameId))
            {
                CsBehaviour.SendError(packet.Id, "Game doesn't exist!");
                return true;
            }

            Game game = CsBehaviour.CsServer.Games[payload.GameId];

            // todo: more error messages depending on state
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

                CsBehaviour.SendError(packet.Id, message);
                return true;
            }

            // Get next available ID in the list
            int id = game.Players.Count;

            // Create player object
            var player = new Player()
            {
                Id = id,
                Name = payload.Username,
                Token = GenerateSecureToken()
            };

            // Create and send welcome payload
            var welcome = new WelcomePayload(game, player, player.Token);
            CsBehaviour.SendPacket(new Packet(Intent.Welcome, welcome));

            Debug.LogInfo($"Player \"{player.Name}\" ({player.Id}) just joined game \"{game.Name}\" ({game.Id})!");

            return true;
        }

        /// <summary>
        /// Generates a cryptographically secure token safe for authentication purposes.
        /// </summary>
        /// <param name="bytes">Number of random bytes to use</param>
        /// <returns>Cryptographically Secure Token</returns>
        public static string GenerateSecureToken(int bytes = 32)
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[bytes];
                rng.GetBytes(tokenData);

                return Convert.ToBase64String(tokenData);
            }
        }

        /// <summary>
        /// Generates a cryptographically secure token safe for authentication purposes.
        /// </summary>
        /// /// <param name="bytes">Number of random bytes to use</param>
        /// <returns>Cryptographically Secure Token</returns>
        /// todo: asynchronously generating tokens is probably overkill and possibly even harmful to performance
        public static async Task<string> GenerateSecureTokenAsync(int bytes = 32)
        {
            var task = Task.Run(() => GenerateSecureToken(bytes));
            return await task;
        }
    }
}
