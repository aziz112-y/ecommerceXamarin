using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using e_comm.Model;
using e_comm.Services;
using e_comm.View;

namespace e_comm.ViewModel
{
    public class ProduitViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private readonly CategorieService _categoryService;

        // Original list of products
        private List<Product> _allProducts;

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Categorie> Categories { get; set; }
        public Categorie SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                FilterProductsByCategory();
            }
        }
        private Categorie _selectedCategory;

        public ICommand AddToCartCommand { get; }
        public ICommand ViewCartCommand { get; }

        public ProduitViewModel()
        {
            _productService = new ProductService();
            _categoryService = new CategorieService();
            Products = new ObservableCollection<Product>();
            Categories = new ObservableCollection<Categorie>();
            AddToCartCommand = new Command<Product>(AddToCart);
            ViewCartCommand = new Command(ViewCart);
            LoadCategories();
            LoadProducts();
        }

        private async void LoadCategories()
        {
            IsBusy = true;
            try
            {
                var categories = await _categoryService.GetCategoriesAsync();
                Categories.Clear();
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        if (category != null && category.Name != null)
                        {
                            Categories.Add(category);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void LoadProducts()
        {
            IsBusy = true;
            try
            {
                var products = await _productService.GetProduitsAsync();
                if (products != null)
                {
                    _allProducts = products.ToList(); 
                    UpdateProductList(_allProducts);  
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddToCart(Product product)
        {
            if (product != null)
            {
                CartService.AddToCart(product);
                Application.Current.MainPage.DisplayAlert("Success", $"{product.Name} added to cart.", "OK");
            }
        }

        private async void ViewCart()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
        }

        private void FilterProductsByCategory()
        {
            if (SelectedCategory != null && _allProducts != null)
            {
                var filteredProducts = _allProducts.Where(p => p.CategoryId == SelectedCategory.Id).ToList();
                UpdateProductList(filteredProducts);
            }
            else
            {
                UpdateProductList(_allProducts ?? new List<Product>());
            }
        }

        private void UpdateProductList(IEnumerable<Product> products)
        {
            Products.Clear();
            if (products != null)
            {
                foreach (var product in products)
                {
                    if (product != null)
                    {
                        Products.Add(product);
                    }
                }
            }
        }
    }
}
