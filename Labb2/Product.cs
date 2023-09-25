using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public class Product
    {
        private string Name { get; set; }
        // Jag tror du vill ha en decimal här istället för en double.
        // decimal är mer lämpad för pengar då double får avrundningsfel.
        // private decimal Price { get; set; }
        private double Price { get; set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        public double price
        {
            get { return Price; }
            set { Price = value; }
        }
    }
}
