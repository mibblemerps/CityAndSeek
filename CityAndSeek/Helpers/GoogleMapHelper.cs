using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CityAndSeek.Helpers
{
    public static class GoogleMapHelper
    {
        /// <summary>
        /// Get a polygon that covers the entire world.<br />
        /// <br />
        /// Adapted from https://stackoverflow.com/a/15958458
        /// </summary>
        /// <returns></returns>
        public static PolygonOptions GetWorldPolygon()
        {
            float delta = 0.1f;

            List<LatLng> worldCoords = new List<LatLng>()
            {
                new LatLng(-90 + delta, -180 + delta),
                new LatLng(-90 + delta, 0),
                new LatLng(-90 + delta, 180 - delta),
                new LatLng(0, 180 - delta),
                new LatLng(90 - delta, 180 - delta),
                new LatLng(90 - delta, 0),
                new LatLng(90 - delta, -180 + delta),
                new LatLng(0,-180 + delta)
           };

            PolygonOptions options = new PolygonOptions();
            options.AddAll(new Java.Util.ArrayList(worldCoords));

            return options;
        }
    }
}