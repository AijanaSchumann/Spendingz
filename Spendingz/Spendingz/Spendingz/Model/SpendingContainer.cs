using Spendingz.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Model
{
   public class SpendingContainer
    {
        public List<Spending> Spendings { get; set; }
        public Category Category { get; set; }

        private double _amount;
        public double Amount {
            get
            {
                _amount= CalculateAmount();
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        private double CalculateAmount()
        {
            foreach(var spending in Spendings)
            {
                _amount += spending.Amount;
            }
            return _amount;
        }
    }
}
