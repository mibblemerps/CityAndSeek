using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using CityAndSeek.Common;
using Java.Lang;

namespace CityAndSeek.CsService
{
    public class LocationTracker : Java.Lang.Object, GoogleApiClient.IConnectionCallbacks,
        GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener
    {
        public Location LastLocation { get; private set; }

        protected CsService CsService;

        [Obsolete]
        protected LocationManager LocationManager;

        protected GoogleApiClient ApiClient;
        protected LocationRequest LocationRequest;

        public LocationTracker(CsService service)
        {
            CsService = service;
        }

        public void Track()
        {
            Log.Info(CityAndSeekApp.Tag, "Starting up City and Seek location tracker...");

            // Setup API client
            ApiClient = new GoogleApiClient.Builder(CsService, this, this)
                .AddApi(LocationServices.API).Build();

            LocationRequest = new LocationRequest();

            // Connect API client
            ApiClient.Connect();
        }

        public async void OnConnected(Bundle connectionHint)
        {
            Log.Info(CityAndSeekApp.Tag, "Location services API client connected.");

            LocationRequest.SetPriority(100); // PRIORITY_HIGH_ACCURACY (100)
            
            LocationRequest.SetFastestInterval(2500); // Fastest interval: 2.5 seconds
            LocationRequest.SetInterval(7500); // Normal interval: 7.5 seconds

            await LocationServices.FusedLocationApi.RequestLocationUpdates(ApiClient, LocationRequest, this);
        }

        public void OnConnectionSuspended(int cause)
        {
            //
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            //
        }

        public async void OnLocationChanged(Location location)
        {
            LastLocation = location;
            Log.Verbose(CityAndSeekApp.Tag,
                string.Format("Location update: {0}, {1}", location.Latitude, location.Longitude));

            // Send location update to server
            await CityAndSeekApp.CsClient.SendPositionUpdateAsync(new LatLng(location.Latitude, location.Longitude));
        }
    }
}