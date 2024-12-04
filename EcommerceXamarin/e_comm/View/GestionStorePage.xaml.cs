using System;
using System.Collections.ObjectModel;
using e_comm;
using e_comm.Services;
using Xamarin.Forms;

namespace e_comm.View
{
    public partial class GestionStorePage : ContentPage
    {
        private readonly ProductService _productService;

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public Command DeleteProductCommand { get; set; }

        public GestionStorePage()
        {
            InitializeComponent();

            _productService = new ProductService();
            DeleteProductCommand = new Command<Product>(OnDeleteProduct);

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var products = await _productService.GetProduitsAsync();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductFormPage());
        }
        public async void OnLogoutClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }

        private async void OnProductTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var product = e.Item as Product;
                await Navigation.PushAsync(new ProductDetailsPage { ProductId = product.Id.ToString() });
            }
        }

        private async void OnDeleteProduct(Product product)
        {
            bool confirmDelete = await DisplayAlert("Confirmation", "Are you sure that you want to delete this product ?", "Yes", "No");

            if (confirmDelete)
            {
                bool isDeleted = await _productService.DeleteProductAsync(product.Id);

                if (isDeleted)
                {
                    Products.Remove(product);
                    await DisplayAlert("Success", "Product Deleted Successfully!", "OK");
                }
                else
                {
                    await DisplayAlert("Error", ".", "OK");
                }
            }
        }
    }
}
