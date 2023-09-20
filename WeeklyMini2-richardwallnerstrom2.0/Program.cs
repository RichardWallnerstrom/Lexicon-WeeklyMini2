using System;
using System.Collections.Generic; //List<T>
using System.Globalization; //CultureInfo.InvariantCulture
using System.Linq;
using CC = System.ConsoleColor;

class Product
{
    public string Category { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string category, string name, double price)
    {
        Category = category;
        Name = name;
        Price = price;
    }
    public static void AddProduct(List<Product> products)
    {
        Program.Print("Please type in desired category: ");
        string category = Console.ReadLine().Trim();
        Program.Print("Please type in product name: ");
        string name = Console.ReadLine().Trim();
        Program.Print("Please type in price: ");
        string priceInput = Console.ReadLine().Trim().Replace(',', '.');
        if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(name))
        {
            Program.PrintLine("Category and name cannot be empty.", CC.White, CC.DarkRed);
            Program.PrintLine("---------------------------------------", CC.DarkBlue);
            return;
        }

        //Make sure price accepts both period and comma to seperate the decimals 
        double price;
        //Convert and Enforce regional system to allow period in decimal numbers 
        if (double.TryParse(priceInput, NumberStyles.Any, CultureInfo.InvariantCulture, out price))
        {
            products.Add(new Product(category, name, price));
            Program.PrintLine("---------------------------------------", CC.DarkBlue);
            Program.PrintLine($"{name} added successfully.", CC.DarkGreen);
            Program.PrintLine("---------------------------------------", CC.DarkBlue);

        }
        else // If price is null or not number
        {
            Program.PrintLine($"Invalid price. {name} not added.", CC.White, CC.DarkRed);
            Program.PrintLine("---------------------------------------", CC.DarkBlue);

        }

    }
    public static void DisplayProducts(List<Product> products)
    {
        Program.PrintLine("---------------------------------------", CC.DarkBlue);
        Program.PrintLine($"{"Category",-10}{"Name",-10} Price", CC.DarkGreen);
        Program.PrintLine("---------------------------------------", CC.DarkBlue);
        products = products.OrderBy(x => x.Price).ThenBy(x => x.Category).ThenBy(x => x.Name).ToList(); 
        foreach (var product in products)
        {
            Program.PrintLine($"{product.Category,-10}{product.Name,-10}{product.Price}");
        }
        double totalPrice = products.Sum(product => product.Price);
        double totalItems = products.Count;
        Program.PrintLine("---------------------------------------", CC.DarkBlue);
        Program.Print($"Total Items: {totalItems}     Total Price: ", CC.DarkYellow);
        Program.Print($"{totalPrice} ", CC.DarkGreen);
        Program.Print("$\n", CC.DarkRed);
        Program.PrintLine("---------------------------------------", CC.DarkBlue);

    }
    public static void SearchProducts(List<Product> products)
    {
        Program.Print("What are you looking for? ");        // Search for either category or name and add all entries to list
        string keyword = Console.ReadLine().ToLower().Trim();
        var searchResults = products.Where(product =>
        product.Category.ToLower().Contains(keyword) ||
        product.Name.ToLower().Contains(keyword));
        if (searchResults.Any())
        {
            Program.PrintLine("---------------------------------------", CC.DarkBlue);
            Program.PrintLine("Search Results", CC.DarkYellow);
            DisplayProducts(searchResults.ToList());
            Program.PrintLine("---------------------------------------", CC.DarkBlue);

        }
        else
        {
            Program.PrintLine("No products match your search criteria.", CC.DarkRed);
            Program.PrintLine("---------------------------------------", CC.DarkBlue);
        }
    }
}

class Program
{
    public static void PrintWelcome()
    {
        PrintLine("---------------------------------------", CC.DarkBlue);
        Print("| ", CC.DarkBlue);
        Print("Welcome to the Product List Manager", CC.DarkYellow);
        Print(" |\n", CC.DarkBlue);
        PrintLine("---------------------------------------", CC.DarkBlue);
    }
    public static void PrintLegend()
    {
        Program.PrintLine("Press 'p' to add a new product.");
        Program.PrintLine("Press 'd' to display products.");
        Program.PrintLine("Press 's' to search for products.");
        Program.PrintLine("Press 'q' to quit application.");
        PrintLine("---------------------------------------", CC.DarkBlue);
        Program.Print("What would you like to do? ");
    }
    public static void Print(string text, CC fgColor = CC.White, CC bgColor = CC.Black) //Color change
    {
        Console.ForegroundColor = fgColor;
        Console.BackgroundColor = bgColor;
        Console.Write(text);
        Console.ResetColor();
    }
    public static void PrintLine(string text, CC fgColor = CC.White, CC bgColor = CC.Black)
    {
        Console.ForegroundColor = fgColor;
        Console.BackgroundColor = bgColor;
        Console.WriteLine(text);
        Console.ResetColor();
    }



    static void Main()
    {
        List<Product> products = new List<Product>();
        PrintWelcome();
        while (true)
        {
            PrintLegend();
            string userInput = Console.ReadLine().ToLower().Trim();

            if (userInput == "q" || userInput == "quit")
            {
                PrintLine("Exiting application.");
                break;
            }
            else if (userInput == "p" || userInput == "add")
            {
                Product.AddProduct(products);

            }
            else if (userInput == "s" || userInput == "search")
            {
                Product.SearchProducts(products);

            }
            else if (userInput == "d" || userInput == "display")
            {
                Product.DisplayProducts(products);
            }
            else
            {
                PrintLine("Incorrect input. ");
                PrintLine("---------------------------------------");

            }
        }



    }
}