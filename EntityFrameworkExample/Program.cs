using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Linq;

namespace EntityFrameworkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Query();
            //Insert();
            //Update();
            //UpdateDetached();
            //Delete();

            QueryProducts("k", "a");
        }

        private static void QueryProducts(string name, string name2)
        {
            using (var db = new MyContext())
            {
                var data = db.Products
                    .Include(x => x.Category)
                    .Select(x => new
                    {
                        productName = x.Name,
                        categoryName = x.Category.Name
                    });

                data = data.OrderBy(x => x.productName);

                data = data.Where(x => x.productName.Contains(name));

                data = data.Where(x => x.productName.Contains(name2));

                foreach (var item in data.Take(5))
                {
                    Console.WriteLine($"Product name: {item.productName}, Category: {item.categoryName}");
                }
            }
        }

        private static void Query()
        {
            using (var db = new MyContext())
            {
                var data = db.Categories.ToList();

                foreach (var cat in data)
                {
                    Console.WriteLine(cat.Name);
                }
            }
        }

        private static void Insert()
        {
            using (var db = new MyContext())
            {
                db.Categories.Add(new Category { Id = 10, Name = "From EF" });
                db.SaveChanges();
            }
        }

        private static void Update()
        {
            using (var db = new MyContext())
            {
                var cat = db.Categories.Find(10);
                cat.Name = "Peter";
                db.SaveChanges();
            }
        }

        private static void UpdateDetached()
        {
            using (var db = new MyContext())
            {
                var cat = new Category { Id = 10, Name = "Testing", Description = "Detached object" };
                db.Categories.Update(cat);
                db.SaveChanges();
            }
        }

        private static void Delete()
        {
            using (var db = new MyContext())
            {
                var cat = db.Categories.Find(10);
                db.Categories.Remove(cat);
                db.SaveChanges();
            }
        }
    }
}