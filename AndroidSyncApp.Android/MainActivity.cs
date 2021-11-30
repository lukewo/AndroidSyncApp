using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Linq;

namespace AndroidSyncApp.Droid
{
    [Activity(Label = "AndroidSyncApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CreateAccount();

            LoadApplication(new App());
        }

        private void CreateAccount()
        {
            var name = "test user";
            var password = "123456";

            var accountType = Resources.GetString(Resource.String.account_type);
            var manager = Android.Accounts.AccountManager.Get(Application.Context);
            var accounts = manager.GetAccountsByType(accountType);

            var account = accounts.FirstOrDefault(i => i.Name == name);

            if (account == null)
            {
                account = new Android.Accounts.Account(name, accountType);
                manager.AddAccountExplicitly(account, password, null);                
            }

            AddPeriodicSync(account);
        }

        private void AddPeriodicSync(Android.Accounts.Account account)
        {
            var authority = Resources.GetString(Resource.String.content_authority);
            var pollFrequencySeconds = 900;    // 15 minutes

            var periodicSyncs = Android.Content.ContentResolver.GetPeriodicSyncs(account, authority);
            if (periodicSyncs.Count == 0)
            {
                var bundle = new Bundle();
                bundle.PutBoolean(Android.Content.ContentResolver.SyncExtrasExpedited, false);
                bundle.PutBoolean(Android.Content.ContentResolver.SyncExtrasDoNotRetry, false);
                bundle.PutBoolean(Android.Content.ContentResolver.SyncExtrasManual, false);

                Android.Content.ContentResolver.SetIsSyncable(account, authority, 1);
                Android.Content.ContentResolver.SetSyncAutomatically(account, authority, true);
                Android.Content.ContentResolver.AddPeriodicSync(account, authority, bundle, pollFrequencySeconds);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}