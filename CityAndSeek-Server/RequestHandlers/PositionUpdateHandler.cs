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

            Player player = CsBehaviour.Player;
            if (player == null)
            {
                CsBehaviour.SendError(packet.Id, "Unauthenticated");
                return true;
            }

            PositionUpdatePayload positionUpdate = packet.GetPayload<PositionUpdatePayload>();

            // Set new position
            player.Position = positionUpdate.NewPosition;

            return true;
        }
    }
}
