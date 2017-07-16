using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Spendingz.Model.Data;
using Spendingz.Model.Messages;
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
        private RelayCommand _dontSave;
        private INavigation _nav;
        private ICategory _categoryService;
        private ISpendings _spendingsService;
        private List<Category> _categories;

        public AddSpendingDetailPageViewModel(INavigation navigationService, ICategory categoryService, ISpendings spendingsService)
        {
            _nav = navigationService;
            _categoryService = categoryService;
            _spendingsService = spendingsService;
            Messenger.Default.Register<NewCategoryMessage>(this, UpdateCategories);
            Amount = "0";
            _categories = new List<Category>(_categoryService.GetAllCategories());
            AvailableCategories = _categories.Select(c => c.Title).ToList();



        }

        private void UpdateCategories(NewCategoryMessage category)
        {
            if (category.Update)
            {
                _categories = new List<Category>(_categoryService.GetAllCategories());
                AvailableCategories = _categories.Select(c => c.Title).ToList();

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

        public RelayCommand DontSave
        {
            get
            {
                return _dontSave ?? (_dontSave = new RelayCommand(()=> { _nav.GoBack(); }));
            }
        }

        private void AddNewSpending()
        {
            double.TryParse(Amount, out double result);
            var spending = new Spending();
            spending.Amount = result;
            spending.CategoryId = _categories.First(s => s.Title == SelectedCategory).Id;

            _spendingsService.SaveSpending(spending);
            _nav.GoBack();
        }

        private void Cancel()
        {
            _nav.GoBack();
        }

    }
}
