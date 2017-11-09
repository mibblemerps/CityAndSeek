using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common.Packets.Payloads
{
    /// <summary>
    /// Contains the data a client needs when joining/creating a game.
    /// </summary>
    public class WelcomePayload : IPayload
    {
        /// <summary>
        /// Game object.
        /// </summary>
        public Game Game;

        /// <summary>
        /// Your player object.
        /// </summary>
        public Player Player;

        /// <summary>
        /// Token used to commumicate with the server.
        /// </summary>
        public string Token;

        public WelcomePayload(Game game, Player player, string token)
        {
            Game = game;
            Player = player;
            Token = token;
        }

        public WelcomePayload()
        {
        }
    }
}
