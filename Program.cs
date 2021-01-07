using System;
using System.Linq;
using System.Collections.Generic;

namespace TaxieTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var salesTax = 0.00;
            var shoppingCart = new List<Item>();

            Console.WriteLine("How many entries would you like to add?");
            var categories = Convert.ToInt32(Console.ReadLine());
            for (var i = 0; i < categories; i++)
            {
                Console.WriteLine("Please enter the  item's category: ");
                var itemCategory = Console.ReadLine();

                Console.WriteLine("Please confirm how many items of this category you wish to add: ");
                var itemAmount = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please confirm the original price of the item with two decimal places: ");
                double itemPrice;
                if (!double.TryParse(Console.ReadLine(), out itemPrice))
                {
                    Console.WriteLine("Price could not be processed. Please make sure you entered the price correctly.");
                }

                Console.WriteLine("Please confirm if the item is imported ('yes' if imported, 'no' if not imported): ");
                var imported = Console.ReadLine();
                if (imported == "yes")
                {
                    shoppingCart.Add(new Item
                    {
                        Category = itemCategory,
                        Amount = itemAmount,
                        Price = itemPrice * itemAmount,
                        IsImported = true
                    });
                }
                if (imported == "no")
                {
                    shoppingCart.Add(new Item
                    {
                        Category = itemCategory,
                        Amount = itemAmount,
                        Price = itemPrice * itemAmount,
                        IsImported = false
                    });
                }
                else
                {
                    Console.WriteLine("Import status could not be processed. Please make sure you only submit 'yes' or 'no'.");
                }
            }

            foreach (var item in shoppingCart)
            {
                if (item.Category == "book" || item.Category == "food" || item.Category == "medical")
                {
                    break;
                }
                else
                {
                    salesTax += (item.Price / 10);
                    item.Price = (item.Price + (item.Price / 10));
                }
            }

            var importTaxable = shoppingCart.Where(i => i.IsImported);
            foreach (var item in importTaxable)
            {
                item.Price += ((item.Price / 100) * 5);
            }

            var total = shoppingCart.Sum(i => i.Price);
            foreach (var item in shoppingCart)
            {
                Console.WriteLine(item.Amount + " " + item.Category);
            }

            Console.WriteLine("Sales taxes: " + salesTax);
            Console.WriteLine("Total: " + total);
        }
        public class Item
        {
            public string Category { get; set; }

            public int Amount { get; set; }

            public double Price { get; set; }

            public bool IsImported { get; set;}
        }
    }
}
