using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common.Packets;
using CityAndSeek.Common.Packets.Payloads;

namespace CityAndSeek.Server.RequestHandlers
{
    public class PositionUpdateHandler : RequestHandler
    {
        public PositionUpdateHandler(CityAndSeekConnection connection) : base(connection)
        {
        }

        public override void OnPacket(Packet packet)
        {
            if (packet.Intent != Intent.PositionUpdate)
                return;

            PositionUpdatePayload positionUpdate = packet.GetPayload<PositionUpdatePayload>();

            ServerPlayer player = Connection.RequirePlayer();

            // Update position
            player.Position = positionUpdate.NewPosition;

            
        }
    }
}
