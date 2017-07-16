using Spendingz.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Services
{
    public interface ICategory
    {
        IEnumerable<Category> GetAllCategories();

        void DeleteAllCategories(List<Category> categories);

        void SaveCategories(List<Category> categories);
    }
}
