using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spendingz.Services
{
    public interface ILocalStorage
    {
        void SaveBool(string key, bool value);

        bool GetBool(string key);
    }
}
