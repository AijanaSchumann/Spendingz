using Spendingz.Model;
using Spendingz.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Services
{
    public interface ISpendings
    {
        IEnumerable<Spending> GetAllSpendings();

        void SaveSpending(Spending spending);

        
    }
}
