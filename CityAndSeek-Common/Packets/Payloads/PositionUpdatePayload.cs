using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common.Packets.Payloads
{
    /// <summary>
    /// Payload sent to the server updating their position.<br />
    /// This should be sent every few seconds.
    /// </summary>
    public class PositionUpdatePayload : IPayload
    {
        public LatLng NewPosition;
    }
}
