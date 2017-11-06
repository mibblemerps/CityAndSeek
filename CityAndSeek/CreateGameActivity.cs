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
using CityAndSeek.Common.Packets;

namespace CityAndSeek
{
    [Activity(Label = "Create Game")]
    public class CreateGameActivity : Activity
    {
        protected CityAndSeekClient CsClient;

        protected EditText GameNameEditText;
        protected EditText GamePasswordEditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.CreateGame);

            base.OnCreate(savedInstanceState);

            CsClient = CityAndSeekApp.CsClient;

            // Find things
            GameNameEditText = FindViewById<EditText>(Resource.Id.gameNameEditText);
            GamePasswordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
        }

        protected void SetFormEnabled(bool enabled)
        {
            GameNameEditText.Enabled = enabled;
            GamePasswordEditText.Enabled = enabled;
        }

        [Java.Interop.Export("OnCreateGamePress")]
        public async void OnCreateGamePress(View view)
        {
            SetFormEnabled(false);

            var newGame = new Common.Game()
            {
                Name = GameNameEditText.Text,
                Password = GamePasswordEditText.Text,
                GameMode = GameMode.Assassins
            };

            Packet result = await CsClient.CreateGameAsync(newGame);
            Common.Game game = result.GetPayload<Common.Game>();

            Toast.MakeText(this, "Game ID: " + game.Id, ToastLength.Long).Show();

//            RunOnUiThread(() =>
//            {
//                
//            });
        }
    }
}