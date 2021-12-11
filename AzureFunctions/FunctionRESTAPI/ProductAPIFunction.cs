using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace FunctionRESTAPI
{
    public static class ProductAPIFunction
    {
        [FunctionName("GetAllProducts")]
        public static IActionResult GetAllProducts([HttpTrigger(AuthorizationLevel.Anonymous,"get", Route = "product")]
            HttpRequest req)
        {
            List<Product> listProducts = new List<Product>();
            string strCon = Environment.GetEnvironmentVariable("ConnectionStrings:DbConnection");
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                var text = "Select * From Products";
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Product product = new Product();
                        product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                        product.Name = rdr["Name"].ToString();
                        product.Description = rdr["Description"].ToString();
                        product.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]);

                        listProducts.Add(product);
                    }
                }
            }
            return new OkObjectResult(listProducts);
        }
    }
}
