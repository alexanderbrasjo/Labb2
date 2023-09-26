using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class CartItem
    {
        private string _name;
        private decimal _price;
        private int _amount;
        private decimal _total;
        public CartItem(string name, decimal price, int amount)
        {
            _name = name;
            _price = price;
            _amount = amount;
            _total = amount * price;

        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; _total = value * _price;  }
        }
        public decimal Total
        {
            get { return _total; }
        }
        

    }
}
