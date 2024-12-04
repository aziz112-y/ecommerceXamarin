using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using e_comm.Model;
using e_comm.Services;

namespace e_comm.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<cart> CartItems { get; set; }
        public decimal TotalPrice { get; private set; }

        public ICommand IncrementQuantityCommand { get; }
        public ICommand DecrementQuantityCommand { get; }
        public ICommand RemoveFromCartCommand { get; }

        public CartViewModel()
        {
            CartItems = new ObservableCollection<cart>(CartService.GetCart());

            IncrementQuantityCommand = new Command<cart>(IncrementQuantity);
            DecrementQuantityCommand = new Command<cart>(DecrementQuantity);
            RemoveFromCartCommand = new Command<cart>(RemoveFromCart);

            CalculateTotalPrice();
        }

        private void IncrementQuantity(cart cartItem)
        {
            if (cartItem != null)
            {
                cartItem.Quantity++;
                CalculateTotalPrice();
            }
        }

        private void DecrementQuantity(cart cartItem)
        {
            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                CalculateTotalPrice();
            }
            else { RemoveFromCart(cartItem); }
        }

        private void RemoveFromCart(cart cartItem)
        {
            if (cartItem != null)
            {
                CartItems.Remove(cartItem);
                CartService.RemoveFromCart(cartItem.Product);
                CalculateTotalPrice();
            }
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = CartItems.Sum(item => item.Product.Price * item.Quantity);
            OnPropertyChanged(nameof(TotalPrice));
        }
    }
}
