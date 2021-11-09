using System;
using Npgsql;
using System.Collections.Generic;
class Controller
{
    public Model model;
    NpgsqlConnection connection;
    public Controller()
    {
        string connString = "Host=localhost;Port=5433;Database=lab1shop;Username=postgres;Password=1a2s3d4f";
        connection = new NpgsqlConnection(connString);
        model = new Model();
    }
    public void CommandProcessing(string answer)
    {
        connection.Open();
        string[] sp = answer.Split(".");
        string[] sp1 = answer.Split(" ");
        if(sp.Length == 2 && sp[0] == "1")
        {
            if(sp[1] == "1")
            {
                Console.WriteLine("Enter:{id} {customer} {price}");
                string[] a = Console.ReadLine().Split(" ");
                model.InsertOrder(connection, int.Parse(a[0]), a[1], int.Parse(a[2]));
            }
            else if(sp[1] == "2")
            {
                Console.WriteLine("Enter:{id} {product_name} {price} {category_id}");
                string[] a = Console.ReadLine().Split(" ");
                model.InsertProduct(connection, int.Parse(a[0]), a[1], int.Parse(a[2]), int.Parse(a[3]));
            }
            else if(sp[1] == "3")
            {
                Console.WriteLine("Enter:{id} {category_name} {section_id}");
                string[] a = Console.ReadLine().Split(" ");
                model.InsertCategory(connection, int.Parse(a[0]), a[1], int.Parse(a[2]));
            }
            else if(sp[1] == "4")
            {
                Console.WriteLine("Enter:{id} {section_name}");
                string[] a = Console.ReadLine().Split(" ");
                model.InsertSection(connection, int.Parse(a[0]), a[1]);
            }
        }
        else if(sp.Length == 2 && sp[0] == "2")
        {
            Console.Write("Enter id: ");
            int a = int.Parse(Console.ReadLine());
            if(sp[1] == "1")
            {
                model.DeleteOrder(connection, a);
            }
            else if(sp[1] == "2")
            {
                model.DeleteProduct(connection, a);
            }
            else if(sp[1] == "3")
            {
                model.DeleteCategory(connection, a);
            }
            else if(sp[1] == "4")
            {
                model.DeleteSection(connection, a);
            }
        }
        else if(sp1.Length == 2 && sp1[0] == "random")
        {
            Randomizer(int.Parse(sp1[1]));
        }
        else if(sp1.Length == 2 && sp1[0] == "order")
        {
            SearchShoppingList(int.Parse(sp1[1]));
        }
        else if(sp1[0] =="cat")
        {
            string section_name = "";
            for(int i = 1; i < sp1.Length; i++)
            {
                section_name += sp1[i];
            }
            ListOfSectionCategories(section_name);
        }
        else if(sp1[0] =="prod")
        {
            string category_name = "";
            for(int i = 1; i < sp1.Length; i++)
            {
                category_name += sp1[i];
            }
            ListOfCategoryProducts(category_name);
        }
        connection.Close();
    }
    public void Randomizer(int numberOfNewValues)
    {
        for(int i = 1; i < numberOfNewValues; i++)
        {
            model.RandomSection(connection);
            model.RandomCategory(connection);
            model.RandomProduct(connection);
            model.RandomOrder(connection);
        }
    }
    public void SearchShoppingList(int id_order)
    {
        List<string[]> list = new List<string[]>();
        try
        {
            string nameOfcustomer = "";
            using (var command = new NpgsqlCommand("SELECT orders.customer, products.product_name, products.price FROM order_product_subscriptions, orders, products WHERE order_product_subscriptions.order_id = @id AND products.id = order_product_subscriptions.product_id AND orders.id = order_product_subscriptions.order_id", connection))
            {
                command.Parameters.AddWithValue("id", id_order);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    string[] sh = new string[2];
                    nameOfcustomer = (string)reader[0];
                    sh[0] = (string)reader[1];
                    sh[1] = Convert.ToString(reader[2]);
                    list.Add(sh);
                }
                reader.Close();
            }

            Console.WriteLine($"\n{nameOfcustomer}'s shopping list:");
            int sum = 0;
            foreach (var item in list)
            {
                Console.WriteLine($"{item[0]} - {item[1]}$");
                sum += Convert.ToInt32(item[1]);
            }
            Console.WriteLine($"Sum: {sum}$");
            model.UpdateOrder(connection, id_order, nameOfcustomer, sum);
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void ListOfSectionCategories(string section_name)
    {
        List<string[]> list = new List<string[]>();
        try
        {
            using (var command = new NpgsqlCommand("SELECT sections.section_name, categories.category_name FROM sections, categories WHERE sections.section_name LIKE @section_name AND categories.section_id = sections.id", connection))
            {
                command.Parameters.AddWithValue("section_name", section_name + "%");
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    string[] str = new string[2];
                    str[0] = (string)reader[0];
                    str[1] = (string)reader[1];
                    list.Add(str);
                }
                reader.Close();
            }

            string nameOfsection = "";
            foreach (var item in list)
            {
                if(nameOfsection != item[0])
                {
                    nameOfsection = item[0];
                    Console.WriteLine($"\nSection '{nameOfsection}' categories:");
                }
                Console.WriteLine($"\t{item[1]}");
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void ListOfCategoryProducts(string category_name)
    {
        try
        {
            List<string[]> list = new List<string[]>();
            using (var command = new NpgsqlCommand("SELECT categories.category_name, products.product_name, products.price FROM categories, products WHERE categories.category_name LIKE @category_name AND products.category_id = categories.id", connection))
            {
                command.Parameters.AddWithValue("category_name", category_name + "%");
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    string[] sh = new string[3];
                    sh[0] = (string)reader[0];
                    sh[1] = (string)reader[1];
                    sh[2] = Convert.ToString(reader[2]);
                    list.Add(sh);
                }
                reader.Close();
            }

            string nameOfCategory = "";
            foreach (var item in list)
            {
                string name = (string)item[0];
                if(nameOfCategory != name)
                {
                    nameOfCategory = name;
                    Console.WriteLine($"\nCategory '{nameOfCategory}' products list:");
                }
                Console.WriteLine($"\t{item[1]} - {item[2]}$");
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}