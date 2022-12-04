using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace PROVA_DEV2.Pages.Filmes
{
    public class lista_filmesModel : PageModel
    {
        public List<FilmsInfoNovo> listFilms = new List<FilmsInfoNovo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Films_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = "Select * from films";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FilmsInfoNovo filmInfo = new FilmsInfoNovo();
                                filmInfo.Id = "" + reader.GetInt32(0);
                                filmInfo.name = reader.GetString(1);
                                filmInfo.genre = reader.GetString(2);
                                filmInfo.create_at = reader.GetDateTime(3).ToString();

                                listFilms.Add(filmInfo);
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

    public class FilmsInfoNovo
    {
        public string Id;
        public string name;
        public string genre;
        public string create_at;
    }
}
