using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2
{
    internal class BronzeCustomer : Customer
    {
        public BronzeCustomer(string name, string password, string currency) : base(name, password, currency)
        {

        }
        public override decimal Discount()
        {
            return 0.05M;
        }
    }
    internal class SilverCustomer : Customer
    {
        public SilverCustomer(string name, string password,string currency) : base(name, password, currency)
        {

        }
        public override decimal Discount()
        {
            return 0.10M;
        }
    }
    internal class GoldCustomer : Customer
    {
        public GoldCustomer(string name,string password, string currency) : base(name, password, currency)
        {

        }
        public override decimal Discount()
        {
            return 0.15M;
        }
    }
    internal class Customer
    {
        //public int Id { get; set; }
        private string _name;
        private string _password;
        private string _currency;
        private decimal _convertedCurrency;
        private Cart _myCart = new Cart();
        
        static Dictionary<string, decimal> currencyConverterMapping = new Dictionary<string, decimal>
        {
            { "SEK", 1},
            { "USD", 0.091M },
            { "GBP", 0.075M }
        };

        public Customer(string name, string password,string currency = "SEK")
        {
            _name = name;
            _password = password;
            _currency = currency;
            _myCart = new Cart();
            _convertedCurrency = currencyConverterMapping[_currency];
        }

        public override string ToString()
        {
            string toString = "Username: " + _name + "\nPassword: " + _password + "\n" + PrintCart() + "\n" + PrintTotalPrice();
            return toString;
        }
        public string Name
        {
            get { return _name; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
        public List<CartItem> Cart
        {
            get { return _myCart.CartItems; }
            set { _myCart.CartItems = value; }
        }
        public decimal ConvertedCurrency
        {
            get { return _convertedCurrency; }
            set { _convertedCurrency = value; }
        }
        
        public virtual decimal Discount()
        {
            return 0M;
        }
        
        public void AddProductToMyCart(Product product,int count)
        {
            _myCart.AddProduct(product, count);
        }

        public string PrintTotalPrice()
        {
            decimal totalPrice = 0M;
            foreach (CartItem cartItem in _myCart.CartItems)
            {
                totalPrice += cartItem.TotalPrice();
            }
            string printTotalPrice = "Total price: " + Math.Round(totalPrice * _convertedCurrency,2) + " " + _currency + " \n";

            Console.WriteLine(printTotalPrice);
            return printTotalPrice;
        }

        public string PrintCart()
        {
            string cartPrint = "";
            int cartIndex = 0;
            foreach (CartItem cartItem in _myCart.CartItems)
            {
                cartIndex++;
                cartPrint += cartIndex.ToString() + ". " + cartItem.Name + " , " + (Math.Round(cartItem.Price * _convertedCurrency,2).ToString()) + 
                    " * " + cartItem.Amount.ToString() + "  " + (Math.Round(cartItem.TotalPrice() * _convertedCurrency,2).ToString()) + " " + _currency + "\n";
            }
            Console.WriteLine(cartPrint);
            return cartPrint;

        }
        public void PrintDiscountedPrice()
        {
            decimal convertedCurrency = currencyConverterMapping[_currency];
            if (_myCart.GetTotalPrice() == PriceWithDiscount())
            {
                return;
            }
            Console.WriteLine($"Discounted price: {Math.Round(PriceWithDiscount() * convertedCurrency,2)} {_currency}\n");
        }
        public decimal PriceWithDiscount()
        {
            decimal discountedPrice = _myCart.GetTotalPrice() - Discount() * _myCart.GetTotalPrice();
            return discountedPrice;
        }
        public void EmptyCart()
        {
            _myCart.RemoveAllCartItems();
        }
        public void RemoveProductFromMyCart(CartItem cartItem, int amount)
        {
            _myCart.RemoveProductFromCart(cartItem, amount);
        }
        public CartItem GetCartItemFromIndex(int index)
        {
            return _myCart.GetCartItemFromIndex(index);
        }
        
    }
}
