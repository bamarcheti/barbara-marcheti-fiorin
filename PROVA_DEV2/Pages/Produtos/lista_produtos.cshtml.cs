using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PROVA_DEV2.Pages.Produtos
{
    public class lista_produtosModel : PageModel
    {
        public List<ProductsInfoNovo> listProduto = new List<ProductsInfoNovo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Produtos_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = "Select * from products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductsInfoNovo productsInfo = new ProductsInfoNovo();
                                productsInfo.Id = "" + reader.GetInt32(0);
                                productsInfo.name = reader.GetString(1);
                                productsInfo.price = reader.GetString(2);
                                productsInfo.validity = reader.GetDateTime(3).ToString();
                                productsInfo.create_at = reader.GetDateTime(4).ToString();

                                listProduto.Add(productsInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCE��O : " + ex.Message.ToString());
            }
        }
    }

    public class ProductsInfoNovo
    {
        public string Id;
        public string name;
        public string price;
        public string validity;
        public string create_at;
    }
}

