using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CityAndSeek.Common.Packets
{
    public class Packet
    {
        /// <summary>
        /// Intent of this packet.
        /// </summary>
        public Intent Intent;

        /// <summary>
        /// Payload for this packet.
        /// </summary>
        public object Payload;

        public Packet(Intent intent, object payload)
        {
            Intent = intent;
            Payload = payload;
        }

        public Packet()
        {
        }

        /// <summary>
        /// Get the payload.<br />
        /// It's recommended you use this instead of directly getting from Payload.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPayload<T>()
        {
            // If payload is a generic JSON object, convert it to the requested type.
            if (Payload is JObject)
                return ((JObject) Payload).ToObject<T>();

            // Cast the payload to the requested type.
            return (T) Payload;
        }
    }
}
