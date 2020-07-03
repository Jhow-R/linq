using System;
using System.Collections.Generic;
using System.Linq;

namespace linq
{
    class Program
    {
        static void Main(string[] args)
        {
            Category c1 = new Category { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category { Id = 3, Name = "Eletronics", Tier = 1 };

            List<Product> products = new List<Product>() {
                new Product{ Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product{ Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Product{ Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product{ Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Product{ Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Product{ Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Product{ Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Product{ Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Product{ Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Product{ Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Product{ Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900);
            Print("TIER 1 AND PRICE < 900: ", r1);

            var r2 = products.Where(p => p.Category.Name.Equals("Tools")).Select(p => p.Name);
            Print("NAME OF PRODUCTS FROM TOOLS: ", r2);

            // Usando alias para não haver ambiguidade entre dois campos com a mesma nomenclatura "Name"
            var r3 = products.Where(p => p.Name.StartsWith("C")).Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name });
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT: ", r3);

            var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME: ", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4: ", r5);

            var r6 = products.First();
            Console.WriteLine("FIRST: " + r6);

            // Usa se FirstOrDefault() para evitar exceptions
            var r7 = products.Where(p => p.Price > 3000).FirstOrDefault();
            Console.WriteLine("FIRST OR DEFAULT: " + r6);

            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("SINGLE OR DEFAULT: " + r8);

            var r9 = products.Max(p => p.Price);
            Console.WriteLine("MAX PRICE: " + r9);

            var r10 = products.Min(p => p.Price);
            Console.WriteLine("MIN PRICE: " + r10);

            var r11 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("CATEGORY 1 SUM PRICES: " + r11);

            var r12 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("CATEGORY 1 AVERAGE PRICES: " + r12);

            var r13 = products.Where(p => p.Category.Id == 5)
                .Select(p => p.Price)
                .DefaultIfEmpty(0.0)
                .Average();
            Console.WriteLine("CATEGORY 5 AVERAGE PRICES: " + r13);

            var r14 = products
                .Where(p => p.Category.Id == 1)
                .Select(p => p.Price)
                .Aggregate((x, y) => x + y);
            Console.WriteLine("CATEGORY 1 AGGREGATE SUM: " + r14);

            var r15 = products
                .Where(p => p.Category.Id == 5)
                .Select(p => p.Price)
                .Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("CATEGORY 5 AGGREGATE SUM: " + r15);

            var r16 = products.GroupBy(p => p.Category);
            Console.WriteLine("\nGROUP BY CATEGORY: ");
            foreach (IGrouping<Category, Product> group in r16)
            {
                Console.WriteLine("\nCategory " + group.Key.Name + ":");
                foreach (Product p in group)
                    Console.WriteLine(p);
            }
            Console.Write("\n");

            Console.ReadKey();
        }

        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            if (collection.Count() == 0)
                throw new ArgumentException("The list is empty");

            foreach (T product in collection)
                Console.WriteLine(product);

            Console.Write("\n");
        }
    }
}
