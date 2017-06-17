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
            return sharedpreferences.GetBoolean(key, false);
        }

        public void SaveBool(string key, bool value)
        {
            editor.PutBoolean(key,value);
            editor.Commit();
        }
    }
}