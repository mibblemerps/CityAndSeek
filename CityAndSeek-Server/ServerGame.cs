using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Server.Events;

namespace CityAndSeek.Server
{
    /// <summary>
    /// An extended version of the Game object which contains server specific metadata.
    /// </summary>
    public class ServerGame : Game
    {
        public event EventHandler<PlayerJoinGameEvent> OnPlayerJoinGame;

        public ServerGame()
        {
            OnPlayerJoinGame += OnPlayerJoinGameHandler;
        }

        public void Setup()
        {
            Debug.LogInfo($"Game created: {this}");
        }

        public void AddPlayer(ServerPlayer player)
        {
            Players.Add(player);

            OnPlayerJoinGame?.Invoke(this, new PlayerJoinGameEvent(player));
        }

        private void OnPlayerJoinGameHandler(object sender, PlayerJoinGameEvent e)
        {
            Debug.LogInfo($"Player {e.Player} has joined {this}.");
        }
    }
}
