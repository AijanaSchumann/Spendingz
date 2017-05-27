using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Spendingz.Services;
using Android.Preferences;

namespace Spendingz.Droid.Services
{

    public class LocalStorage : ILocalStorage
    {
        ISharedPreferences sharedpreferences;
        ISharedPreferencesEditor editor;

        public LocalStorage(Context context)
        {
            sharedpreferences = PreferenceManager.GetDefaultSharedPreferences(context);
            editor = sharedpreferences.Edit();
        }

        public bool GetBool(string key)
        {
            var value = sharedpreferences.GetString(key, null);
            var parseResult = bool.TryParse(value, out bool result);
            return parseResult;
        }

        public void SaveValue(string key, string value)
        {
            editor.PutString(key,value);
            editor.Commit();
        }
    }
}