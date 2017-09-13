using FluentValidation;
using FreshMvvm;
using System.Windows.Input;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Validators;

namespace XrnCourse.BucketList.ViewModels
{
    public class BucketViewModel : FreshBasePageModel
    {
        private BucketsInMemoryService bucketService;
        private AppSettingsInMemoryService settingsService;
        private Bucket currentBucket;
        private IValidator bucketValidator;

        public BucketViewModel()
        {
            settingsService = new AppSettingsInMemoryService();
            bucketService = new BucketsInMemoryService();

            bucketValidator = new BucketValidator();

        }

        #region Properties


        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                RaisePropertyChanged(nameof(pageTitle));
            }
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

        private string bucketTitle;
        public string BucketTitle
        {
            get { return bucketTitle; }
            set
            {
                bucketTitle = value;
                RaisePropertyChanged(nameof(BucketTitle));
            }
        }

        private string bucketTitleError;
        public string BucketTitleError
        {
            get { return bucketTitleError; }
            set
            {
                bucketTitleError = value;
                RaisePropertyChanged(nameof(BucketTitleError));
                RaisePropertyChanged(nameof(BucketTitleErrorVisible));
            }
        }

        public bool BucketTitleErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(BucketDescriptionError); }
        }

        private string bucketDescription;
        public string BucketDescription
        {
            get { return bucketDescription; }
            set
            {
                bucketDescription = value;
                RaisePropertyChanged(nameof(BucketDescription));
            }
        }

        private string bucketDescriptionError;
        public string BucketDescriptionError
        {
            get { return bucketDescriptionError; }
            set
            {
                bucketDescriptionError = value;
                RaisePropertyChanged(nameof(BucketDescriptionError));
                RaisePropertyChanged(nameof(BucketDescriptionErrorVisible));
            }
        }

        public bool BucketDescriptionErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(BucketDescriptionError); }
        }

        private bool bucketIsFavorite;
        public bool BucketIsFavorite
        {
            get { return bucketIsFavorite; }
            set
            {
                bucketIsFavorite = value;
                RaisePropertyChanged(nameof(BucketIsFavorite));
            }
        }

        private string bucketPercentComplete;
        public string BucketPercentComplete
        {
            get { return bucketPercentComplete; }
            set
            {
                bucketPercentComplete = value;
                RaisePropertyChanged(nameof(BucketPercentComplete));
            }
        }
        #endregion

        /// <summary>
        /// Callled whenever the page is navigated to.
        /// </summary>
        /// <param name="initData"></param>
        public override void Init(object initData)
        {
            Bucket bucket = initData as Bucket;
            if (bucket == null)
            {
                currentBucket = new Bucket();
                PageTitle = "New Bucket List";
            }
            else
            {
                currentBucket = bucket;
                PageTitle = currentBucket.Title;
            }

            LoadBucketState();
            base.Init(initData);
        }


        private void LoadBucketState()
        {
            BucketTitle = currentBucket.Title;
            BucketDescription = currentBucket.Description;
            BucketIsFavorite = currentBucket.IsFavorite;
            BucketPercentComplete = currentBucket.PercentCompleted.ToString("P0");
        }

        private void SaveBucketState()
        {
            currentBucket.Title = BucketTitle;
            currentBucket.Description = BucketDescription;
            currentBucket.IsFavorite = BucketIsFavorite;
        }

        private bool Validate(Bucket bucket)
        {
            //BucketDescriptionError.IsVisible = false;
            //BucketTitleError.IsVisible = false;

            var validationResult = bucketValidator.Validate(bucket);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(bucket.Title))
                {
                    BucketTitleError = error.ErrorMessage;
                    //lblErrorTitle.IsVisible = true;
                }
                if (error.PropertyName == nameof(bucket.Description))
                {
                    BucketDescriptionError = error.ErrorMessage;
                    //lblErrorDescription.IsVisible = true;
                }
            }
            return validationResult.IsValid;
        }

        public ICommand SaveBucketCommand => new Command(
            async () => {
                SaveBucketState();
                if (Validate(currentBucket))
                {
                    IsBusy = true;
                    await bucketService.SaveBucketList(currentBucket);
                    IsBusy = false;

                    MessagingCenter.Send(this,
                        Constants.MessageNames.BucketSaved, currentBucket);
                }
            }
        );
    }
}
