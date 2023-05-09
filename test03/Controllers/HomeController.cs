using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using test03.Models;

namespace test03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Person> persons = new List<Person>();
            using (MySqlConnection con = new MySqlConnection("server=localhost; user=root; database=test;port=3306;password=visspan01"))
            {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from person";
                cmd.Connection=con;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Person person = new Person();
                    person.Id = Convert.ToInt32(reader["id"]);
                    person.FirstName = reader["first_name"].ToString();
                    person.LastName = reader["last_name"].ToString();
                    person.Age = Convert.ToInt32(reader["age"]);

                    persons.Add(person);
                }

                con.Close();

            }
            return View(persons);
            
        }
        public IActionResult Class()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}