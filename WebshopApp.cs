using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    public class WebshopApp
    {
        SqlConnection sqlCon = new SqlConnection("Data Source=DESKTOP-I5TRKAB\\SQLEXPRESS;Initial Catalog=Webshop;Integrated Security=True");

        public void viewCustomer(int id)
        {

          
            string query = @"SELECT name, address, phone, Email FROM customer WHERE id = @id";
            SqlDataReader dr = Connect.GetDataReaderFromSql(query,id);
            if(dr.HasRows)
            {
                while (dr.Read())
                {
                    string name  = dr.GetString(0);
                    string address = dr.GetString(1);
                    int phone = dr.GetInt32(2);
                    string email = dr.GetString(3);
                 //   Customer customer = new Customer(name, address, phone, email);
                 //   Console.WriteLine(customer.name);
                }
            }
       

        }
        public void addCustomer(Customer customer) 
        {
            try
            {

                string query = $"INSERT INTO Customer VALUES('{customer.name}', '{customer.address}', '{customer.phone}', '{customer.email}')";

                Connect.ExecuteSqlcommand(query);

            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.ToString());
            }
        }
        public void RemoveCustomer(int id) 
        {   
            
            string query = $"DELETE FROM customer WHERE id = '{id}'";

            Connect.ExecuteSqlcommand(query);
        }

        public Customer GetCustomer(int id)
        {
           Customer customer = Customer.GetCustomer(id);
            Console.WriteLine(customer.orders[1].produkts[0].amount);
            return customer;
        }
    }
}
