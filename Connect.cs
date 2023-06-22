using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

public static class Connect
{
   //public static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-I5TRKAB\\SQLEXPRESS;Initial Catalog=Webshop;Integrated Security=True");
    public static SqlDataReader makeConnection (string query, int id)
    {
    
            string connectionString = GetConnectionString();

            SqlConnection connection = new SqlConnection();


            SqlCommand command = new SqlCommand(query, connection);
            connection.ConnectionString = connectionString;
            command.Parameters.AddWithValue("@id", id);        
            connection.Open();
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            
        
            return dr;

            
     }

    public static void executeConnection(string query, SqlCommand inCommand = null)
    {

        string connectionString = GetConnectionString();

        SqlConnection connection = new SqlConnection();

        if (inCommand is null)
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.ConnectionString = connectionString;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        if (inCommand is not null) 
        {          
            connection.ConnectionString = connectionString;
            connection.Open();
            inCommand.ExecuteNonQuery();
            connection.Close();
        }

    }


    static public string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Data Source=DESKTOP-I5TRKAB\\SQLEXPRESS;Initial Catalog=Webshop;Integrated Security=True";
        }
    
}
