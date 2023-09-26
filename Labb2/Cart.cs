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
        
        public void AddCartItem(CartItem item)
        {
            _cartItems.Add(item);
        }
        //public List<CartItem> GetMyCart()
        //{
        //    return _cartItems;
        //}
    }
}
