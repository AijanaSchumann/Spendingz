using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Spendingz.Model.Data;
using Spendingz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.ViewModels
{
    public class AddSpendingDetailPageViewModel : ViewModelBase
    {
        private string _selectedCategory;
        private List<string> _availableCategories;
        private string _amount;
        private RelayCommand _saveSpending;
        private INavigation _nav;
        private IDbStorage _storage;
        private List<Category> _categories;

        public AddSpendingDetailPageViewModel(INavigation navigationService, IDbStorage storage)
        {
            _nav = navigationService;
            _storage = storage;
            Amount = "0";
            _categories = _storage.GetAllEntries<Category>();
            AvailableCategories = new List<string>();
            foreach(var s in _categories)
            {
                AvailableCategories.Add(s.Title);
            }
           


        }

        public List<string> AvailableCategories {
            get
            {
                return _availableCategories;
            }
            set
            {
                _availableCategories = value;
                RaisePropertyChanged(nameof(AvailableCategories));
            }
        }

        public string SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        public string Amount
        {
            get { return _amount; }
            set { _amount = value; RaisePropertyChanged(nameof(Amount)); }
        }

       

        public RelayCommand SaveSpending
        {
            get
            {
               return _saveSpending ?? (_saveSpending = new RelayCommand(AddNewSpending));
            }
        }

        private void AddNewSpending()
        {
            double.TryParse(Amount, out double result);
            var spending = new Spending();
            spending.Amount = result;
            spending.CategoryId = _categories.First(s => s.Title == SelectedCategory).Id;

            _storage.CreateOrUpdateEntry(spending);
            _nav.GoBack();
        }

        private void Cancel()
        {
            _nav.GoBack();
        }

    }
}
