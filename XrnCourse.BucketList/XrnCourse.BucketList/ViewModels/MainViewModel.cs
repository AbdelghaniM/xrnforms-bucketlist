using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services.Abstract;

namespace XrnCourse.BucketList.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        IAppSettingsService settingsService;
        IBucketsService bucketListService;

        public MainViewModel(IAppSettingsService settingsService, IBucketsService bucketListService)
        {
            this.settingsService = settingsService;
            this.bucketListService = bucketListService;
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
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
