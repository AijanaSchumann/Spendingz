using GalaSoft.MvvmLight.Ioc;
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
        private static string SETUP_NEEDED = "SETUP_NEEDED";

        public App()
        {
            InitializeComponent();

            MainPage = new MonthlyOverviewPage();
        }

        public App(ILocalStorage storage)
        {
            if (!SimpleIoc.Default.IsRegistered<ILocalStorage>())
            {
                SimpleIoc.Default.Register(() => storage);
            }
          
            InitializeComponent();
            SetPage(storage);
        }

        public static void SetPage(ILocalStorage storage)
        {
            if (!storage.GetBool(SETUP_NEEDED))
            {
                Current.MainPage = new SetupPage();
            }
            else
            {
                Current.MainPage = new MonthlyOverviewPage();
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
