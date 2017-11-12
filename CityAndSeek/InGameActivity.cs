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
using CityAndSeek.Client.Events;
using CityAndSeek.Common;
using CityAndSeek.Game;
using LatLng = Android.Gms.Maps.Model.LatLng;

namespace CityAndSeek
{
    [Activity(Label = "City & Seek")]
    public class InGameActivity : FragmentActivity, IOnMapReadyCallback
    {
        protected GoogleMap Map;

        private List<Marker> _playerMarkers = new List<Marker>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.InGame);

            // Setup Google Map
            if (Map == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googleMap).GetMapAsync(this);
            }

            // Place initial player markers
            PlacePlayerMarkers();

            CityAndSeekApp.CsClient.OnGameStateUpdate += OnGameStateUpdate;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            Map = googleMap;

            Map.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(new LatLng(-34.928650, 138.599954), 15f, 0, 0)));
            
        }

        private void PlacePlayerMarkers()
        {
            // Remove existing markers
            foreach (Marker marker in _playerMarkers)
                marker.Remove();

            _playerMarkers.Clear();
            
            // Add new markers
            foreach (Player player in CityAndSeekApp.CsClient.Player.Game.Players)
            {
                if (player.PublicPosition == null)
                    continue; // Off-radar

                var newMarker = new MarkerOptions();
                newMarker.SetTitle(player.Name);
                newMarker.SetPosition(new LatLng(player.PublicPosition.Latitude, player.PublicPosition.Longitude));

                _playerMarkers.Add(Map.AddMarker(newMarker));
            }
        }

        private void OnGameStateUpdate(object sender, GameStateUpdateEvent e)
        {
            RunOnUiThread(PlacePlayerMarkers);
        }
    }
}