using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common.Packets.Payloads
{
    /// <summary>
    /// Sent to the server when the client wants to create a new game.
    /// </summary>
    public class CreateGamePayload : IPayload
    {
        public string Name = "Game";
        public string Password = "";
        public GameMode GameMode = GameMode.Assassins;

        public CreateGamePayload(string name, string password, GameMode gameMode)
        {
            Name = name;
            Password = password;
            GameMode = gameMode;
        }

        public CreateGamePayload()
        {
        }
    }
}
