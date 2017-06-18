using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Spendingz.Model;
using Spendingz.Model.Data;
using Spendingz.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.ViewModels
{
    public class SetupPageViewModel : ViewModelBase
    {
        private string _newCategoryTitle;
        private ObservableCollection<Category> _demoCategories;
        private ILocalStorage _localStorage;
        private IDbStorage _dbStorage;
        private INavigation _navigation;
        private RelayCommand<Category> _deleteCategory;
        private RelayCommand _createCategory;
        private RelayCommand _saveCategories;
        private RelayCommand _createLater;

        public string NewCategoryTitle {
            get
            {
                return _newCategoryTitle;
            }
            set
            {
                _newCategoryTitle = value;
                RaisePropertyChanged(nameof(NewCategoryTitle));
            }
        }



        public ObservableCollection<Category> DemoCategories {
            get { return _demoCategories; }
            set {
                _demoCategories = value;
                RaisePropertyChanged(nameof(DemoCategories));
            }
        }

        public SetupPageViewModel(ILocalStorage localStorage, IDbStorage dbStorage, INavigation nav)
        {
            _localStorage = localStorage;
            _dbStorage = dbStorage;
            _navigation = nav;
            DemoCategories = new ObservableCollection<Category> { new Category { Title = "Lebensmittel" }, new Category { Title="Kino" }, new Category { Title = "Reisen" } };

        }

        public RelayCommand<Category> DeleteCatagory
        {
            get
            {
                if (_deleteCategory == null)
                {
                    _deleteCategory = new RelayCommand<Category>( (obj) => 
                    {
                        DemoCategories.Remove(obj);
                    });
                    return _deleteCategory;
                }
                else { return _deleteCategory; }
            }
        }

        public RelayCommand CreateCategory
        {
            get
            {
                if (_createCategory == null)
                {
                    _createCategory = new RelayCommand(()=> {
                        if(!string.IsNullOrEmpty(NewCategoryTitle))
                        {
                            DemoCategories.Add(new Category { Title = NewCategoryTitle });
                            NewCategoryTitle = string.Empty;
                        }
                    });
                    return _createCategory;
                }
                else { return _createCategory; }
            }
        }

        public RelayCommand SaveCategories
        {
            get
            {
                if (_saveCategories == null)
                {
                    _saveCategories = new RelayCommand(()=> 
                    {
                        _localStorage.SaveBool(App.SETUP_FINISHED,true);
                        _dbStorage.CreateDatabase<Category>();
                        _dbStorage.CreateEntries(DemoCategories.ToList());
                        _navigation.NavigateTo(AppPages.MonthlyOverviewPage);
                        _navigation.RemoveFromHistory(AppPages.SetupPage);
                    });
                    return _saveCategories;
                }
                else
                {
                    return _saveCategories;
                }
            }
        }

        public RelayCommand CreateCategoriesLater
        {
            get
            {
                if(_createLater == null)
                {
                    _createLater = new RelayCommand(()=> 
                    {
                        _localStorage.SaveBool(App.SETUP_FINISHED, true);
                        _navigation.NavigateTo(AppPages.MonthlyOverviewPage);
                    });
                    return _createLater;
                }
                else
                {
                    return _createLater;
                }
            }
        }
        
    }
}
