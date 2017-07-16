using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Spendingz.Model;
using Spendingz.Model.Data;
using Spendingz.Model.Messages;
using Spendingz.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.ViewModels
{
    public class MonthlyOverviewPageViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private List<Category> _userCategories;
        private List<Spending> _userSpendings;
        private ICategory _categoryService;
        private INavigation _nav;
        private ISpendings _spendingsService;
        private RelayCommand _addNewSpending;

        private ObservableCollection<SpendingContainer> _spendingz;
       

        public MonthlyOverviewPageViewModel(ISpendings spendingsService, ICategory categoryService, INavigation navigationService)
        {
            _spendingsService = spendingsService;
            _categoryService = categoryService;
            _nav = navigationService;

            Title = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            Spendings = new ObservableCollection<SpendingContainer>();
            _userCategories = new List<Category>();
            _userSpendings = new List<Spending>();
            Initialize();
            Messenger.Default.Register<NewSpendingMessage>(this, UpdateSpendings);
            Messenger.Default.Register<NewCategoryMessage>(this, UpdateCategories);
        }

        public ObservableCollection<SpendingContainer> Spendings
        {
            get
            {
                return _spendingz;
            }
            set
            {
                _spendingz = value;
                RaisePropertyChanged(nameof(Spendings));
            }
        }

        public RelayCommand AddNewSpending
        {
            get
            {
                return _addNewSpending ?? (_addNewSpending = new RelayCommand(()=> {
                    _nav.NavigateTo(AppPages.AddSpendingDetailPage);
                }));
            }
        }

        private void Initialize()
        {
            var categoryEntries = _categoryService.GetAllCategories();
            var spendingEntries = _spendingsService.GetAllSpendings();
            Spendings = new ObservableCollection<SpendingContainer>();

            if (categoryEntries!= null && spendingEntries != null)
            {
                _userCategories = new List<Category>(categoryEntries);
                _userSpendings = new List<Spending>(spendingEntries);

                    foreach (var category in _userCategories)
                    {
                        SpendingContainer container = new SpendingContainer();
                        container.Currency = "€";

                        container.Category = category;
                        container.Amount = 0;
                        container.Spendings = new List<Spending>();

                        foreach (var spending in _userSpendings)
                        {
                            if (category.Id == spending.CategoryId)
                            {
                                container.Spendings.Add(spending);
                            }
                        }
                        if(container.Spendings.Count>0)
                            Spendings.Add(container);
                    }
            }
           
          
        }

        public void UpdateSpendings(NewSpendingMessage message)
        {
            if (message.Update)
            {
                Initialize();
            }
        }

        public void UpdateCategories(NewCategoryMessage message)
        {
            if (message.Update)
            {
                Initialize();
            }
        }

    }
}
