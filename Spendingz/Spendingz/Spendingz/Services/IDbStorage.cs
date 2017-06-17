using Spendingz.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Services
{
    public interface IDbStorage
    {
        bool CreateDatabase<T>() where T : StorageObject;

        int? CreateOrUpdateEntry<T>(T data) where T : StorageObject;

        List<int> CreateEntries<T>(List<T> data) where T : StorageObject;

        bool DeleteEntry<T>(T data) where T : StorageObject;

        T GetEntry<T>(int primKey) where T : StorageObject, new();

        List<T> GetAllEntries<T>() where T : StorageObject, new();
    }
}
