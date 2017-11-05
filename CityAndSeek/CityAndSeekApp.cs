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

namespace CityAndSeek
{
    public class CityAndSeekApp : Application, Application.IActivityLifecycleCallbacks
    {
        public static Common.Game CurrentGame;
        public static Player CurrentPlayer;

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            //
        }

        public void OnActivityDestroyed(Activity activity)
        {
            //
        }

        public void OnActivityPaused(Activity activity)
        {
            //
        }

        public void OnActivityResumed(Activity activity)
        {
            //
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            //
        }

        public void OnActivityStarted(Activity activity)
        {
            //
        }

        public void OnActivityStopped(Activity activity)
        {
            //
        }
    }
}