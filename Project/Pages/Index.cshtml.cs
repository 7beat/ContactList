using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Project.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using Project.Data;

namespace Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        //Będzie działać podobnie!

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            //Logging service
            _context = context;
            //DbContext
        }

        [BindProperty]
        public Person NewPerson { get; set; }

        [BindProperty]//NewContact?
        public Contact Contact { get; set; }
        //Person template used to create objects

        public List<Person> People = new();
        public List<Contact> Contacts = new();

        public string data;

        public void OnGet()
        {
            Contacts = _context.Contacts.ToList();

            //foreach (var item in Contacts)
            //{
            //    Console.WriteLine($"Contacts: {item.FirstName}");
            //}
            //Refresh Data EveryTime
        }


        public IActionResult OnPost()
        {
            Console.WriteLine(Contact.FirstName); 
            Console.WriteLine(Contact.LastName);
            Console.WriteLine(Contact.Email);
            Console.WriteLine($"Contact's BirthDay: {Contact.BirthDay}");
            //LOG: Contact's BirthDay: 01.01.0001, po wybraniu daty z formy
            //Contact został uzupełniony z formy!

            //Contact's id is specified from form!
            if (NewPerson.Id == 0) //If 0 means it was sent from creation form else it was specified to delete
            {
                //AddContactDB(Contact);
                //AddNewPersonDB(new() { FirstName = NewPerson.FirstName, LastName = NewPerson.LastName, Email = NewPerson.Email, Phone = NewPerson.Phone, BirthDay = NewPerson.BirthDay, Category = NewPerson.Category });

            }
            else
            {
                //DeletePersonDB(NewPerson.Id);
                //DeleteContactDB();

            }

            return RedirectToPage("Index");
            //Refresh
        }

        private void AddContactDB(Contact newContact)
        {
            //try? date format. ToDo: Fix date!
            //_context.Add(contact);
            _context.Contacts.Add(newContact);
            _context.SaveChanges();
            //Musi być w try bo jeśli wartośc unique się powtarza to exception!
            //SaveChanges() to insert it!
        }

        private void AddNewPersonDB(Person person) 
        {
            string conString = "SERVER=localhost;DATABASE=project;UID=root;";

            var query = $@"INSERT INTO `users` (`id`, `email`, `password`, `name`, `surname`, `phone`, `birthday`, `category`) VALUES (NULL, '{person.Email}', 'user', '{person.FirstName}', '{person.LastName}', '{person.Phone}', '{person.BirthDay.ToString("O")}', '{person.Category}')";

            MySqlConnection conn = new MySqlConnection(conString);

            //Musi dostać obiekt typu contact
            //_context.Add<Contact>();

            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, "Couldnt connect to DB, Error Message: " + e.Message);
                return;
            }

            var cmd = new MySqlCommand(query, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, "Couldnt not execute query, Error Message: " + e.Message);
            }
        }

        private void ConnectDB()
        {

            string conString = "SERVER=localhost;DATABASE=project;UID=root;";

            MySqlConnection conn = new MySqlConnection(conString);

            try
            {
                _logger.Log(LogLevel.Information, "Connecting to DataBase");
                conn.Open();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, "Couldnt connect to DB, Error Message: " + e.Message);
                return;
            }

            string query = "SELECT * FROM users";

            using (var cmd = new MySqlCommand(query, conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var category = rdr["category"].ToString();
                        Console.WriteLine(category);
                        People.Add(new Person()
                        {
                            Id = Convert.ToInt32(rdr["id"]),
                            FirstName = rdr["name"].ToString(),
                            LastName = rdr["surname"].ToString(),
                            BirthDay = Convert.ToDateTime(rdr["birthday"]),
                            Email = rdr["email"].ToString(),
                            Phone = rdr["phone"].ToString()
                        });
                    }
                }
            }
        }

        private void DeletePersonDB(int id)
        {
            string conString = "SERVER=localhost;DATABASE=project;UID=root;";

            string query = $"DELETE FROM users WHERE `users`.`id` = {id}";

            MySqlConnection conn = new MySqlConnection(conString);

            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, "Couldnt connect to DB, Error Message: " + e.Message);
                return;
            }

            var cmd = new MySqlCommand(query, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, "Couldnt execute SQL Query, Error Message: " + e.Message);
                return;
            }
        }
    }
}