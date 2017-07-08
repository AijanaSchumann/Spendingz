using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Spendingz.Model.Data;
using Spendingz.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.ViewModels
{
    public class CategoryDetailPageViewModel : ViewModelBase
    {
        private string _categoryTitle;
        private ObservableCollection<Category> _userCategories;
        private List<Category> _newCategories;
        private List<Category> _deleteCategories;
        private RelayCommand<Category> _deleteCategory;
        private RelayCommand _createCategory;
        private RelayCommand _save;
        private RelayCommand _dontSave;
        private INavigation _navigationService;
        private IDbStorage _storageService;
        

        public CategoryDetailPageViewModel(INavigation navigationService, IDbStorage storageService)
        {
            _navigationService = navigationService;
            _storageService = storageService;
            var categories = _storageService.GetAllEntries<Category>();
            UserCategories = new ObservableCollection<Category>(categories);
            _newCategories = new List<Category>();
            _deleteCategories = new List<Category>();
        }

        public string CategoryTitle
        {
            get
            {
                return _categoryTitle;
            }
            set
            {
                _categoryTitle = value;
                RaisePropertyChanged(nameof(CategoryTitle));
            }
        }


        public ObservableCollection<Category> UserCategories
        {
            get
            {
                return _userCategories;
            }
            set
            {
                _userCategories = value;
                RaisePropertyChanged(nameof(UserCategories));
            }

        }

        public RelayCommand CreateCategory
        {
            get
            {
                return _createCategory ?? (_createCategory = new RelayCommand(TemporaryCreateCategory));
            }
        }

        public RelayCommand<Category> DeleteCategory
        {
            get
            {
                return _deleteCategory ?? (_deleteCategory = new RelayCommand<Category>(category => 
                TemporaryDeleteCategory(category)));
            }
        }

        public RelayCommand Save
        {
            get
            {
                return _save ?? (_save = new RelayCommand(SaveChanges));
            }
        }

        public RelayCommand DontSave
        {
            get
            {
                return _dontSave ?? (_dontSave = new RelayCommand(()=> { _navigationService.GoBack(); }));
            }
        }

        private void SaveChanges()
        {
            DeleteCategories();
            SaveCategories();
            _navigationService.GoBack();
        }

        private void SaveCategories()
        {
            if (_newCategories.Any())
            {
                _storageService.CreateAllEntries(_newCategories);
            }
        }

        private void DeleteCategories()
        {
            var deleteAll = new List<Category>();

            if (_deleteCategories.Any())
            {
                foreach (var category in _deleteCategories)
                {
                    if (category.Id == 0)
                    {
                        _newCategories.Remove(category);
                    }
                    else
                    {
                        deleteAll.Add(category);
                    }
                }

                _storageService.DeleteAllEntries(deleteAll);
            }
        }

        private void TemporaryDeleteCategory(Category category)
        {
            _deleteCategories.Add(category);
            UserCategories.Remove(category);
        }

        private void TemporaryCreateCategory()
        {
           
            if(!string.IsNullOrEmpty(CategoryTitle))
            {
                var exists = UserCategories.FirstOrDefault(c => c.Title.Equals(CategoryTitle));
                if (exists == null)
                {
                    var category = new Category();
                    category.Title = CategoryTitle;
                    _newCategories.Add(category);
                    UserCategories.Add(category);
                }
               
            }
           
        }

    }
}
