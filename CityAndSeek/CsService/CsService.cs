using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CityAndSeek.CsService
{
    [Service(Name = "net.mitchfizz05.cityandseek.CsService", Label = "City and Seek Service")]
    public class CsService : Service
    {
        const int ReturnToGamePendingIntentId = 0;

        protected CityAndSeekApp CsApp;

        protected LocationManager LocationManager;
        protected LocationTracker LocationTracker;

        private const int ServiceRunningNotificationId = 10000;

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Info(CityAndSeekApp.Tag, "City and Seek service starting...");

            CsApp = CityAndSeekApp.Instance;

            if (CsApp.CurrentGame == null)
                throw new Exception("Attempt to start City and Seek service, but there is no active game.");

            BeginTracking();

            // Run as a foreground service
            StartForeground(ServiceRunningNotificationId, BuildRunningNotification());

            return StartCommandResult.RedeliverIntent;
        }

        private void BeginTracking()
        {
            LocationTracker = new LocationTracker(this);
            LocationTracker.Track();
        }

        private Notification BuildRunningNotification()
        {
            // Intent when the notification is pressed
            var intent = new Intent(this, typeof(InGameActivity));
            var pendingIntent = PendingIntent.GetActivity(this, ReturnToGamePendingIntentId, intent, PendingIntentFlags.UpdateCurrent);

            Bitmap largeIcon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.icon);

            // Build the notification
            var notification = new Notification.Builder(this)
                .SetContentTitle("City & Seek")
                .SetContentText("In game: <game name>")
                .SetSmallIcon(Resource.Drawable.icon_statusbar)
                .SetLargeIcon(largeIcon)
                .SetContentIntent(pendingIntent)
                .SetOngoing(true)
                .Build();

            return notification;
        }
    }
}