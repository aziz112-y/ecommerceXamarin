using System;
using e_comm.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_comm.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ProductId), "productId")]
    public partial class ProductDetailsPage : ContentPage
    {
        public string ProductId { get; set; }
        ProductService ProductService = new ProductService();

        public ProductDetailsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

           
                var product = await ProductService.GetProductByIdAsync(ProductId);
                if (product != null)
                {
                    ProductNameLabel.Text = product.Name;
                    ProductPriceLabel.Text = $"Prix : {product.Price:C}";
                    ProductDescriptionLabel.Text = product.Description;
                }
            
        }

      

        
    }
}
