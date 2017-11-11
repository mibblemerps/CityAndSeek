using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;

namespace CityAndSeek.Server.Events
{
    public class PlayerPositionUpdateEvent
    {
        public LatLng NewPosition;

        public PlayerPositionUpdateEvent(LatLng newPosition)
        {
            NewPosition = newPosition;
        }
    }
}
