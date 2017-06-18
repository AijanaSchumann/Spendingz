using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Spendingz.Model;
using Spendingz.Services;
using Spendingz.ViewModels;
using Spendingz.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Spendingz
{
    public partial class App : Application
    {
        public static readonly string SETUP_FINISHED = "SETUP_FINISHED";

        public App()
        {
            InitializeComponent();

            Services.INavigation navigationService;

            //setup navigation service
            if (!SimpleIoc.Default.IsRegistered<Services.INavigation>())
            {
                navigationService = new Navigation();
                navigationService.Configure(AppPages.SetupPage, typeof(SetupPage));
                navigationService.Configure(AppPages.MonthlyOverviewPage, typeof(MonthlyOverviewPage));
                SimpleIoc.Default.Register(() => navigationService);
            }
            else
            {
                navigationService = SimpleIoc.Default.GetInstance<Services.INavigation>();
            }

            if (SimpleIoc.Default.IsRegistered<ILocalStorage>())
            {
                var localStorage = SimpleIoc.Default.GetInstance<ILocalStorage>();


                var firstPage = GetPage(localStorage);
                //set Navigation page as default page for nav service
                navigationService.Initialize(firstPage);
                //you have to init MainPage!
                MainPage = firstPage;
            }
        }

        private NavigationPage GetPage(ILocalStorage storage)
        {
            if (!storage.GetBool(SETUP_FINISHED))
            {
                return new NavigationPage(new SetupPage());
            }
            else
            {
                return new NavigationPage(new MonthlyOverviewPage());
            }
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
