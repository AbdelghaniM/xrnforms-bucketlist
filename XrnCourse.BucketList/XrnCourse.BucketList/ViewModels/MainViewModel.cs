using System;
using FreshMvvm;
using XrnCourse.BucketList.Domain.Services;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using XrnCourse.BucketList.Domain.Models;
using System.Threading.Tasks;

namespace XrnCourse.BucketList.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        BucketsInMemoryService bucketListService;
        AppSettingsInMemoryService settingsService;

        public MainViewModel()
        {
            settingsService = new AppSettingsInMemoryService();
            bucketListService = new BucketsInMemoryService();
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        private ObservableCollection<Bucket> buckets;
        public ObservableCollection<Bucket> Buckets
        {
            get { return buckets; }
            set
            {
                buckets = value;
                RaisePropertyChanged(nameof(Buckets));
            }
        }

        public ICommand OpenSettingsPageCommand => new Command(
           async () => {
               await CoreMethods.PushPageModel<SettingsViewModel>(true);
           }
        );

        public ICommand OpenBucketPageCommand => new Command<Bucket>(
            async (Bucket bucket) => {
                await CoreMethods.PushPageModel<BucketViewModel>(bucket, false, true);
            }
        );

        public ICommand DeleteBucketCommand => new Command<Bucket>(
            async (Bucket bucket) => {
                await bucketListService.DeleteBucketList(bucket.Id);
                await RefreshBucketLists();
            }
        );

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await RefreshBucketLists();
        }

        private async Task RefreshBucketLists()
        {
            IsBusy = true;
            //get settings, because we need current user Id
            var settings = await settingsService.GetSettings();
            //get all bucket lists for this user
            var buckets = await bucketListService.GetBucketListsForUser(settings.CurrentUserId);
            //bind IEnumerable<Bucket> to the ListView's ItemSource
            Buckets = null;    //Important! ensure the list is empty first to force refresh!
            Buckets = new ObservableCollection<Bucket>(buckets);
            IsBusy = false;
        }


    }
}
