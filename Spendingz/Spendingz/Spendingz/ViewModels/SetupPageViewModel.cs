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
        private RelayCommand<Category> _deleteCategory;
        private RelayCommand _createCategory;

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

        public SetupPageViewModel(ILocalStorage localStorage, IDbStorage dbStorage)
        {
            _localStorage = localStorage;
            _dbStorage = dbStorage;
            DemoCategories = new ObservableCollection<Category> { new Category { Title = "Lebensmittel" }, new Category { Title="Kino" }, new Category { Title = "Reisen" } };

        }

        public void SaveCategories()
        {
            _localStorage.SaveBool(App.SETUP_FINISHED, true);
            _dbStorage.CreateDatabase<Category>();
            _dbStorage.CreateEntries(DemoCategories.ToList());
        }

        public void DontSaveCategories()
        {
            _localStorage.SaveBool(App.SETUP_FINISHED, true);
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
        
    }
}
