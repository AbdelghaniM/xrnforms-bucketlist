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
    public partial class BucketView : ContentPage
    {
        private BucketsInMemoryService bucketService;
        private AppSettingsInMemoryService settingsService;
        private Bucket currentBucket;

        public BucketView(Bucket bucket)
        {
            InitializeComponent();

            settingsService = new AppSettingsInMemoryService();
            bucketService = new BucketsInMemoryService();

            if (bucket == null)
            {
                currentBucket = new Bucket();
                Title = "New Bucket List";
            }
            else
            {
                currentBucket = bucket;
                Title = currentBucket.Title;
            }
        }

        protected override void OnAppearing()
        {
            LoadBucketState();
            base.OnAppearing();
        }

        private void LoadBucketState()
        {
            txtTitle.Text = currentBucket.Title;
            txtDescription.Text = currentBucket.Description;
            swIsFavorite.IsToggled = currentBucket.IsFavorite;
            lblPercentComplete.Text = currentBucket.PercentCompleted.ToString("P0");
        }

        private void SaveBucketState()
        {
            currentBucket.Title = txtTitle.Text;
            currentBucket.Description = txtDescription.Text;
            currentBucket.IsFavorite = swIsFavorite.IsToggled;
        }
        
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            SaveBucketState();
            if (Validate(currentBucket))
            {
                busyIndicator.IsVisible = true;
                await bucketService.SaveBucketList(currentBucket);
                busyIndicator.IsVisible = false;
                await DisplayAlert("Saved", $"Your bucket list {currentBucket.Title} has been saved", "Ok");
            }
        }

        private bool Validate(Bucket bucket)
        {
            bool error = false;
            if (string.IsNullOrWhiteSpace(bucket.Title))
            {
                error = true;
                lblErrorTitle.Text = "Title cannot be empty";
                lblErrorTitle.IsVisible = true;
            }
            if (string.IsNullOrWhiteSpace(bucket.Description))
            {
                error = true;
                lblErrorDescription.Text = "Description cannot be empty";
                lblErrorDescription.IsVisible = true;
            }

            if (!error)
            {
                lblErrorTitle.Text = "";
                lblErrorTitle.IsVisible = false;
                lblErrorDescription.Text = "";
                lblErrorDescription.IsVisible = false;
            }
            return !error;
        }
    }
}