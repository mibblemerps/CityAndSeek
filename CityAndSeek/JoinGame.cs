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
    public class JoinGame : Activity
    {
        protected EditText GameIdEditText;
        protected EditText GamePasswordEditText;
        protected EditText UsernameEditText;

        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.JoinGame);

            base.OnCreate(bundle);

            // Find things
            GameIdEditText = FindViewById<EditText>(Resource.Id.gameIdEditText);
            GamePasswordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            UsernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);

            // Check if data was supplied
            if (bundle.ContainsKey("GameId"))
            {
                GameIdEditText.Text = bundle.GetString("GameId");
                GameIdEditText.Visibility = ViewStates.Gone;
            }
            if (bundle.ContainsKey("GamePassword"))
            {
                GamePasswordEditText.Text = bundle.GetString("GamePassword");
                GamePasswordEditText.Visibility = ViewStates.Gone;
            }
        }
    }
}