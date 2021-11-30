using Android.App;
using Android.Content;
using Android.OS;

namespace AndroidSyncApp.Droid.Sync
{
    [Service(Exported = true, Name = "com.companyname.androidsyncapp.SyncAdapterService")]
    [IntentFilter(new[] { "android.content.SyncAdapter" })]
    [MetaData("android.content.SyncAdapter", Resource = "@xml/sync_adapter")]
    public class SyncAdapterService : Service
    {
        private static SyncAdapter _syncAdapter = null;

        private static readonly object _syncAdapterLock = new object();

        public override void OnCreate()
        {
            base.OnCreate();

            lock (_syncAdapterLock)
            {
                if (_syncAdapter == null)
                    _syncAdapter = new SyncAdapter(ApplicationContext, true);
            }
        }

        public override IBinder OnBind(Intent intent)
        {
            return _syncAdapter.SyncAdapterBinder;
        }
    }
}