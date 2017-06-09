using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Services
{
    public interface IDbStorage
    {
        bool CreateDatabase<T>(T dataType);

        bool CreateOrUpdateEntry<T>(T data);

        bool DeleteEntry<T>(T data);
    }
}
