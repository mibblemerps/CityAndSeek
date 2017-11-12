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
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;
using Intent = CityAndSeek.Common.Packets.Intent;

namespace CityAndSeek.Client.RequestHandlers
{
    public class ServerPositionUpdateHandler : RequestHandler
    {
        /// <summary>
        /// Called when the server has sent an update on a player's position.
        /// </summary>
        public event EventHandler<ServerPositionUpdateEvent> OnServerPositionUpdate;

        public ServerPositionUpdateHandler(CityAndSeekClient client) : base(client)
        {
        }

        public override void OnPacket(Packet packet)
        {
            if (packet.Intent != Intent.ServerPositionUpdate)
                return;

            ServerPositionUpdatePayload positionUpdate = packet.GetPayload<ServerPositionUpdatePayload>();

            foreach (KeyValuePair<int, LatLng> kv in positionUpdate.NewPositions)
                OnServerPositionUpdate?.Invoke(this, new ServerPositionUpdateEvent(kv.Key, kv.Value));
        }
    }
}