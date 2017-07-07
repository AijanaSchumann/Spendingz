using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Spendingz.Model;
using Spendingz.Model.Data;
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
        private IDbStorage _storage;
        private INavigation _nav;
        private RelayCommand _addNewSpending;

        private ObservableCollection<SpendingContainer> _spendingz;
       

        public MonthlyOverviewPageViewModel(IDbStorage storage, INavigation navigationService)
        {
            _storage = storage;
            _nav = navigationService;

            Title = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            Spendings = new ObservableCollection<SpendingContainer>();
            _userCategories = new List<Category>();
            _userSpendings = new List<Spending>();
            Initialize();
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
            var categoryEntries = _storage.GetAllEntries<Category>();
            var spendingEntries = _storage.GetAllEntries<Spending>();
            if(categoryEntries!= null && spendingEntries != null)
            {
                _userCategories.AddRange(categoryEntries);
                _userSpendings.AddRange(spendingEntries);

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

    }
}
