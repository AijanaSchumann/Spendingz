using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Spendingz.Model;
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

        private RelayCommand<Category> _deleteCategory;

        private RelayCommand _createCategory;

        private RelayCommand _saveCategories;

        private RelayCommand _dontSave;

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

        public SetupPageViewModel()
        {
            
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
                        //TODO: save categories to db and nav
                    });
                    return _saveCategories;
                }
                else
                {
                    return _saveCategories;
                }
            }
        }

        
    }
}
