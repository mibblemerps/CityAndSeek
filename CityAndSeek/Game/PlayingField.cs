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

namespace CityAndSeek.Game
{
    /// <summary>
    /// Defines a playing field (an physical area where players can play).
    /// </summary>
    public class PlayingField
    {
        /// <summary>
        /// Name of this playing field
        /// </summary>
        public string Name;

        /// <summary>
        /// Polygon defining the designated play area.
        /// </summary>
        public Polygon PlayArea;

        /// <summary>
        /// The center point of the playing area. This doesn't need to be the actual center, just a designated<br />
        /// roughly central point.
        /// </summary>
        public LatLng Center;

        /// <summary>
        /// Default camera position.
        /// </summary>
        public CameraPosition DefaultPosition;

        public PlayingField(string name, Polygon playArea, LatLng center, CameraPosition defaultPosition = null)
        {
            Name = name;
            PlayArea = playArea;
            Center = center;

            DefaultPosition = defaultPosition ?? new CameraPosition(Center, 15f, 0, 0);
        }
    }
}
