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
            UpdateDetached();
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
    }
}