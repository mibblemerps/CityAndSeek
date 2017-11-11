using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common.Packets;

namespace CityAndSeek.Server.RequestHandlers
{
    public interface IRequestHandler
    {
        void OnPacket(Packet packet);
    }
}
