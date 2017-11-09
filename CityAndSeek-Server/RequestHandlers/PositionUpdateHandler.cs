using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;

namespace CityAndSeek.Server.RequestHandlers
{
    public class PositionUpdateHandler : RequestHandler
    {
        public PositionUpdateHandler(CityAndSeekBehaviour csBehaviour) : base(csBehaviour)
        {
        }

        public override bool OnPacket(Packet packet)
        {
            if (packet.Intent != Intent.PositionUpdate)
                return false;

            Player player = CsBehaviour.RequirePlayer();

            // Update position from payload
            PositionUpdatePayload positionUpdate = packet.GetPayload<PositionUpdatePayload>();
            player.Position = positionUpdate.NewPosition;

            Debug.LogDebug($"Position update from \"{player.Name}\" ({player.Id}): {player.Position}");

            return true;
        }
    }
}
