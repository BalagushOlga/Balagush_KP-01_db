using System;
using Npgsql;
using System.Collections.Generic;
class Model
{
    public Model()
    {}
    #region order
    public void InsertOrder(NpgsqlConnection connection, int id, string customer,  int price)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"orders\" (id, customer, price) VALUES (@id, @customer, @price)", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("customer", customer);
                command.Parameters.AddWithValue("price", price);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void UpdateOrder(NpgsqlConnection connection, int id, string customer,  int price)
    {
        try
        {
            using (var command = new NpgsqlCommand("UPDATE \"orders\" SET customer = @customer, price = @price WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("customer", customer);
                command.Parameters.AddWithValue("price", price);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void DeleteOrder(NpgsqlConnection connection, int id)
    {
        try
        {
            using (var command = new NpgsqlCommand("DELETE FROM \"orders\" WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
            Int64 l;
            using (var command = new NpgsqlCommand("SELECT COUNT(*) FROM \"order_product_subscriptions\" WHERE order_id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                l = (Int64)command.ExecuteScalar();;
            }
            for(int i = 0; i < l; i++)
            {
                DeleteSubscription(connection, id, -1);
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void RandomOrder(NpgsqlConnection connection)
    {
        try
        {
            int id = NextOId(connection);
            using (var command = new NpgsqlCommand("INSERT INTO \"orders\" (id, customer, price) VALUES (@id, chr(trunc(65+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int), floor(random() * (3000-500+1) + 500)::int)", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
            Random random = new Random();
            InsertSubscription(connection, NextSubId(connection), id, random.Next(1, NextPId(connection)));
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public int NextOId(NpgsqlConnection connection)
    {
        Int32 max = 0;
        using (var command = new NpgsqlCommand("SELECT * FROM \"orders\"", connection))
        {
            var reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                Int32 num = (Int32) reader[0];
                if(num >= max)
                {
                    max = num;
                }
            }
            reader.Close();
        }
        return max + 1;
    }
    #endregion
    #region product
    public void InsertProduct(NpgsqlConnection connection, int id, string product_name,  int price, int category_id)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"products\" (id, product_name, price, category_id) VALUES (@id, @product_name, @price, @category_id)", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("product_name", product_name);
                command.Parameters.AddWithValue("price", price);
                command.Parameters.AddWithValue("category_id", category_id);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void UpdateProduct(NpgsqlConnection connection, int id, string product_name,  int price, int category_id)
    {
        try
        {
            using (var command = new NpgsqlCommand("UPDATE \"products\" SET product_name = @product_name, price = @price, category_id = @category_id WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("product_name", product_name);
                command.Parameters.AddWithValue("price", price);
                command.Parameters.AddWithValue("category_id", category_id);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void DeleteProduct(NpgsqlConnection connection, int id)
    {
        try
        {
            using (var command = new NpgsqlCommand("DELETE FROM \"products\" WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
            Int64 l;
            using (var command = new NpgsqlCommand("SELECT COUNT(*) FROM \"order_product_subscriptions\" WHERE product_id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                l = (Int64)command.ExecuteScalar();;
            }
            for(int i = 0; i < l; i++)
            {
                DeleteSubscription(connection, -1, id);
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void RandomProduct(NpgsqlConnection connection)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"products\" (id, product_name, price, category_id) VALUES (@id, chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int), floor(random() * (200-5+1) + 5)::int, @category_id)", connection))
            {
                Random random = new Random();
                command.Parameters.AddWithValue("id", NextPId(connection));
                int m_id = NextCId(connection);
                command.Parameters.AddWithValue("category_id", random.Next(1, m_id));
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public int NextPId(NpgsqlConnection connection)
    {
        Int32 max = 0;
        using (var command = new NpgsqlCommand("SELECT * FROM \"products\"", connection))
        {
            var reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                Int32 num = (Int32) reader[0];
                if(num >= max)
                {
                    max = num;
                }
            }
            reader.Close();
        }
        return max + 1;
    }
    #endregion
    #region category
    public void InsertCategory(NpgsqlConnection connection, int id, string category_name, int section_id)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"categories\" (id, category_name, section_id) VALUES (@id, @category_name, @section_id)", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("category_name", category_name);
                command.Parameters.AddWithValue("section_id", section_id);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void UpdateCategory(NpgsqlConnection connection, int id, string category_name, int section_id)
    {
        try
        {
            using (var command = new NpgsqlCommand("UPDATE \"categories\" SET category_name = @category_name, section_id = @section_id WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("category_name", category_name);
                command.Parameters.AddWithValue("section_id", section_id);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void DeleteCategory(NpgsqlConnection connection, int id)
    {
        try
        {
            using (var command = new NpgsqlCommand("DELETE FROM \"categories\" WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
            List<int> product_ids = new List<int>();
            using (var command = new NpgsqlCommand("SELECT * FROM \"products\" WHERE category_id = @id", connection))
            {
                command.Parameters.AddWithValue("id", 2);
                var reader = command.ExecuteReader();
                
                while(reader.Read())
                {
                    product_ids.Add((int) reader[0]);
                }
                reader.Close();
            }
            foreach (var i in product_ids)
            {
                DeleteProduct(connection, i);
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        //разом з категорією видаляються усі продукти категорії
    }
    public void RandomCategory(NpgsqlConnection connection)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"categories\" (id, category_name, section_id) VALUES (@id, chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int), @section_id)", connection))
            {
                Random random = new Random();
                command.Parameters.AddWithValue("id", NextCId(connection));
                int m_id = NextSId(connection);
                command.Parameters.AddWithValue("section_id", random.Next(1, m_id));
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public int NextCId(NpgsqlConnection connection)
    {
        Int32 max = 0;
        using (var command = new NpgsqlCommand("SELECT * FROM \"categories\"", connection))
        {
            var reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                Int32 num = (Int32) reader[0];
                if(num >= max)
                {
                    max = num;
                }
            }
            reader.Close();
        }
        return max + 1;
    }
    #endregion
    #region section
    public void InsertSection(NpgsqlConnection connection, int id, string section_name)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"sections\" (id, section_name) VALUES (@id, @section_name)", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("section_name", section_name);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void UpdateSection(NpgsqlConnection connection, int id, string section_name)
    {
        try
        {
            using (var command = new NpgsqlCommand("UPDATE \"sections\" SET section_name = @section_name WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("section_name", section_name);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void DeleteSection(NpgsqlConnection connection, int id)
    {
        try
        {
            using (var command = new NpgsqlCommand("DELETE FROM \"sections\" WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
            List<int> category_ids = new List<int>();
            using (var command = new NpgsqlCommand("SELECT * FROM \"categories\" WHERE section_id = @id", connection))
            {
                command.Parameters.AddWithValue("id", 2);
                var reader = command.ExecuteReader();
                
                while(reader.Read())
                {
                    category_ids.Add((int) reader[0]);
                }
                reader.Close();
            }
            foreach (var i in category_ids)
            {
                DeleteCategory(connection, i);
            }
            List<int> product_ids = new List<int>();
            using (var command = new NpgsqlCommand("SELECT * FROM \"products\" WHERE section_id = @id", connection))
            {
                command.Parameters.AddWithValue("id", 2);
                var reader = command.ExecuteReader();
                
                while(reader.Read())
                {
                    product_ids.Add((int) reader[0]);
                }
                reader.Close();
            }
            foreach (var i in product_ids)
            {
                DeleteProduct(connection, i);
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public void RandomSection(NpgsqlConnection connection)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"sections\" (id, section_name) VALUES (@id, chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int) || chr(trunc(97+random()*25)::int)|| chr(trunc(97+random()*25)::int))", connection))
            {
                command.Parameters.AddWithValue("id", NextSId(connection));
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public int NextSId(NpgsqlConnection connection)
    {
        Int32 max = 0;
        using (var command = new NpgsqlCommand("SELECT * FROM \"sections\"", connection))
        {
            var reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                Int32 num = (Int32) reader[0];
                if(num >= max)
                {
                    max = num;
                }
            }
            reader.Close();
        }
        return max + 1;
    }
    #endregion
    #region subscription order-products
    public void InsertSubscription(NpgsqlConnection connection, int id, int order_id, int product_id)
    {
        try
        {
            using (var command = new NpgsqlCommand("INSERT INTO \"order_product_subscriptions\" (id, order_id, product_id) VALUES (@id, @order_id, @product_id)", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("order_id", order_id);
                command.Parameters.AddWithValue("product_id", product_id);
                command.ExecuteNonQuery();
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    //Зв'язки видаляються при видалені замовлення або продукту з бд
    public void DeleteSubscription(NpgsqlConnection connection, int order_id, int product_id)
    {
        try
        {
            if(product_id == -1)
            {
                using (var command = new NpgsqlCommand("DELETE FROM \"order_product_subscriptions\" WHERE order_id = @order_id", connection))
                {
                    command.Parameters.AddWithValue("order_id", order_id);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (var command = new NpgsqlCommand("DELETE FROM \"order_product_subscriptions\" WHERE product_id = @product_id", connection))
                {
                    command.Parameters.AddWithValue("product_id", product_id);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    public int NextSubId(NpgsqlConnection connection)
    {
        Int32 max = 0;
        using (var command = new NpgsqlCommand("SELECT * FROM \"order_product_subscriptions\"", connection))
        {
            var reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                Int32 num = (Int32) reader[0];
                if(num >= max)
                {
                    max = num;
                }
            }
            reader.Close();
        }
        return max + 1;
    }
    #endregion
}