using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Model.Data
{
    public class StorageObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
