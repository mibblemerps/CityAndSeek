using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace CityAndSeek
{
    [Activity(Name = "net.mitchfizz05.cityandseek.MainActivity",
        Label = "City & Seek", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            
        }

        [Java.Interop.Export("OnCreateGamePress")]
        public void OnCreateGamePress(View view)
        {
            var intent = new Intent(this, typeof(CreateGameActivity));
            StartActivity(intent);
        }

        [Java.Interop.Export("OnJoinGamePress")]
        public void OnJoinGamePress(View view)
        {
            Toast.MakeText(this, "Work in progress!", ToastLength.Long).Show();

            Intent intent = new Intent(this, typeof(CsService.CsService));
            StartService(intent);
        }
    }
}
