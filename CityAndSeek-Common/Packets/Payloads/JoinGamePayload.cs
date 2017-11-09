using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common.Packets.Payloads
{
    public class JoinGamePayload : IPayload
    {
        public int GameId;
        public string GamePassword;
        public string Username;

        public JoinGamePayload(int gameId, string gamePassword, string username)
        {
            GameId = gameId;
            GamePassword = gamePassword;
            Username = username;
        }

        public JoinGamePayload()
        {
        }
    }
}
