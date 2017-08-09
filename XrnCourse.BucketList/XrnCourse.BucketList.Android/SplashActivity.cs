﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using System.Threading.Tasks;

namespace XrnCourse.BucketList.Droid
{
    [Activity(Label = "Bucket List", Icon = "@drawable/icon", 
              Theme = "@style/MainTheme.Splash", MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Android.Support.V7.App.AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //simulate loading time
            //Task.Delay(8000).Wait();

            //launch the MainActivity screen when this activity ends
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}