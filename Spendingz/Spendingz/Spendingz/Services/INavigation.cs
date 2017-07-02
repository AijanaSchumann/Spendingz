using Spendingz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Spendingz.Services
{
    public interface INavigation
    {
        void NavigateTo(AppPages pageKey);
        void Configure(AppPages pageKey, Type pageType);
        void Initialize(MasterDetailPage navigation);

    }
}
