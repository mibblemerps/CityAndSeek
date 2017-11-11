using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Server.Events
{
    public class PlayerJoinGameEvent : EventArgs
    {
        public ServerPlayer Player;

        public PlayerJoinGameEvent(ServerPlayer player)
        {
            Player = player;
        }
    }
}
