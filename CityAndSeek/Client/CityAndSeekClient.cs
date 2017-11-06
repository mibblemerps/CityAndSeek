using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CityAndSeek.Common.Packets;
using CityAndSeek.Helpers;
using Newtonsoft.Json;
using WebSocketSharp;
using Intent = CityAndSeek.Common.Packets.Intent;

namespace CityAndSeek.Client
{
    public class CityAndSeekClient
    {
        /// <summary>
        /// Links packet Ids to Actions which are supposed to handle them.
        /// </summary>
        private IDictionary<int, Action<Packet>> _pendingActions = new Dictionary<int, Action<Packet>>();

        private int _requestId = 0;

        /// <summary>
        /// Websocket URL to the server
        /// </summary>
        public string Url { get; private set; }

        public WebSocket WebSocket { get; protected set; }

        /// <summary>
        /// Get a unique request ID
        /// </summary>
        public int RequestId => _requestId++;

        public CityAndSeekClient(string url)
        {
            Url = url;
        }

        public void Connect(Action<bool> result = null)
        {
            WebSocket = new WebSocket(Url);
            WebSocket.ConnectAsync();

            if (result == null)
                return;

            EventHandler success = null;
            EventHandler<ErrorEventArgs> error = null;
            DelayedAction Timeout = null;

            Action unregisterActions = () =>
            {
                WebSocket.OnOpen -= success;
                WebSocket.OnError -= error;
                Timeout.Abort();
            };

            WebSocket.OnOpen += success = (sender, args) =>
            {
                unregisterActions();

                result.Invoke(true);
            };
            WebSocket.OnError += error = (sender, args) =>
            {
                unregisterActions();

                result.Invoke(false);
            };
            Timeout = new DelayedAction(() =>
            {
                unregisterActions();

                result.Invoke(false);
            });
            Timeout.Run(3000); // 3 second timeout

            WebSocket.OnMessage += OnWebSocketMessage;
        }

        protected void OnWebSocketMessage(object sender, MessageEventArgs e)
        {
            Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);

            if (_pendingActions.ContainsKey(packet.Id))
            {
                // We have been awaiting this packet
                _pendingActions[packet.Id](packet);
                _pendingActions[packet.Id] = null; // Remove pending action so it cannot be called again
            }
        }

        /// <summary>
        /// Create a new City and Seek game.
        /// </summary>
        /// <param name="game">Game object with requested settings set</param>
        /// <param name="complete">Action to invoke when game is created</param>
        public void CreateGame(Common.Game game, Action<Common.Game> complete)
        {
            int requestId = RequestId;
            WebSocket.SendAsync(JsonConvert.SerializeObject(new Packet(Intent.CreateGame, game, requestId)), b => {}); // todo: handle error here

            _pendingActions.Add(requestId, (packet) =>
            {
                complete.Invoke(packet.GetPayload<Common.Game>());
            });
        }
    }
}