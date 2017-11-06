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

namespace CityAndSeek
{
    [Activity(Label = "Join Game")]
    public class JoinGameActivity : Activity
    {
        protected EditText GameIdEditText;
        protected EditText GamePasswordEditText;
        protected EditText UsernameEditText;

        protected TextView GameIdLabel;
        protected TextView GamePasswordLabel;

        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.JoinGame);

            base.OnCreate(bundle);

            // Find things
            GameIdEditText = FindViewById<EditText>(Resource.Id.gameIdEditText);
            GamePasswordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            UsernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);

            GameIdLabel = FindViewById<TextView>(Resource.Id.gameIdLabel);
            GamePasswordLabel = FindViewById<TextView>(Resource.Id.gamePasswordLabel);

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
    }
}