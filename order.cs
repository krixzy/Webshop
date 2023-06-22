using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    public class Order
    {
        int id;
        string weight;
        string date;
        string delivery_address;
        int customer_id;
        public List<produkt>? produkts;

        public Order(string weight, string date, string delivery_address, int customer_id, List<produkt>? produkts = null, int id = 0) 
        {
            this.id = id;
            this.weight = weight; 
            this.date = date;
            this.delivery_address = delivery_address;
            this.customer_id = customer_id;
            this.produkts = produkts;


        }

        public void AddToSqlServer()
        {
            try
            {
                string query = $"INSERT INTO orders VALUES('{weight}', '{date}', '{delivery_address}', '{customer_id}') ";

                Connect.ExecuteSqlcommand(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /*public void SetMailToKundeMail()
        {
            try
            {
            
               
                con.Open();
                string query = @"UPDATE orders SET orders.delivery_address = customer.address FROM orders JOIN customer on customer.id = @customer_id ";
                SqlCommand cmd = new SqlCommand(query, con);
              //  cmd.Parameters.AddWithValue("@customer_id", customer_id);
                cmd.ExecuteNonQuery ();
                con.Close ();



            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.ToString());
            }
        }*/
        public static List<Order> GetOrderList(int id)
        {
            List<Order> orders = new List<Order>();
    
            string query = @"SELECT  * from orders where customer_id=@id";
            SqlDataReader reader = Connect.GetDataReaderFromSql(query, id);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int orderid = reader.GetInt32(0);
                    string weight = reader.GetString(1);
                    string date = reader.GetString(2);
                    string deliveryAddress = reader.GetString(3);
                    int customer_id = reader.GetInt32(4);
                    Order order = new Order(weight, date, deliveryAddress, customer_id,produkt.GetProduct(orderid), orderid);
                    orders.Add(order);
                }
            }
            

            return orders;
        }

        public override string ToString()
        {
            string returnString = $"id: {id} weight: {weight} date: {date} delivery address {delivery_address} produkts:" + System.Environment.NewLine;
            foreach(var pro in produkts) { returnString += pro + System.Environment.NewLine; };
            return returnString;
        }

    }
        
}
