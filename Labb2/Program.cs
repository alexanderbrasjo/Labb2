using System.Reflection.Metadata.Ecma335;

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
            AddRegistratedCustomers();
            AddRegistratedProducts();
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
            Console.Clear();
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.WriteLine($"Logged in as {activeCustomer.name} \n");
            Console.WriteLine("1. SHOP");
            Console.WriteLine("2. SHOW CART");
            Console.WriteLine("3. CHECKOUT");
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

                    break;
                default:

                    break;
            }
        }
        static void ShowCart()
        {
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.WriteLine($"Logged in as {activeCustomer.name} \n");

            List<Product> tempList = new List<Product>(activeCustomer.Cart);


            for (int i = 0; i < tempList.Count - 1; i++)
            {
                if(tempList.ElementAt(i).name == "pitt")
                {
                    continue;
                }
                int productCounter = 0;
                for(int j = i + 1; j < tempList.Count; j++)
                {
                    Console.WriteLine($"{tempList.ElementAt(i).name}, {tempList.ElementAt(j).name}");
                    if (tempList.ElementAt(j).name.Equals("pitt"))
                    {
                        continue;
                    }
                    if (tempList.ElementAt(i).Equals(tempList.ElementAt(j)))
                    {
                        productCounter++;
                        tempList.ElementAt(j).name = "pitt";
                    }
                }
                Console.WriteLine($"antal likadana: {tempList.ElementAt(i).name} = {productCounter}");
                //tempList.ElementAt(i).name = "";
            }
        }
        static void PrintProducts()
        {
            Console.WriteLine("******  SNUSBOLAGET  ******\n\n");
            Console.WriteLine($"Logged in as {activeCustomer.name} \n");
            foreach (Product p in products)
            {
                Console.WriteLine($"{products.IndexOf(p) + 1}. {p.name}         {p.price} ");
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
                Console.Write($" I want the product on line: ");
                int productChoice = Convert.ToInt32(Console.ReadLine());
                
                Product shoppedProduct = products.ElementAt(productChoice - 1);
                Console.Write($"{shoppedProduct.name}, How many? ");
                int countOfProduct = Convert.ToInt32(Console.ReadLine());
                for(int i = 0; i < countOfProduct; i++)
                {
                    activeCustomer.Cart.Add(shoppedProduct);
                }
                Console.WriteLine($"Putting {countOfProduct} {shoppedProduct.name} in your cart");
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
            foreach (Product p in activeCustomer.Cart)
            {
                Console.WriteLine($"shopping items : {p.name} och {p.price}");

            }
            MainMenu();


        }
        static bool RegisterNewAccount()
        {
            Console.Clear();
            Console.Write("Please enter the username you like to use: ");
            string userName = Console.ReadLine();
            Console.Write("Please enter a password: ");
            string passWord = Console.ReadLine();
            foreach (Customer c in customers)
            {
                Console.WriteLine(c.name);
            }
            AddCustomer(userName, passWord);

            activeCustomer = GetCustomer(userName); //new Customer(customers.ElementAt(customers.Count() - 1).name, customers.ElementAt(customers.Count() - 1).password);
            return true;
            foreach (Customer c in customers)
            {
                Console.WriteLine(c.name);
            }
            Console.WriteLine(activeCustomer.name + " " + activeCustomer.password);



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
                if (temporaryCustomer.password.Equals(passWord))
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
        static Customer GetCustomer(string user)
        {
            foreach(Customer c in customers) 
            {
                if (c.name.Equals(user))
                {
                    return c;
                }
            }
            return null;
        }
        static void AddRegistratedCustomers()
        {
            AddCustomer("Knatte", "123");
            AddCustomer("Fnatte", "321");
            AddCustomer("Tjatte", "213");
        }
        static void AddRegistratedProducts()
        {
            AddProducts("General, dosa", 51.90);
            AddProducts("General, stock", 479.90);
            AddProducts("Göteborgs Rape, dosa", 48.90);
            AddProducts("Göteborgs Rape, stock", 449.90);
            AddProducts("Ettan lös, dosa", 54.90);
            AddProducts("Ettan lös, stock", 499.90);
            AddProducts("Lundgrens Skåne, dosa", 41.90);
            AddProducts("Lundgrens Skåne, stock", 389.90);
            AddProducts("LOOP Jalapeno/Lime, dosa", 43.90);
            AddProducts("LOOP Jalapeno/Lime, stock", 399.90);

        }
        public static void AddCustomer(string name, string password)
        {
            customers.Add(new Customer(name, password));
        }
        public static void AddProducts(string name, double price)
        {
            products.Add(new Product(name, price));
        }
    }
}