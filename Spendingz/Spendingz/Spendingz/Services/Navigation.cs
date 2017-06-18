using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spendingz.Model;
using Xamarin.Forms;
using System.Reflection;

namespace Spendingz.Services
{
    //based on: https://mobileprogrammerblog.wordpress.com/2017/01/21/xamarin-forms-with-mvvm-light/
    public class Navigation : INavigation
    {
        // Dictionary with registered pages in the app
        private readonly Dictionary<AppPages, Type> _pagesByKey = new Dictionary<AppPages, Type>();
       
        // Navigation page where MainPage is hosted
        private NavigationPage _navigation;
        
        // Get currently displayed page
        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                                      ? _pagesByKey.First(p => p.Value == pageType).Key.ToString() : null;
                }
            }
        }
        // GoBack implementation (just pop page from the navigation stack)
        public void GoBack()
        {
            _navigation.PopAsync();
        }

        // NavigateTo method to navigate between pages without passing parameter
        public void NavigateTo(AppPages pageKey)
        {
            NavigateTo(pageKey, null);
        }
        // NavigateTo method to navigate between pages with passing parameter
        public void NavigateTo(AppPages pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                _navigation.PushAsync(CreatePage(pageKey,parameter));
            }
        }

        private Page CreatePage(AppPages pageKey, object parameter)
        {
            if (_pagesByKey.ContainsKey(pageKey))
            {
                var type = _pagesByKey[pageKey];
                ConstructorInfo constructor;
                object[] parameters;

                if (parameter == null)
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());

                    parameters = new object[]
                    {
                    };
                }
                else
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(
                            c =>
                            {
                                var p = c.GetParameters();
                                return p.Count() == 1
                                       && p[0].ParameterType == parameter.GetType();
                            });

                    parameters = new[]
                    {
                        parameter
                    };
                }

                if (constructor == null)
                {
                    throw new InvalidOperationException(
                        "No suitable constructor found for page " + pageKey);
                }
                return constructor.Invoke(parameters) as Page;
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        "No such page: {0}. Did you forget to call Navigation.Configure?",
                        pageKey), nameof(pageKey));
            }
        }
        // Register pages and add them to the dictionary
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
        // Initialize first app page
        public void Initialize(NavigationPage navigation)
        {
            _navigation = navigation;
        }

        public void RemoveFromHistory(AppPages page)
        {
            _navigation.Navigation.RemovePage(CreatePage(page,null));
        }
    }
}
