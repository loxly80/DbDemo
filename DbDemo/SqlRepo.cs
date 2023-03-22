using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDemo
{
  internal class SqlRepo
  {
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Demo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public List<Item> LoadData()
    {
      List<Item> data = new List<Item>();
      using (SqlConnection sqlConnection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = sqlConnection.CreateCommand())
        {
          command.CommandText = "select IdDemo, Col1, Col2, Col3 from Demo";
          sqlConnection.Open();
          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              //načtení hodnot do pole řetězců
              Item item = new Item(Convert.ToInt32(reader[0]), reader[1].ToString() ?? "", reader[2].ToString() ?? "", reader[3].ToString() ?? "");
              data.Add(item);
            }
          }
          sqlConnection.Close();
        }
      }
      return data;
    }

    public List<Item> SearchData(string search)
    {
      List<Item> data = new List<Item>();
      using (SqlConnection sqlConnection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = sqlConnection.CreateCommand())
        {
          command.CommandText = $"select IdDemo, Col1, Col2, Col3 from Demo where Col1 like @search";
          command.Parameters.AddWithValue("search", "%" + search + "%");
          sqlConnection.Open();
          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              //načtení hodnot do pole řetězců
              Item item = new Item(Convert.ToInt32(reader[0]), reader[1].ToString() ?? "", reader[2].ToString() ?? "", reader[3].ToString() ?? "");
              data.Add(item);
            }
          }
          sqlConnection.Close();
        }
      }
      return data;
    }

    public void DeleteItem(string id)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = $"delete from Demo where IdDemo={id}";
          command.ExecuteNonQuery();
        }
        connection.Close();
      }
    }

    public void InsertItem(Item item)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = $"insert into Demo(Col1,Col2,Col3) values(@Col1,@Col2,@Col3)";
          command.Parameters.AddWithValue("Col1", item.Col1);
          command.Parameters.AddWithValue("Col2", item.Col2);
          command.Parameters.AddWithValue("Col3", item.Col3);
          command.ExecuteNonQuery();
        }
        connection.Close();
      }
    }

    public void UpdateItem(Item item)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = $"update Demo set Col1=@Col1,Col2=@Col2,Col3=@Col3 where IdDemo={item.Id}";
          command.Parameters.AddWithValue("Col1", item.Col1);
          command.Parameters.AddWithValue("Col2", item.Col2);
          command.Parameters.AddWithValue("Col3", item.Col3);
          command.ExecuteNonQuery();
        }
        connection.Close();
      }
    }
  }
}
