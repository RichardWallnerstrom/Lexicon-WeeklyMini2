using System;
using System.Collections.Generic; //List<T>
using System.Globalization; //CultureInfo.InvariantCulture
using System.Linq;
using CC = System.ConsoleColor;


namespace Lexicon.Mini2
{
    public class Program
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
}
