using FluentValidation;
using System;
using System.Linq;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Validators;

namespace XrnCourse.BucketList.Views
{
    public partial class BucketView : ContentPage
    {
        private BucketsInMemoryService bucketService;
        private AppSettingsInMemoryService settingsService;
        private Bucket currentBucket;
        private IValidator bucketValidator;

        public BucketView(Bucket bucket)
        {
            InitializeComponent();

            settingsService = new AppSettingsInMemoryService();
            bucketService = new BucketsInMemoryService();

            bucketValidator = new BucketValidator();

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
            lblErrorDescription.IsVisible = false;
            lblErrorTitle.IsVisible = false;

            var validationResult = bucketValidator.Validate(bucket);
            //loop through error to identify properties
            foreach(var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(bucket.Title))
                {
                    lblErrorTitle.Text = error.ErrorMessage;
                    lblErrorTitle.IsVisible = true;
                }
                if (error.PropertyName == nameof(bucket.Description))
                {
                    lblErrorDescription.Text = error.ErrorMessage;
                    lblErrorDescription.IsVisible = true;
                }
            }
            return validationResult.IsValid;
        }
    }
}