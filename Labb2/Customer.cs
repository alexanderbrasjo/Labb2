using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Customer
    {
        //public int Id { get; set; }
        private string _name;
        private string _password;
        private Cart _myCart = new Cart();
        
        public Customer(string name, string password)
        {
            _name = name;
            _password = password;
            _myCart = new Cart();
        }
        //public void ShoppingProducts()
        //{

        //}
        //public void ToString()
        //{

        //}
        public string Name
        {
            get { return _name; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public List<CartItem> Cart
        {
            get { return _myCart.CartItems; }
            set { _myCart.CartItems = value; }
        }
        
        
    }
}
