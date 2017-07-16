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

        public override bool Equals(object obj)
        {
            return Equals(obj as Spending);
        }

        public bool Equals(Spending spending)
        {
            if(spending == null)
            {
                return false;
            }

            if (ReferenceEquals(this, spending))
            {
                return true;
            }

            return (Amount == spending.Amount && CategoryId == spending.CategoryId && Id == spending.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                hash = (hash * 7) + Amount.GetHashCode();
                hash = (hash * 13) + Id.GetHashCode();
                hash = (hash * 23) + CategoryId.GetHashCode();
                return hash;
            }
        }
    }
}
