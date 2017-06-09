using Spendingz.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Droid.Services
{
    public class DbStorage : IDbStorage
    {
        public bool CreateDatabase<T>(T dataType)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbName = nameof(T) + ".db";
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, dbName)))
                {
                    conn.CreateTable<T>();
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                //TODO: add some loging?
                return false;
            }
        }

        public bool CreateOrUpdateEntry<T>(T data)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntry<T>(T data)
        {
            throw new NotImplementedException();
        }
    }
}
