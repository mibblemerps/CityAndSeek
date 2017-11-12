using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using CityAndSeek.Server.Events;

namespace CityAndSeek.Server
{
    /// <summary>
    /// An extended version of the Game object which contains server specific metadata.
    /// </summary>
    public class ServerGame : Game
    {
        public event EventHandler<GameStateChangeEvent> OnGameStateChange;
        public event EventHandler<PlayerJoinGameEvent> OnPlayerJoinGame;
        public event EventHandler<MapCycleEvent> OnMapCycle;

        /// <summary>
        /// Interval that map cycles occur (in milliseconds).
        /// </summary>
        public int MapCycleInterval { get; set; } = 30000; // 30 seconds for testing // todo: change this to a more appropriate value, probably 5 minutes
        /// <summary>
        /// Timer that counts map cycles.
        /// </summary>
        protected Timer CycleTimer;
        /// <summary>
        /// Number of times a map cycle has ocurred.
        /// </summary>
        protected int CycleCount;

        private GameState _gameState = GameState.Setup;

        /// <summary>
        /// Current game state.<br />
        /// Changing this will trigger an <em>OnGameStateChange</em> event.
        /// </summary>
        public override GameState GameState
        {
            get => _gameState;
            set
            {
                var oldState = _gameState;
                _gameState = value;
                OnGameStateChange?.Invoke(this, new GameStateChangeEvent(oldState, _gameState));
            }
        }

        public ServerGame()
        {
            OnGameStateChange += OnGameStateChangeHandler;
            OnPlayerJoinGame += OnPlayerJoinGameHandler;
            OnMapCycle += OnMapCycleHandler;
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

        /// <summary>
        /// Manually trigger a map cycle.
        /// </summary>
        public void TriggerCycle(bool resetTimer = true)
        {
            // Restart timer if there is one
            if (resetTimer && CycleTimer != null)
            {
                CycleTimer.Stop();
                CycleTimer.Start();
            }

            CycleCount++;

            OnMapCycle?.Invoke(this, new MapCycleEvent());
        }

        #region EventHandlers

        private void OnGameStateChangeHandler(object stateChangeSender, GameStateChangeEvent stateChangeEvent)
        {
            Debug.LogInfo($"Game {this} state change: {stateChangeEvent.NewState}");

            // Start/stop the map cycle timer based on game state
            if (stateChangeEvent.NewState == GameState.Running)
            {
                if (CycleTimer == null)
                {
                    // Initialise a cycle timer
                    CycleTimer = new Timer(MapCycleInterval);
                    CycleTimer.Elapsed += (sender, e) => TriggerCycle(false);
                }
                CycleTimer.Start();
            }
            else
            {
                CycleTimer?.Stop();
            }
        }

        private void OnPlayerJoinGameHandler(object joinEventSender, PlayerJoinGameEvent joinEvent)
        {
            ServerPlayer player = joinEvent.Player;

            Debug.LogInfo($"Player {player} has joined {this}.");

            // todo: for testing, this should be replaced soon
            GameState = GameState.Running;
            
            player.OnPlayerPositionUpdate += (sender, e) =>
            {
                Debug.LogDebug($"Position update received from {player} in {player.Game}: {player.Position}");
            };
        }

        private void OnMapCycleHandler(object sender, MapCycleEvent e)
        {
            Debug.LogInfo($"Game {this} map cycle (cycle count: {CycleCount}).");

            var newPositions = new Dictionary<int, LatLng>();

            // Update everyone's public position
            foreach (Player player in Players)
            {
                player.PublicPosition = player.Position;
                newPositions.Add(player.Id, player.PublicPosition);
            }

            // Create a packet to broadcast to players
            var positionUpdate = new ServerPositionUpdatePayload(newPositions);
            var packet = new Packet(Intent.ServerPositionUpdate, positionUpdate);

            foreach (var player1 in Players)
            {
                var player = (ServerPlayer) player1;
                player.Connection.SendPacket(packet);
            }
        }

        #endregion
    }
}
