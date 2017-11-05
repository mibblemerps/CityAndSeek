using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace CityAndSeek.CsService
{
    [Service(Name = "net.mitchfizz05.cityandseek.CsService", Label = "City and Seek Service")]
    public class CsService : Service
    {
        private const int ServiceRunningNotificationId = 10000;
        const int ReturnToGamePendingIntentId = 0;

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //            if (CityAndSeekApp.CurrentGame == null || CityAndSeekApp.CurrentPlayer == null)
            //                throw new RuntimeException("Attempt to run City and Seek service without a current game/player!");

            // Run as a foreground service
            StartForeground(ServiceRunningNotificationId, BuildRunningNotification());

            return StartCommandResult.RedeliverIntent;
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