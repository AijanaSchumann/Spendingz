using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Spendingz.ViewModels
{
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance
        {
            get { return Application.Current.Resources["Locator"] as ViewModelLocator; }
        }

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<SetupPageViewModel>();
            SimpleIoc.Default.Register<MonthlyOverviewPageViewModel>();
        }


        public SetupPageViewModel Setup
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SetupPageViewModel>();
            }
        }

        public MonthlyOverviewPageViewModel Monthly
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MonthlyOverviewPageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }

}
