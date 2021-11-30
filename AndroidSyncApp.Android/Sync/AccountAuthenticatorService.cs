using Android.App;
using Android.Content;
using Android.OS;

namespace AndroidSyncApp.Droid.Sync
{
    [Service()]
    [IntentFilter(actions: new[] { "android.accounts.AccountAuthenticator" })]
    [MetaData("android.accounts.AccountAuthenticator", Resource = "@xml/account_authenticator")]
    public class AccountAuthenticatorService : Service
    {
        private AccountAuthenticator _authenticator;

        public override void OnCreate()
        {
            base.OnCreate();
            _authenticator = new AccountAuthenticator(this);
        }

        public override IBinder OnBind(Intent intent)
        {
            return _authenticator.IBinder;
        }
    }
}