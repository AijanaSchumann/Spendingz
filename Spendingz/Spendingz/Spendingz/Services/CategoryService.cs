using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spendingz.Model.Data;
using GalaSoft.MvvmLight.Messaging;
using Spendingz.Model.Messages;

namespace Spendingz.Services
{
    public class CategoryService : ICategory
    {
        private IDbStorage _dbStorage;
        private List<Category> _categories;
        public CategoryService(IDbStorage storage)
        {
            _dbStorage = storage;
            _categories = _dbStorage.GetAllEntries<Category>();
        }
        public void DeleteAllCategories(List<Category> categories)
        {
            foreach(var category in categories)
            {
                _categories.Remove(category);
            }

            Task.Run(()=> { _dbStorage.DeleteAllEntries(categories); });
            Messenger.Default.Send(new NewCategoryMessage { Update = true });
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return new List<Category>(_categories);
        }

        public void SaveCategories(List<Category> categories)
        {
            _dbStorage.CreateAllEntries(categories);
            _categories.AddRange(categories);
            Messenger.Default.Send(new NewCategoryMessage { Update = true });
          
        }
    }
}
