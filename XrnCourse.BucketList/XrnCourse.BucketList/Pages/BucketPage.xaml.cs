using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.ViewModels;

namespace XrnCourse.BucketList.Pages
{
    public partial class BucketPage : ContentPage
    {
        
        public BucketPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe(this, Constants.MessageNames.BucketSaved,
                async (BucketViewModel sender, Bucket savedBucket) => {
                    await DisplayAlert("Saved", $"Your bucket list {savedBucket.Title} has been saved", "Ok");
                });

        }
    }
}