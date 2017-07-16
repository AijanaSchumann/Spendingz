using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Spendingz.Droid.Services;
using GalaSoft.MvvmLight.Ioc;
using Spendingz.Services;

namespace Spendingz.Droid
{
    [Activity(Label = "Spendingz", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Setup();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private void Setup()
        {
            var dbStorage = new DbStorage();
            SimpleIoc.Default.Register<IDbStorage>(()=> dbStorage);
            SimpleIoc.Default.Register<ISpendings>(() => new SpendingsService(dbStorage));
            SimpleIoc.Default.Register<ICategory>(()=> new CategoryService(dbStorage));
            SimpleIoc.Default.Register<ILocalStorage>(()=> new LocalStorage(this));
        }
    }
}

