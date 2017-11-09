using CityAndSeek.Common.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Server.RequestHandlers
{
    /// <summary>
    /// When requests come in, they are routed to their specific handler.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Handle an incoming packet.
        /// </summary>
        /// <param name="packet">Incoming packet</param>
        /// <returns>Return true if this packet was handled by this handler</returns>
        bool OnPacket(Packet packet);
    }
}
