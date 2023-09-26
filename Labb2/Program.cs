using Microsoft.VisualBasic;
using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Labb2
{
    internal class Program
    {
        static List<Customer> customers = new List<Customer>();
        static List<Product> products = new List<Product>();
        static Customer activeCustomer;
        //public Program()
        //{
        //    AddRegistratedCustomers();
        //    AddRegistratedProducts();
        //}
        

        static void Main(string[] args)
        {
            //CreateRegistratedCustomers();
            RunProgram();
            //foreach(Customer c in customers)
            //{
            //    Console.WriteLine(c.name);
            //}
            //foreach (Product p in products)
            //{
            //    Console.WriteLine(p.name + "  " + String.Format("{0:C2}", p.price));
            //}

            //Console.WriteLine("Hello, World!");
            //Customer nisse = new Customer("Nisse", "123");
            //nisse.SetName("Pelle");
            //Console.WriteLine(nisse.GetName());
            //Product apple = new Product("Apple", 3.90);
            //apple.name = "banana";
            //Console.WriteLine(apple.name);

        }
        static void RunProgram()
        {
            CreateRegistratedCustomers();
            CreateRegistratedProducts();
            StartMenu();
        }
        static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("******  WELCOME TO SNUSBOLAGET  ******\n\n");
            Console.WriteLine("1.  Log in");
            Console.WriteLine("2.  Register new account");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (LogIn())
                    {
                        MainMenu();
                    }
                    break;
                case "2":
                    RegisterNewAccount();
                    break;
                default:
                    Console.WriteLine("Please choose one of the options");
                    break;

            }

        }
        static void MainMenu()
        {
            bool loggedIn = true;
            while (loggedIn)
            {

                Console.Clear();
                Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
                Console.WriteLine($"Logged in as {activeCustomer.Name} \n");
                Console.WriteLine("1. SHOP");
                Console.WriteLine("2. SHOW CART");
                Console.WriteLine("3. CHECKOUT");
                Console.WriteLine("9. LOG OUT");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShopMenu();
                        break;
                    case "2":
                        ShowCart();
                        break;
                    case "3":
                        //CheckOut();
                        break;
                    case "9":
                        loggedIn = false;
                        break;
                    default:

                        break;
                }
            }
            StartMenu();
        }
        static void ShowCart()
        {
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.WriteLine($"Logged in as {activeCustomer.Name} \n");

            //List<Product> tempList = new List<Product>(activeCustomer.Cart);

            //for (int i = 0; i < tempList.Count; i++)
            //{
            //    if (tempList.ElementAt(i).Name == "")
            //    {
            //        continue;
            //    }
            //    int productCounter = 1;
            //    for (int j = i + 1; j < tempList.Count; j++)
            //    {
            //        Console.WriteLine($"{tempList.ElementAt(i).Name}, {tempList.ElementAt(j).Name}");
            //        if (tempList.ElementAt(j).Name.Equals(""))
            //        {
            //            continue;
            //        }
            //        if (tempList.ElementAt(i).Equals(tempList.ElementAt(j)))
            //        {
            //            productCounter++;
            //            tempList.ElementAt(j).Name = "";
            //        }
            //    }
            //    Console.WriteLine($"antal likadana: {tempList.ElementAt(i).Name} = {productCounter}");
            //    tempList.ElementAt(i).name = "";
            //}

            foreach (CartItem p in activeCustomer.Cart)
            {
                Console.Write($"{p.Name}     {p.Price}    {p.Amount}     {p.Total}  \n");
            }
            Console.ReadKey();
        }
        static void PrintProducts()
        {
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.WriteLine($"Logged in as {activeCustomer.Name} \n");
            foreach (Product p in products)
            {
                Console.WriteLine($"{products.IndexOf(p) + 1}.\t{p.Name}\t\t{p.Price} kr");
            }
            Console.WriteLine("Please use the line number to choose product");
        }

        static void ShopMenu()
        {
            Console.Clear();
            PrintProducts();
            bool keepShopping = true;
            while (keepShopping)
            {
                Console.Write($"I want the product on line: ");
                int productChoice = Convert.ToInt32(Console.ReadLine());
                
                Product shoppedProduct = products.ElementAt(productChoice - 1);
                Console.Write($"{shoppedProduct.Name}, How many? ");
                int countOfProduct = Convert.ToInt32(Console.ReadLine());
                CartItem cartItem = new CartItem(shoppedProduct.Name,shoppedProduct.Price, countOfProduct);
                activeCustomer.Cart.Add(cartItem);
                Console.WriteLine($"prdouct = {cartItem.Name} Pris = {cartItem.Price} antal = {cartItem.Amount} total = {cartItem.Total}");
                Console.WriteLine($"Putting {countOfProduct} {shoppedProduct.Name} in your cart");
                Console.WriteLine($"prdouct = {cartItem.Name} Pris = {cartItem.Price} antal = {cartItem.Amount}  total = {cartItem.Total}");
                Console.Write("Do you want something else? Y or N ");
                string continueShopping = Console.ReadLine().ToUpper();
                if (continueShopping.Equals("Y"))
                {
                    continue;
                }
                else
                {
                    keepShopping = false;
                }

            }


        }
        static bool RegisterNewAccount()
        {
            Console.Clear();
            Console.Write("Please enter the username you like to use: ");
            string userName = Console.ReadLine();
            Console.Write("Please enter a password: ");
            string passWord = Console.ReadLine();
            
            activeCustomer = CreateCustomer(userName, passWord);

            return true;
            



            //Console.Write("Please confirm your password: ");
            //string confirmPassword = Console.ReadLine();
            //if(!passWord.Equals(confirmPassword))
            //{
            //    Console.WriteLine("Your ");
            //    AddCustomer(userName, passWord);
            //}
            //else
            //{
            //    cw
            //}
        }
        static bool LogIn()
        {

            Console.Clear();
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.Write("Please enter your accountname: ");
            string userName = Console.ReadLine();
            Customer  temporaryCustomer = GetCustomer(userName);
            if (temporaryCustomer != null)
            {
                Console.Write("Please enter your password: ");
                string passWord = Console.ReadLine();
                if (temporaryCustomer.Password.Equals(passWord))
                {
                    Console.WriteLine("Access Granted");
                    activeCustomer = temporaryCustomer;
                    return true;
                }
            }
            
            Console.WriteLine($"{userName} is not a valid username");
            Console.WriteLine($"Maybe you haven't registered an account yet, Do you like to registrate a new account? Press Y or N.");
            string answerInput = Console.ReadLine().ToUpper();
            if(answerInput.Equals("Y"))
            {
                Console.WriteLine("Please press enter to 'Register new account' on the next menu");
                Console.ReadKey();
                StartMenu();
                return true;
            }
            return false;

        }
        static Customer? GetCustomer(string user)
        {
            foreach(Customer c in customers) 
            {
                if (c.Name.Equals(user))
                {
                    return c;
                }
            }
            return null;
        }
        static void CreateRegistratedCustomers()
        {
            //string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            //Console.WriteLine("Current Working Directory: " + currentDirectory);

            //if (File.Exists(@"./data/customers.json"))
            //{
            //    Console.WriteLine("Jupp");
            //}
            //string text = File.ReadAllText(@".\\data\\customers.json");
            //Console.WriteLine(text);
            //List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(text);

            //Console.WriteLine($"Account name: {customers[0].Name}");
            //Console.WriteLine($"Password: {customers[0].Password}");

            CreateCustomer("Knatte", "123");
            CreateCustomer("Fnatte", "321");
            CreateCustomer("Tjatte", "213");
        }
        

        static void CreateRegistratedProducts()
        {
            CreateProduct("General, dosa", 51.90M);
            CreateProduct("General, stock", 479.90M);
            CreateProduct("Göteborgs Rape, dosa", 48.90M);
            CreateProduct("Göteborgs Rape, stock", 449.90M);
            CreateProduct("Ettan lös, dosa", 54.90M);
            CreateProduct("Ettan lös, stock", 499.90M);
            CreateProduct("Lundgrens Skåne, dosa", 41.90M);
            CreateProduct("Lundgrens Skåne, stock", 389.90M);
            CreateProduct("LOOP Jalapeno/Lime, dosa", 43.90M);
            CreateProduct("LOOP Jalapeno/Lime, stock", 399.90M);

        }
        static Customer CreateCustomer(string name, string password)
        {
            Customer customer = new Customer(name, password);

            customers.Add(customer);
            return customer;

        }
        static Product CreateProduct(string name, decimal price)
        {
            Product product = new Product(name, price);

            products.Add(product);
            return product;
        }
    }
}