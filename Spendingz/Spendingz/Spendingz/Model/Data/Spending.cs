using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Model.Data
{
    public class Spending : StorageObject
    {
        public double Amount { get; set; }
        public int CategoryId { get; set; }

    }
}
