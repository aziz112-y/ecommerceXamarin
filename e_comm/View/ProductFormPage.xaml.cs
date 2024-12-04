using System;
using Xamarin.Forms;
using e_comm.Services;
using e_comm.Model;
using e_comm.ViewModel;
using Xamarin.Forms.Xaml;
using System.Linq;
namespace e_comm.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ProductId), "productId")]
    public partial class ProductFormPage : ContentPage
    {
        public string ProductId { get; set; }
        private ProductService ProductService = new ProductService();
        private CategorieService CategorieService = new CategorieService();
        private Product CurrentProduct;

        public ProductFormPage()
        {
            InitializeComponent();
            BindingContext = new ProduitViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(ProductId))
            {
                var product = await ProductService.GetProductByIdAsync(ProductId);

                if (product != null)
                {
                    CurrentProduct = product;
                    NameEntry.Text = product.Name;
                    PriceEntry.Text = product.Price.ToString("C");
                    DescriptionEntry.Text = product.Description;

                    var selectedCategory = (BindingContext as ProduitViewModel)?.Categories
                        .FirstOrDefault(c => c.Id == product.CategoryId);
                    CategoryPicker.SelectedItem = selectedCategory;
                }
                else
                {
                    await DisplayAlert("Error", "Product not found.", "OK");
                }
            }
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(DescriptionEntry.Text) ||
                string.IsNullOrWhiteSpace(PriceEntry.Text) ||
                CategoryPicker.SelectedItem == null)
            {
                await DisplayAlert("Validation Error", "All fields must be filled in.", "OK");
                return;
            }

            var selectedCategory = (Categorie)CategoryPicker.SelectedItem;

            decimal price;
            if (!decimal.TryParse(PriceEntry.Text, out price))
            {
                await DisplayAlert("Validation Error", "Please enter a valid price.", "OK");
                return;
            }

            var newProduct = new Product
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                Price = price, 
                CategoryId = selectedCategory.Id 
            };

            if (string.IsNullOrEmpty(ProductId))
            {
                await ProductService.AddProductAsync(newProduct); 
                await DisplayAlert("Success", "Product added successfully!", "OK");
            }
           

            await Navigation.PopAsync();
        }
    }
}
