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
using CityAndSeek.Common.Packets.Payloads;

namespace CityAndSeek
{
    [Activity(Label = "Join Game")]
    public class JoinGameActivity : Activity
    {
        protected CityAndSeekClient CsClient;

        protected EditText GameIdEditText;
        protected EditText GamePasswordEditText;
        protected EditText UsernameEditText;

        protected TextView GameIdLabel;
        protected TextView GamePasswordLabel;

        protected Button JoinGameButton;

        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.JoinGame);

            base.OnCreate(bundle);

            CsClient = CityAndSeekApp.CsClient;

            // Find things
            GameIdEditText = FindViewById<EditText>(Resource.Id.gameIdEditText);
            GamePasswordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            UsernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);

            GameIdLabel = FindViewById<TextView>(Resource.Id.gameIdLabel);
            GamePasswordLabel = FindViewById<TextView>(Resource.Id.gamePasswordLabel);

            JoinGameButton = FindViewById<Button>(Resource.Id.joinGameButton);

            // Check if data was supplied
            if (Intent.HasExtra("GameId"))
            {
                GameIdEditText.Text = Intent.GetIntExtra("GameId", 0).ToString();
                GameIdLabel.Visibility = ViewStates.Gone;
                GameIdEditText.Visibility = ViewStates.Gone;
            }
            if (Intent.HasExtra("GamePassword"))
            {
                GamePasswordEditText.Text = Intent.GetStringExtra("GamePassword");
                GamePasswordLabel.Visibility = ViewStates.Gone;
                GamePasswordEditText.Visibility = ViewStates.Gone;
            }
        }

        [Java.Interop.Export("OnJoinGamePress")]
        public async void OnJoinGamePress(View view)
        {
            JoinGameButton.Enabled = false;

            // Join game
            WelcomePayload welcome = await CsClient.JoinGameAsync(Int32.Parse(GameIdEditText.Text), GamePasswordEditText.Text,
                UsernameEditText.Text);

            // Record this data
            welcome.Player.Token = welcome.Token;
            CityAndSeekApp.CurrentGame = welcome.Game;
            CityAndSeekApp.CurrentPlayer = welcome.Player;

            // Launch in-game activity
            var intent = new Android.Content.Intent(this, typeof(InGameActivity));
            StartActivity(intent);
        }
    }
}