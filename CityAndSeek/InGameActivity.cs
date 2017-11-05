using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CityAndSeek.Game;

namespace CityAndSeek
{
    [Activity(Label = "InGameActivity")]
    public class InGameActivity : FragmentActivity, IOnMapReadyCallback
    {
        protected GoogleMap Map;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.InGame);

            // Setup Google Map
            if (Map == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googleMap).GetMapAsync(this);
            }

            
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            Map = googleMap;

            Map.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(new LatLng(-34.928650, 138.599954), 15f, 0, 0)));
            
        }
    }
}