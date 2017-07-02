using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Spendingz.Model;
using Spendingz.Services;
using Spendingz.ViewModels;
using Spendingz.Views;
using Spendingz.Views.MainMasterDetail;
using Spendingz.Views.MainMasterDetail.DetailPages;
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

        public static MasterDetailPage Master;

        public App()
        {
            InitializeComponent();

            Services.INavigation navigationService;

            //setup navigation service
            if (!SimpleIoc.Default.IsRegistered<Services.INavigation>())
            {
                navigationService = new Navigation();
                navigationService.Configure(AppPages.MonthlyOverviewDetailPage, typeof(MonthlyOverviewDetailPage));
                navigationService.Configure(AppPages.SettingsDetailPage, typeof(SettingsDetailPage));
                SimpleIoc.Default.Register(() => navigationService);
            }
            else
            {
                navigationService = SimpleIoc.Default.GetInstance<Services.INavigation>();
            }

            //setup masterDetail and nav service
            Master = new MainMasterDetailPage();
            navigationService.Initialize(Master);

            if (SimpleIoc.Default.IsRegistered<ILocalStorage>())
            {
                var localStorage = SimpleIoc.Default.GetInstance<ILocalStorage>();

                if (!localStorage.GetBool(SETUP_FINISHED)) //first startup
                {
                    MainPage = new SetupPage();
                }
                else
                {
                    MainPage = Master;
                    Master = null;
                }
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
