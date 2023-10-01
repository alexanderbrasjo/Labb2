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
        static string customersFilePath = @".\\customers.json";
        static string customersDefaultFilePath = @".\\data\\customers.json";




        static void Main(string[] args)
        {
            RunProgram();
            ExportCustomers();
        }
        static void RunProgram()
        {
            LoadCustomers();
            CreateRegistratedProducts();
            StartMenu();
        }
        static void StartMenu()
        {
            Console.Clear();
            PrintHeader();
            bool startCheck = false;
            while (!startCheck)
            {
                Console.Clear();
                PrintHeader();
                Console.WriteLine("Welcome to Snusbolaget\n");
                Console.WriteLine("A snus shop like Snusbolaget offers a specialized platform with an extensive collection of snus products.\n" +
                    "Our range includes various snus varieties, flavors, and strengths,\nalong with practical accessories such as cans and tools for" +
                    " snus use.\nAt Snusbolaget, there are experts in snus culture available to provide advice and guidance on products and the best\n" +
                    "usage techniques for customers.\nPlease note that there is an age limit for the purchase of snus products, " +
                    "as they contain nicotine and are regulated\naccording to applicable laws.\nVisiting Snusbolaget provides snus enthusiasts " +
                    "with the opportunity to explore and find their favorite snus products\nwith the assistance of specialized experts in the field\n");
                Console.WriteLine("1.  LOG IN");
                Console.WriteLine("2.  REGISTER NEW ACCOUNT");
                Console.Write("Enter the line number you want to use: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        (Customer tempCustomer,string enteredUserName) = LogInMenu();
                        if (tempCustomer == null)
                        {
                            Console.WriteLine($"We couldn't find any accounts with {enteredUserName}");
                            Thread.Sleep(3000);
                            if (RegisterNewAccountMenu(enteredUserName) == null)
                            {
                                break;
                            }
                            MainMenu();
                        }
                        else
                        {
                            if (!LogInUserMenu(tempCustomer))
                            {
                                Console.WriteLine("You didnt input the correct password");
                                break;
                            }
                            MainMenu();
                        }
                        break;
                    case "2":
                        if (RegisterNewAccountMenu() == null)
                        {
                            break;
                        }
                        MainMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please choose one of the options");
                        continue;

                }
            }

        }
        static bool LogInUserMenu(Customer customer)
        {
            for (int wrong = 2; wrong >= 0; wrong--)
            {
                Console.Write("Please enter your password: ");
                string passWord = Console.ReadLine();
                if (customer.Password.Equals(passWord))
                {
                    activeCustomer = customer;
                    return true;
                }
                if (wrong > 0)
                {
                    Console.WriteLine("Wrong password, please try again! (You got " + wrong + " tries left)");
                }
            }
            return false;

            //Console.WriteLine($"{userName} is not a valid username");
            //Console.WriteLine($"Maybe you haven't registered an account yet, Do you like to registrate a new account? Press Y or N.");
            //string answerInput = Console.ReadLine().ToUpper();
            //if (answerInput.Equals("Y"))
            //{
            //    Console.WriteLine("Please press enter and choose 'Register new account' on the next menu");
            //    Console.ReadKey();
            //    return false;
            //}
            //if (answerInput.Equals("N"))
            //{
            //    return false;
            //}
        }
        static (Customer?, string) LogInMenu()
        {
            Console.Clear();
            PrintHeader();
            Console.Write("Please enter your accountname: ");
            
            string userName = Console.ReadLine();
            return (GetCustomer(userName),userName);
            
        }
        static Customer? RegisterNewAccountMenu(string? userName = null)
        {
            Console.Clear();
            PrintHeader();
            while (true)
            {
                if(userName == null)
                {
                    while (true)
                    {
                        Console.Write("Choose your account name: ");
                        userName = Console.ReadLine();
                        if (GetCustomer(userName) != null)
                        {
                            Console.WriteLine("That account name is already taken, please choose another name");
                            continue;
                        }
                        break;

                    }
                }
                Console.WriteLine($"Do you want to create an account using {userName}? (Y or N)");
                string choice = Console.ReadLine().ToUpper();
                if(choice == "Y")
                {
                    Console.Write("Please enter a password: ");
                    string passWord = Console.ReadLine();
                    Console.WriteLine("Choose prefered currency:");
                    Console.WriteLine("Press enter for SEK else choose 1 or 2");
                    Console.WriteLine("1. AMERICAN DOLLAR");
                    Console.WriteLine("2. ENGLISH POUNDS");
                    string inputChoice = Console.ReadLine();
                    switch (inputChoice)
                    {
                        case "1":
                            activeCustomer = CreateCustomer(userName, passWord,"USD");
                            break;
                        case "2":
                            activeCustomer = CreateCustomer(userName, passWord,"GBP");
                            break;
                        default:
                            activeCustomer = CreateCustomer(userName, passWord);
                            break;
                    }
                    return activeCustomer;
                }
                if(choice == "N")
                {
                    return null;
                }
                Console.WriteLine("Please choose between Y or N");
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
                Console.WriteLine("10. SAVE AND EXIT PROGRAM");
                Console.Write("Enter the line number you want to use: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShopMenu();
                        break;
                    case "2":
                        ShowCartMenu();
                        break;
                    case "3":
                        CheckOutMenu();
                        break;
                    case "9":
                        loggedIn = false;
                        break;
                    case "10":
                        ExportCustomers();
                        Environment.Exit(0);
                        break;
                    default:

                        break;
                }
            }
            StartMenu();
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
        static void ShowCartMenu()

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
                activeCustomer.PrintTotalPrice();
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
                        CheckOutMenu();
                        return;
                    case "9":
                        finished = true;
                        break;

                }
            }
        }
        static void CheckOutMenu()
        {
            PrintLoggedInHeader();
            activeCustomer.PrintCart();
            activeCustomer.PrintTotalPrice();
            activeCustomer.PrintDiscountedPrice();

            Console.WriteLine("1. CHOOSE PAYMENT METHOD");
            Console.WriteLine("9. RETURN TO MAIN MENU");
            Console.Write("Enter the line number you want to use: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PaymentMenu();
                        break;
                case "9":
                    MainMenu();
                    break;
            }

        }
        static void PaymentMenu()
        {
            PrintLoggedInHeader();
            activeCustomer.PrintCart();
            activeCustomer.PrintTotalPrice();
            activeCustomer.PrintDiscountedPrice();

            Console.WriteLine("1. PAY BY CARD");
            Console.WriteLine("2. PAY BY SWISH");
            Console.WriteLine("9. RETURN TO CHECKOUT");
            Console.Write("Enter the line number you want to use: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PayByCardMenu();
                    break;
                case "2":
                    PayBySwishMenu();
                    break;
                case "9":
                    CheckOutMenu();
                    break;
            }


        }
        static bool PayByCardMenu()
        {
            PrintLoggedInHeader();
            activeCustomer.PrintCart();
            activeCustomer.PrintTotalPrice();
            activeCustomer.PrintDiscountedPrice();
            string cardInput;
            string cvcInput;

            while (true)
            {
                Console.Write("Enter your card number (16 digits):");
                cardInput = Console.ReadLine();
                if (!ContainsOnlyNumbers(cardInput))
                {
                    Console.WriteLine("You didnt input digits.");
                    continue;
                }
                if (cardInput.Length < 16 || cardInput.Length > 16)
                {
                    Console.WriteLine("You didnt input a correct card number.");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.WriteLine("Enter your CVC number (3 digits)");
                cvcInput = Console.ReadLine();
                if (!ContainsOnlyNumbers(cvcInput))
                {
                    Console.WriteLine("You didnt input digits");
                    continue;
                }
                if (cvcInput.Length < 3 || cvcInput.Length > 3)
                {
                    Console.WriteLine("You didnt input a correct CVC number");
                    continue;
                }
                break;
            }
                
            while (true)
            {
                Console.WriteLine("Card Payment accepted, do you wish to complete the payment? Y or N");
                string acceptPayment = Console.ReadLine().ToUpper();
                if (acceptPayment.Equals("Y"))
                {
                    Console.WriteLine("Payment Successful! Thank you for shopping, snus is good for you :)");
                    activeCustomer.EmptyCart();
                    Thread.Sleep(3000);
                    return true;
                }
                if (acceptPayment.Equals("N"))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
        }
        static bool PayBySwishMenu()
        {
            PrintLoggedInHeader();
            activeCustomer.PrintCart();
            activeCustomer.PrintTotalPrice();
            activeCustomer.PrintDiscountedPrice();
            string phoneInput;
            string choiceInput;
            while (true)
            {
                Console.Write("Enter your phone number: ");
                phoneInput = Console.ReadLine();
                if (!ContainsOnlyNumbers(phoneInput))
                {
                    Console.WriteLine("You didnt input a correct phone number.");
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.WriteLine("Do you wish to complete the payment? Y or N\n");
                choiceInput = Console.ReadLine().ToUpper();

                if (choiceInput.Equals("Y"))
                {
                    Console.WriteLine("Payment Successful! Thank you for shopping, snus is good for you :)");
                    activeCustomer.EmptyCart();
                    Thread.Sleep(3000);
                    return true;
                }
                if (choiceInput.Equals("N"))
                {
                    return false;
                }
                Console.WriteLine("Please choose Y or N");
            }
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
                decimal convertedPrice = Math.Round(p.Price * activeCustomer.ConvertedCurrency, 2);
                Console.WriteLine($"{products.IndexOf(p) + 1}.\t{p.Name}\t\t{convertedPrice} {activeCustomer.Currency}");
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
                Thread.Sleep(2000);
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
        static bool ContainsOnlyNumbers(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
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
        static void LoadCustomers()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            //Console.WriteLine("Current Working Directory: " + currentDirectory);
            string text;
            if (File.Exists(customersFilePath))
            {
                text = File.ReadAllText(customersFilePath);
            }
            else
            {
                text = File.ReadAllText(customersDefaultFilePath);
            }
            customers = JsonSerializer.Deserialize<List<Customer>>(text);
            
            //Console.WriteLine($"Account name: {customers[0].Name}");
            //Console.WriteLine($"Password: {customers[0].Password}");

            //CreateGoldCustomer("Knatte", "123");
            //CreateSilverCustomer("Fnatte", "321","USD");
            //CreateBronzeCustomer("Tjatte", "213","GBP");
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
        static Customer CreateGoldCustomer(string name, string password, string currency = "SEK")
        {
            Customer customer = new GoldCustomer(name, password, currency);

            customers.Add(customer);
            return customer;

        }
        static Customer CreateSilverCustomer(string name, string password, string currency = "SEK")
        {
            Customer customer = new SilverCustomer(name, password, currency);

            customers.Add(customer);
            return customer;

        }
        static Customer CreateBronzeCustomer(string name, string password, string currency = "SEK")
        {
            Customer customer = new BronzeCustomer(name, password, currency);

            customers.Add(customer);
            return customer;

        }
        static Customer CreateCustomer(string name, string password,string currency = "SEK")
        {
            Customer customer = new Customer(name, password, currency);

            customers.Add(customer);
            return customer;

        }
        static Product CreateProduct(string name, decimal price)
        {
            Product product = new Product(name, price);

            products.Add(product);
            return product;
        }
        static void ExportCustomers()
        {
            string output = JsonSerializer.Serialize<List<Customer>>(customers, new JsonSerializerOptions
            {
                WriteIndented = true // Makes the JSON output more readable
            });
            string filePath = "customers.json";
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            try
            {
                // Write the string to the specified file
                File.WriteAllText(customersFilePath, output);

                Console.WriteLine("String saved to file successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
        
    }
}