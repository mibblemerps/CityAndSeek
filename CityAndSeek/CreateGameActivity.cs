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
    [Activity(Label = "Create Game")]
    public class CreateGameActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.CreateGame);

            base.OnCreate(savedInstanceState);
            
            //
        }

        [Java.Interop.Export("OnCreateGamePress")]
        public void OnCreateGamePress()
        {
            
        }
    }
}