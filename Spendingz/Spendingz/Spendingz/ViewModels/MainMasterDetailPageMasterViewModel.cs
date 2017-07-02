using GalaSoft.MvvmLight;
using Spendingz.Model;
using Spendingz.Services;
using Spendingz.Views.MainMasterDetail.DetailPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.ViewModels
{
    public class MainMasterDetailPageMasterViewModel : ViewModelBase
    {
        private List<MainMasterDetailPageMenuItem> _menuItems;

        private MainMasterDetailPageMenuItem _selectedItem;

        private INavigation _nav;


        public MainMasterDetailPageMasterViewModel(INavigation navigationService)
        {
            _menuItems = new List<MainMasterDetailPageMenuItem>
            {
                new MainMasterDetailPageMenuItem{ Title=DateTime.Now.Month.ToString(), TargetType=AppPages.MonthlyOverviewDetailPage},
                new MainMasterDetailPageMenuItem{ Title="Settings", TargetType= AppPages.SettingsDetailPage }
            };

            _nav = navigationService;
        }

        public ObservableCollection<MainMasterDetailPageMenuItem> MenuItems
        {
            get
            {
                return new ObservableCollection<MainMasterDetailPageMenuItem>(_menuItems);
            }
        }

        public MainMasterDetailPageMenuItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
                if (SelectedItem != null)
                {
                    _nav.NavigateTo(SelectedItem.TargetType);
                }
                    
            }
        }
    }
}
