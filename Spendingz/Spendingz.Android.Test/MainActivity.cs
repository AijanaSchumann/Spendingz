using System.Reflection;
using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using Android.Content;

namespace Spendingz.Android.Test
{
    [Activity(Label = "Spendingz.Android.Test", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestSuiteActivity
    {
        public static Context Context { get; private set; }
        protected override void OnCreate(Bundle bundle)
        {
            Context = this;

            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
           
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}

