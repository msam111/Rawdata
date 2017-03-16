using MySql.Data.MySqlClient;
using System;

namespace AdoExample
{
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name ;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Query();

            Insert();

        }

        private static void Query()
        {
            using (var conn = new MySqlConnection("server=localhost;database=northwind;uid=bulskov;pwd=henrik"))
            {
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from category";

                var reader = cmd.ExecuteReader();

                while (reader.HasRows && reader.Read())
                {
                    var category = new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2)
                    };


                    Console.WriteLine($"Category: {category}");
                }
            }
        }

        private static void Insert()
        {
            var conn = new MySqlConnection("server=localhost;database=northwind;uid=bulskov;pwd=henrik");
            conn.Open();
            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into category values (9, 'Testing', null)";

            var numRows = cmd.ExecuteNonQuery();

            Console.WriteLine($"Number of rows: {numRows}");
            
        }
    }
}