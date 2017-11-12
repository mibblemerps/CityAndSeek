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
using CityAndSeek.Client;
using CityAndSeek.Common;

namespace CityAndSeek
{
    [Application]
    public class CityAndSeekApp : Application, Application.IActivityLifecycleCallbacks
    {
        public const string Tag = "City&Seek";

        /// <summary>
        /// Current application instance.
        /// </summary>
        public static CityAndSeekApp Instance { get; private set; }

        public static CityAndSeekClient CsClient;

        public Common.Game CurrentGame
        {
            get => CsClient.Player.Game;
            private set => CsClient.Player.Game = value;
        }
        public Player CurrentPlayer
        {
            get => CsClient.Player;
            private set => CsClient.Player = value;
        }

        public CityAndSeekApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Instance = this;
        }

        public void StartGame(Common.Game game, Player player)
        {
            CurrentPlayer = player;
            CurrentGame = game;

            // Start service
            var intent = new Intent(this, typeof(CsService.CsService));
            StartService(intent);

            // Good luck
            Toast.MakeText(this, "Good Luck", ToastLength.Long).Show();
        }

        #region Stubs

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

        #endregion
    }
}