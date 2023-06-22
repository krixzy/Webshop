using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Webshop
{
    public class produkt
    {
        int? id;
        string name;
        int amount;
        int order_id;
        public produkt(string name, int amount, int order_id, int? id = null)
        {
            this.id = id;
            this.name = name;
            this.amount = amount;
            this.order_id = order_id;

        }

            public void AddToSqlServer()
           {
               try
               {
                  string query = $"INSERT INTO product VALUES('{name}', '{amount}', '{order_id}')";

                  Connect.ExecuteSqlcommand(query);
               }
               catch (Exception ex)
               {

               }
            }
         public static List<produkt> GetProduct(int id)
         {
             List<produkt> products = new List<produkt>();
             
              
             string query = @"SELECT product.id, name, amount, order_id FROM product WHERE @id = product.order_id";
             SqlDataReader reader = Connect.GetDataReaderFromSql(query, id);
             if (reader.HasRows)
             { 
                 while (reader.Read())
                 {
                     int productId = reader.GetInt32(0);
                     string name = reader.GetString(1);
                     int amount = reader.GetInt32(2);
                     int orderId = reader.GetInt32(3);
                     produkt product = new produkt(name, amount, orderId, productId);
                     products.Add(product);
                 }
             }


             return products;
         }

        public override string ToString()
        {
            string returnString = $"id: {id} name: {name} amount: {amount} ";
            return returnString;
        }

    }
}
