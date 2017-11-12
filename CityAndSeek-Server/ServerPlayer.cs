using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;
using CityAndSeek.Server.Events;
using Newtonsoft.Json;

namespace CityAndSeek.Server
{
    /// <summary>
    /// An extended version of the Player object which contains server specific metadata.
    /// </summary>
    public class ServerPlayer : Player
    {
        public EventHandler<PlayerPositionUpdateEvent> OnPlayerPositionUpdate;

        [JsonIgnore]
        public CityAndSeekConnection Connection { get; set; }

        public new LatLng Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                OnPlayerPositionUpdate?.Invoke(this, new PlayerPositionUpdateEvent(value));
            }
        }
    }
}
