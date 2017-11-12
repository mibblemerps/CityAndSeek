using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common.Packets.Payloads
{
    public class ServerPositionUpdatePayload : IPayload
    {
        /// <summary>
        /// Dictonary mapping player Ids to their new public positions.
        /// </summary>
        public IDictionary<int, LatLng> NewPositions;

        public ServerPositionUpdatePayload(IDictionary<int, LatLng> newPositions)
        {
            NewPositions = newPositions;
        }

        public ServerPositionUpdatePayload(int playerId, LatLng newPosition)
        {
            NewPositions = new Dictionary<int, LatLng>
            {
                {playerId, newPosition}
            };
        }

        public ServerPositionUpdatePayload()
        {
        }
    }
}
