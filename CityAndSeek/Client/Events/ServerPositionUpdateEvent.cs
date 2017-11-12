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
using CityAndSeek.Common;

namespace CityAndSeek.Client.Events
{
    public class ServerPositionUpdateEvent : EventArgs
    {
        public int PlayerId;
        public LatLng NewPosition;

        public ServerPositionUpdateEvent(int playerId, LatLng newPosition)
        {
            PlayerId = playerId;
            NewPosition = newPosition;
        }
    }
}