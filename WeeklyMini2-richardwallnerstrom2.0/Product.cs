using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC = System.ConsoleColor;

namespace Lexicon.Mini2
{
    public class Product
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
            Program.Print($"Total Items: ", CC.DarkYellow);
            Program.Print($"{totalItems} ");
            Program.Print("Total Price: ", CC.DarkYellow);
            Program.Print($"{totalPrice} ");
            Program.Print("$\n", CC.DarkGreen);
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
}
