using Spendingz.ViewModels;
using Spendingz.Views.MainMasterDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spendingz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupPage : ContentPage
    {
        SetupPageViewModel _viewmodel;
        public SetupPage()
        {
            InitializeComponent();
            _viewmodel = BindingContext as SetupPageViewModel;
        }

        private void DontSave_Clicked(object sender, EventArgs e)
        {
            _viewmodel.DontSaveCategories();
            Application.Current.MainPage = new MainMasterDetailPage();
        }

        private void SaveCategories_Clicked(object sender, EventArgs e)
        {
            _viewmodel.SaveCategories();
            Application.Current.MainPage = App.Master;
            App.Master = null;
        }
    }
}