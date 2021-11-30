using Android.Accounts;
using Android.Content;
using Android.OS;
using Android.Util;

namespace AndroidSyncApp.Droid.Sync
{
    public class SyncAdapter : AbstractThreadedSyncAdapter
    {
        private readonly ContentResolver _contentResolver;
        private readonly AccountManager _accountManager;

        public SyncAdapter(Context context, bool autoInitialize) : base(context, autoInitialize)
        {
            _contentResolver = context.ContentResolver;
            _accountManager = AccountManager.Get(context);
        }

        public override void OnPerformSync(Account account, Bundle extras, string authority, ContentProviderClient provider, SyncResult syncResult)
        {
            Log.Info("SyncAdapter", $"OnPerformSync for {account.Name}");
        }
    }
}