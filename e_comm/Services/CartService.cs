using System.Collections.Generic;
using System.Linq;
using e_comm.Model;

namespace e_comm.Services
{
    public static class CartService
    {
        private static readonly List<cart> CartItems = new List<cart>();

        public static void AddToCart(Product product)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new cart { Product = product, Quantity = 1 });
            }
        }

        public static void RemoveFromCart(Product product)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (existingItem != null)
            {
                CartItems.Remove(existingItem);
            }
        }

        public static void UpdateQuantity(Product product, int quantity)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (existingItem != null && quantity > 0)
            {
                existingItem.Quantity = quantity;
            }
        }

        public static List<cart> GetCart()
        {
            return CartItems;
        }
    }
}
