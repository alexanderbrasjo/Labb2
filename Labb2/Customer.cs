using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public class Customer
    {
        private string Name { get; }
        private string Password { get; set; }
        private List<Product> _cart;
        public List<Product> Cart { get { return _cart; } }
        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            _cart = new List<Product>();
        }
        public void ShoppingProducts()
        {

        }
        public void ToString()
        {

        }
        public string name
        {
            get { return Name; }
        }
        
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }
    }
}
