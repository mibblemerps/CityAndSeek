using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CityAndSeek.Common.Packets;

namespace CityAndSeek.Client.RequestHandlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        protected CityAndSeekClient Client;

        public RequestHandler(CityAndSeekClient client)
        {
            Client = client;
        }

        public abstract void OnPacket(Packet packet);
    }
}