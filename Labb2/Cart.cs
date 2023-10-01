using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Cart
    {
        private List<CartItem> _cartItems;

        public Cart()
        {
            _cartItems = new List<CartItem>();
        }
        public List<CartItem> CartItems
        {
            get { return _cartItems; }
            set { _cartItems = value; }
        }
        public void AddProduct(Product product, int count)
        {
            bool productExist = _cartItems.Any(cartItem => cartItem.Product == product);
            if (productExist)
            {
                CartItem existingCartItem = getCartItemFromProduct(product);
                existingCartItem.Amount += count;
            }
            else
            {
                CartItem cartItem = new CartItem(product, count);
                _cartItems.Add(cartItem);

            }
        }
        public CartItem getCartItemFromProduct(Product product)
        {
            foreach (CartItem cartItem in _cartItems)
            {
                if (cartItem.Product.Equals(product))
                {
                    return cartItem;
                }
            }
            return null;
        }
        public void AddCartItem(CartItem item)
        {
            _cartItems.Add(item);
        }
        public void RemoveProductFromCart(CartItem cartItem ,int amount)
        {
            if(amount >= cartItem.Amount)
            {
                _cartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Amount -= amount;
            }

        }
        
        
        
        public decimal GetTotalPrice() 
        {
            decimal totalSum = 0M;
            foreach (CartItem cartItem in _cartItems)
            {
                totalSum += cartItem.TotalPrice();
            }
            return totalSum;
        }
        public void RemoveAllCartItems()
        {
            _cartItems.Clear();

        }
        public CartItem GetCartItemFromIndex(int index)
        {
            return _cartItems.ElementAt(index);
        }
        //public List<CartItem> GetMyCart()
        //{
        //    return _cartItems;
        //}
    }
}
