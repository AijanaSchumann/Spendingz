using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spendingz.Model;
using Xamarin.Forms;

namespace Spendingz.Services
{
    /// <summary>
    /// A navigation service for navigating between detail pages of a MasterDetailPage
    /// </summary>
    public class Navigation : INavigation
    {
        // Dictionary with registered pages in the app
        private readonly Dictionary<AppPages, Type> _pagesByKey = new Dictionary<AppPages, Type>();
      
        // MasterDetailPage to handle navigation between detail pages
        private MasterDetailPage _navigation;

        public void Configure(AppPages pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void Initialize(MasterDetailPage navigation)
        {
            _navigation = navigation;
        }

        public void NavigateTo(AppPages pageKey)
        {
            if (_pagesByKey.ContainsKey(pageKey))
            {
                var type = _pagesByKey[pageKey];
                var page = (Page)Activator.CreateInstance(type);
               

                _navigation.Detail = new NavigationPage(page);
                _navigation.IsPresented = false;
            }
        }
    }
}
