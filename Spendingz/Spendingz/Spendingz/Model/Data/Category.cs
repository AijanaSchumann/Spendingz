using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Model.Data
{
    public class Category : StorageObject
    {
        

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Category);
        }

        public bool Equals(Category category)
        {
            if (category == null)
            {
                return false;
            }

            if (ReferenceEquals(this, category))
            {
                return true;
            }

            return (Title == category.Title && Id == category.Id);
        }

       

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                hash = (hash * 7) + (Title == null ? 0 : Title.GetHashCode());
                hash = (hash * 13) + Id.GetHashCode();
                return hash;
            }
         
        }
    }
}
