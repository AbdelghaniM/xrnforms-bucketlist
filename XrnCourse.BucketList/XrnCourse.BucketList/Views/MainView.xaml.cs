using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;

namespace XrnCourse.BucketList.Views
{
    public partial class MainView : ContentPage
    {
        BucketsInMemoryService bucketListService;
        AppSettingsInMemoryService settingsService;

        public MainView()
        {
            InitializeComponent();
            settingsService = new AppSettingsInMemoryService();
            bucketListService = new BucketsInMemoryService();
        }

        protected async override void OnAppearing()
        {
            await RefreshBucketLists();
            base.OnAppearing();
        }

        private async Task RefreshBucketLists()
        {
            busyIndicator.IsVisible = true;
            //get settings, because we need current user Id
            var settings = await settingsService.GetSettings();
            //get all bucket lists for this user
            var buckets = await bucketListService.GetBucketListsForUser(settings.CurrentUserId);
            //bind IEnumerable<Bucket> to the ListView's ItemSource
            lvBucketLists.ItemsSource = null;    //Important! ensure the list is empty first to force refresh!
            lvBucketLists.ItemsSource = buckets;
            busyIndicator.IsVisible = false;
        }

        private async void btnSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }
        private async void btnAddBucketList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BucketView(null));
        }

        private async void lvBucketLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //mark item as unselected
            //lvBucketLists.SelectedItem = null;
            //get the item on which we received a tap
            var bucket = e.Item as Bucket;
            if (bucket != null)
            {
                await Navigation.PushAsync(new BucketView(bucket));
            }
        }

        private async void mnuBucketEdit_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await Navigation.PushAsync(new BucketView(selectedBucket));
        }

        private async void mnuBucketDelete_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await bucketListService.DeleteBucketList(selectedBucket.Id);
            await RefreshBucketLists(); 
        }
    }
}