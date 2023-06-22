using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Webshop
{
    public class Customer
    {
        public int? id;
        public string name;
        public string address;
        public int phone;
        public string email;
        public List<Order>? orders;



        public Customer(string name, string address, int phone, string email, List<Order>? orders = null, int id = 0)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.orders = orders;

        }

          public void AddCustomerToSql()
          {

              try
              {
                      string query = $"INSERT INTO Customer VALUES('{name}', '{address}', '{phone}', '{email}')";

                      Connect.executeConnection(query);


            }
            catch (Exception ex)
              {
                     Console.WriteLine(ex.Message);   
              }
          }

          public void ChangeName(string newName)
          {

              try
              {
                      string connectionString = Connect.GetConnectionString();
                      SqlConnection conn = new SqlConnection(connectionString);
                      conn.Open();
                      string updateQuery = @"UPDATE Customer SET name = @newName WHERE email = @email";
                      SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                      updateCmd.Parameters.AddWithValue("@newName", newName);
                      updateCmd.Parameters.AddWithValue("@email", email);
                      Connect.executeConnection(updateQuery, updateCmd);
                      this.name = newName;
                      conn.Close();
              }
               catch (Exception ex)
              {
                Console.WriteLine(ex.Message);
              }
          }
        public static Customer ViewCustomer(int id)
        {


            //this.openConnection();
            string query = @"SELECT id, name, address, phone, email FROM customer WHERE id = @id";
            SqlDataReader dr = Connect.makeConnection(query, id);
            dr.Read();
            int customerid = dr.GetInt32(0);
            string name = dr.GetString(1);
            string address = dr.GetString(2);
            int phone = dr.GetInt32(3);
            string email = dr.GetString(4);
            return new Customer(name, address, phone, email, Order.ViewOrderList(id), customerid);

        }
        public override String ToString()
        {
            string returnString = $"Customer name:{name} address:{address} email:{email}" + System.Environment.NewLine;
            foreach (Order order in orders) { returnString += order + System.Environment.NewLine; };
            return returnString;
        }
    }
}
