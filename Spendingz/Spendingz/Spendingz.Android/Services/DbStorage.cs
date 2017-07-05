using Spendingz.Model.Data;
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
        private string CreateDatabasePath<T>()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string type = typeof(T).Name;
            return System.IO.Path.Combine(folder, type+ ".db");
        }

        public bool CreateDatabase<T>() where T : StorageObject
        {
            try
            {
                using (var conn = new SQLiteConnection(CreateDatabasePath<T>()))
                {
                    conn.CreateTable<T>();
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                //TODO: add some loging
                return false;
            }
        }

        public int? CreateOrUpdateEntry<T>(T data) where T : StorageObject
        {

            try
            {
                using (var conn = new SQLiteConnection(CreateDatabasePath<T>()))
                {

                    if (data.Id != 0)
                    {
                       return conn.Update(data);
                    }
                    else
                    {
                       return conn.Insert(data);
                    }
                   
                }
            }
            catch (SQLiteException ex)
            {
                //TODO: add some loging
                return null;
            }
        }

        public bool DeleteEntry<T>(T data) where T : StorageObject
        {
            try
            {
                using (var conn = new SQLiteConnection(CreateDatabasePath<T>()))
                {
                    conn.Delete<T>(data.Id);
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                //TODO: add some loging
                return false;
            }
        }

        public T GetEntry<T>(int primKey) where T : StorageObject, new()
        {
            try
            {
                T obj;
                using (var conn = new SQLiteConnection(CreateDatabasePath<T>()))
                {
                    obj = conn.Table<T>().FirstOrDefault(data => data.Id == primKey);
                }
                return obj;
            }
            catch (SQLiteException ex)
            {
                //TODO: add some loging
                return default(T);
            }
        }

        public List<T> GetAllEntries<T>() where T : StorageObject, new()
        {
            try
            {
                List<T> allEntries;
                using (var conn = new SQLiteConnection(CreateDatabasePath<T>()))
                {
                    var name = typeof(T).Name;
                    allEntries = conn.Query<T>($"SELECT * FROM {name}");
                }
                return allEntries;
            }
            catch (SQLiteException ex)
            {
                //TODO: add some loging
                return null;
            }
        }

        public List<int> CreateEntries<T>(List<T> data) where T : StorageObject
        {
            List<int> createdEntries = new List<int>();
            try{
               
                using (var conn = new SQLiteConnection(CreateDatabasePath<T>()))
                {
                    foreach(var element in data)
                    {
                        createdEntries.Add(conn.Insert(element));
                    }
                    
                }
                return createdEntries;
            }
            catch (SQLiteException ex)
            {
                //TODO: add some loging
                return null;
            }
        }
    }
    
}
