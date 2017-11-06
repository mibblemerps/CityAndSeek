using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using CityAndSeek.Client;

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

            // Get location permission
            var result = ObtainPermission(Android.Manifest.Permission.AccessFineLocation);
            if (result == Permission.Denied)
                ShowMissingPermissionsAlert();

            // Connect to City and Seek server
            ConnectToServer();
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

        private async void ConnectToServer()
        {
            // Connect to City and Seek if necessary
            if (CityAndSeekApp.CsClient != null && CityAndSeekApp.CsClient.WebSocket.IsAlive)
                return;

            // Show connection dialog
            ProgressDialog connectingDialog = new ProgressDialog(this);
            connectingDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            connectingDialog.SetMessage("Connecting to server...");
            connectingDialog.SetCancelable(false);
            connectingDialog.Show();

            // Create client and connect
            string serverUrl = Resources.GetString(Resource.String.server_url);
            Log.Info(CityAndSeekApp.Tag, "Connecting to City & Seek server: " + serverUrl);
            var client = CityAndSeekApp.CsClient = new CityAndSeekClient(serverUrl);

            // Connect
            bool success = await client.Connect();

            // Hide connecting dialog
            connectingDialog.Hide();

            if (!success)
            {
                // Alert user
                AlertDialog connectErrorDialog = new AlertDialog.Builder(this)
                    .SetTitle("Connection Error")
                    .SetMessage("Couldn't connect to the City & Seek server!")
                    .SetPositiveButton("Retry", (sender, args) => ConnectToServer())
                    .SetNegativeButton("Cancel", (sender, args) => Finish())
                    .SetCancelable(false)
                    .Show();
            }
        }

        /// <summary>
        /// Obtain permission for something.
        /// </summary>
        /// <param name="permission">Permission name</param>
        /// <returns>Permission result, or null if not known yet.</returns>
        private Permission? ObtainPermission(string permission)
        {
            // Check permission currently
            Permission permissionCheck = CheckSelfPermission(permission);
            if (permissionCheck == Permission.Granted)
                return Permission.Granted; // Permission already granted

            RequestPermissions(new[] { permission }, 0);
            return null;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode != 0) return;

            bool allGood = true;
            foreach (Permission result in grantResults)
            {
                if (result == Permission.Denied)
                {
                    allGood = false;
                    break;
                }
            }

            if (!allGood)
                ShowMissingPermissionsAlert();
        }

        private void ShowMissingPermissionsAlert()
        {
            // Alert the user that City & Seek is missing permissions
            var alert = new AlertDialog.Builder(this)
                .SetTitle("Missing Permissions")
                .SetMessage("City & Seek is missing the required permissions to run.")
                .SetPositiveButton("Okay", (sender, args) =>
                {
                    Finish();
                })
                .SetCancelable(false)
                .Show();
        }
    }
}
