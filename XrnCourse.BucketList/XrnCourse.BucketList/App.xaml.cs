using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XrnCourse.BucketList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationContainer = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());

            FreshIOC.Container.Register<INavigation>(navigationContainer.Navigation);
            MainPage = navigationContainer;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
