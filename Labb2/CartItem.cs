using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class CartItem
    {
        public Product _product;
        public int _amount;
        public decimal _total;
        public CartItem(Product product, int amount)
        {
            _product = product;
            _amount = amount;
            _total = amount * product.Price;

        }
        public Product Product
        {
            get { return _product; }
        }
        public string Name
        {
            get { return _product.Name; }
        }
        public decimal Price
        {
            get { return _product.Price; }
        }
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public void RemoveOne()
        {
            if(_amount > 0)
            {
                _amount--;
            }
        }
        public void AddOne()
        {
            _amount++;
        }
        public decimal TotalPrice()
        {
            return _amount * _product.Price;
        }
        


    }
}
