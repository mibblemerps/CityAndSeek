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
        protected CityAndSeekConnection Connection;

        public RequestHandler(CityAndSeekConnection connection)
        {
            Connection = connection;
        }

        public abstract void OnPacket(Packet packet);
    }
}
