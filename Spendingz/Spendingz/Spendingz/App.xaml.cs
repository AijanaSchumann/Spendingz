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
        public static readonly string SETUP_FINISHED = "SETUP_FINISHED";

        public App()
        {
            InitializeComponent();

            if (SimpleIoc.Default.IsRegistered<ILocalStorage>())
            {
                var localStorage = SimpleIoc.Default.GetInstance<ILocalStorage>();
                SetPage(localStorage);
            }
        }

        public static void SetPage(ILocalStorage storage)
        {
            if (!storage.GetBool(SETUP_FINISHED))
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
