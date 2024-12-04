using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_comm.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_comm.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProduitsPage : ContentPage
    {
        private ProduitViewModel _viewModel;

        public ProduitsPage()
        {
            InitializeComponent();
            BindingContext = new ProduitViewModel();
        }

        public async void OnLogoutClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }


    }
}