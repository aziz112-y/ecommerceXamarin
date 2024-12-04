using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using e_comm.ViewModel;

namespace e_comm.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartViewModel();
        }
    }
}
