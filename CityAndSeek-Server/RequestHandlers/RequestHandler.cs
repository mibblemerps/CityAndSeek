using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common.Packets;

namespace CityAndSeek.Server.RequestHandlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        protected CityAndSeekBehaviour CsBehaviour;

        public RequestHandler(CityAndSeekBehaviour csBehaviour)
        {
            CsBehaviour = csBehaviour;
        }

        public abstract bool OnPacket(Packet packet);
    }
}
