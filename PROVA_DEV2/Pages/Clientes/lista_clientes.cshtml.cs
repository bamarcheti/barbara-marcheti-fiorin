using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace PROVA_DEV2.Pages.Clientes
{
    public class lista_clientesModel : PageModel
    {
        public List<ClientInfoNovo> listClients = new List<ClientInfoNovo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=prova_dev2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = "Select * from clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfoNovo clientInfo = new ClientInfoNovo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.adress = reader.GetString(4);
                                clientInfo.create_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
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

    public class ClientInfoNovo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string adress;
        public string create_at;
    }
}