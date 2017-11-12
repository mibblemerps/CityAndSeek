using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CityAndSeek.Client.Events;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using Intent = CityAndSeek.Common.Packets.Intent;

namespace CityAndSeek.Client.RequestHandlers
{
    public class GameStateUpdateHandler : RequestHandler
    {
        public event EventHandler<GameStateUpdateEvent> OnGameStateUpdate; 

        public GameStateUpdateHandler(CityAndSeekClient client) : base(client)
        {
        }

        public override void OnPacket(Packet packet)
        {
            if (packet.Intent == Intent.GameState)
            {
                OnGameStateUpdate?.Invoke(this, new GameStateUpdateEvent(packet.GetPayload<Common.Game>()));
            } else if (packet.Intent == Intent.Welcome)
            {
                OnGameStateUpdate?.Invoke(this, new GameStateUpdateEvent(packet.GetPayload<WelcomePayload>().Game));
            }
        }
    }
}