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
            PrintHeader();
            Console.WriteLine("1.  Log in");
            Console.WriteLine("2.  Register new account");
            Console.Write("Enter the line number you want to use: ");
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
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Please choose one of the options");
                    StartMenu();
                    break;

            }

        }
        static void MainMenu()
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.Clear();
                PrintLoggedInHeader();
                Console.WriteLine("1. SHOP");
                Console.WriteLine("2. SHOW CART");
                Console.WriteLine("3. CHECKOUT");
                Console.WriteLine("9. LOG OUT");
                Console.Write("Enter the line number you want to use: ");
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
            
            bool finished = false;
            while (!finished)
            {
                Console.Clear();
                PrintLoggedInHeader();

                if (activeCustomer.Cart.Count == 0)
                {
                    Console.Clear();
                    PrintLoggedInHeader();
                    Console.WriteLine("Your cart is empty.");
                }
                activeCustomer.PrintCart();
                Console.WriteLine("1. REMOVE ITEM");
                Console.WriteLine("2. EMPTY CART");
                Console.WriteLine("3. CONTINUE SHOPPING");
                Console.WriteLine("4. GO TO CHECKOUT");
                Console.WriteLine("9. MAIN MENU");
                Console.Write("Enter the line number you want to use: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        RemoveProducts();
                        break;
                    case "2":
                        if (EmptyMyCart())
                        {
                            finished = true;
                        }
                        break;
                    case "3":
                        ShopMenu();
                        finished = true;
                        break;
                    case "4":

                        break;
                    case "9":
                        finished = true;
                        break;

                }
            }
        }
        static void CheckOut()
        {
            PrintLoggedInHeader();
            activeCustomer.PrintCart();
            Console.WriteLine("1. GO TO PAYMENT");
            Console.WriteLine("9. MAIN MENU");
            Console.Write("Enter the line number you want to use: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":   
                    Payment()
                        break;
                case "9":
                    MainMenu();
                    break;
            }

        }
        static void Payment()
        {
            PrintLoggedInHeader();

        }

        static void PrintHeader()
        {
            Console.Clear();
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            
        }
        static void PrintLoggedInHeader()
        {
            Console.Clear();
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.WriteLine($"Logged in as {activeCustomer.Name} \n");
            
        }
        static void PrintProducts()
        {
            
            foreach (Product p in products)
            {
                Console.WriteLine($"{products.IndexOf(p) + 1}.\t{p.Name}\t\t{p.Price} kr");
            }
            Console.WriteLine("Please use the line number to choose product");
        }
        static bool EmptyMyCart() 
        {
            Console.WriteLine("Do you really want to empty your cart? Y or N");
            string input = Console.ReadLine();
            if(input.ToUpper().Equals("Y"))
            {
                Console.WriteLine("Removed all items in your cart.");
                activeCustomer.EmptyCart();
                return true;
            }
            return false;
        }
        static void RemoveProducts()
        {
            bool finished = false;
            while (!finished)
            {
                Console.Clear();
                PrintLoggedInHeader();
                activeCustomer.PrintCart();
                Console.Write("Remove from line?: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                CartItem tempCartItem = activeCustomer.GetCartItemFromIndex(choice - 1);
                Console.Write("How many do you want to remove?: ");
                choice = Convert.ToInt32(Console.ReadLine());

                //Console.WriteLine($"Confirm removing {choice} {tempCartItem.Name} from you cart, ");
                activeCustomer.RemoveProductFromMyCart(tempCartItem, choice);
                Console.WriteLine($"Removed {choice} {tempCartItem.Name} from your cart");
                Console.WriteLine("You want to remove something else? Press Y or N");
                string stringChoice = Console.ReadLine();

                if(stringChoice.ToUpper() == "N")
                {
                    finished = true;
                    continue;
                }

                if(stringChoice.ToUpper() != "Y")
                {
                    Console.WriteLine("Please choose between 'Y' or 'N'");
                }

                //activeCustomer.PrintCart();
                //Console.ReadKey();
            }
        }

        static void ShopMenu()
        {
            Console.Clear();
            PrintLoggedInHeader();
            PrintProducts();
            bool keepShopping = true;
            while (keepShopping)
            {
                Console.Write($"I want the product on line: ");
                int productChoice = Convert.ToInt32(Console.ReadLine());
                
                Product shoppedProduct = products.ElementAt(productChoice - 1);
                Console.Write($"{shoppedProduct.Name}, How many? ");
                int countOfProduct = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"Putting {countOfProduct} {shoppedProduct.Name} in your cart");
                activeCustomer.AddProductToMyCart(shoppedProduct, countOfProduct);

                Console.Write("Do you want something else? Y or N ");
                string continueShopping = Console.ReadLine().ToUpper();

                Console.WriteLine($"prdouct = {shoppedProduct.Name} Pris = {shoppedProduct.Price} antal = {countOfProduct} total = {shoppedProduct.Price * countOfProduct}");
                Console.WriteLine(activeCustomer.Cart.Count);
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
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");

            bool checkingRegistration = false;
            while (!checkingRegistration)
            {
                Console.Write("Please enter the username you like to use: ");
                string userName = Console.ReadLine();

                if (GetCustomer(userName) == null)
                {
                    Console.Write("Please enter a password: ");
                    string passWord = Console.ReadLine();
                    activeCustomer = CreateCustomer(userName, passWord);
                    customers.Add(activeCustomer);
                    checkingRegistration = true;
                    continue;
                }

                Console.WriteLine("The account name is already used.");

            }
            return true;
        }

        static bool LogIn()
        {

            Console.Clear();
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.Write("Please enter your accountname: ");
            
            string userName = Console.ReadLine();
            Customer  temporaryCustomer = GetCustomer(userName);
            if(temporaryCustomer != null)
            {
                for(int wrong = 2; wrong >= 0; wrong--)
                {
                    Console.Write("Please enter your password: ");
                    string passWord = Console.ReadLine();
                    if (temporaryCustomer.Password.Equals(passWord))
                    {
                        activeCustomer = temporaryCustomer;
                        return true;
                    }
                    if(wrong > 0)
                    {
                        Console.WriteLine("Wrong password, please try again! (You got " + wrong + " tries left)");
                    }

                }
                Console.WriteLine("Try another day!");
                return false;
            }
            

            
            Console.WriteLine($"{userName} is not a valid username");
            Console.WriteLine($"Maybe you haven't registered an account yet, Do you like to registrate a new account? Press Y or N.");
            string answerInput = Console.ReadLine().ToUpper();
            if(answerInput.Equals("Y"))
            {
                Console.WriteLine("Please press enter to 'Register new account' on the next menu");
                Console.ReadKey();
                StartMenu();
            }
            return false;

        }
       
        static CartItem? GetCartItem(string name)
        {
            foreach (CartItem c in activeCustomer.Cart)
            {
                if (c.Name.Equals(name))
                {
                    return c;
                }
            }
            return null;
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
            CreateProduct("General dosa", 51.90M);
            CreateProduct("General stock", 479.90M);
            CreateProduct("Göteborgs Rape dosa", 48.90M);
            CreateProduct("Göteborgs Rape stock", 449.90M);
            CreateProduct("Ettan lös dosa", 54.90M);
            CreateProduct("Ettan lös stock", 499.90M);
            CreateProduct("Lundgrens Skåne dosa", 41.90M);
            CreateProduct("Lundgrens Skåne stock", 389.90M);
            CreateProduct("LOOP Jalapeno/Lime dosa", 43.90M);
            CreateProduct("LOOP Jalapeno/Lime stock", 399.90M);

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