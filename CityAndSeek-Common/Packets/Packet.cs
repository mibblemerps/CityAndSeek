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
        /// A simple identifier that the client or server may send with their packet.<br />
        /// This identifies this particular request. When an Id is received in a packet,
        /// the same Id <b>must</b> be sent in any packet in response.<br />
        /// This Id can be left as 0 if it's not needed.
        /// </summary>
        public int Id = -1;

        /// <summary>
        /// Payload for this packet.
        /// </summary>
        public object Payload;

        public Packet(Intent intent, object payload)
        {
            Intent = intent;
            Payload = payload;
        }

        public Packet(Intent intent, object payload, int id)
        {
            Intent = intent;
            Payload = payload;
            Id = id;
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
