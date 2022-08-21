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

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            //Logging service
            _context = context;
            //DbContext
        }

        [BindProperty]
        public Contact Contact { get; set; }
        //Template used to create objects from DB

        public List<Person> People = new();
        public List<Contact> Contacts = new();

        public void OnGet()
        {
            try
            {
                Contacts = _context.Contacts.ToList();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message);
            }
        }

        public IActionResult OnPost()
        {
            //LOG: Contact's BirthDay: 01.01.0001, after post
            //ToDo: Fix Birth date!

            if (Contact.Id == 0) //If 0 means it was sent from creation form else it was specified to delete
            {
                AddContactDB(Contact);
            }
            else
            {
                DelContactDB(Contact.Id);
            }

            return RedirectToPage("Index");
            //Refresh
        }

        private void AddContactDB(Contact newContact)
        {
            //ToDo: Fix Date!
            try
            {
                _context.Contacts.Add(newContact);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.InnerException.Message);
            }
        }

        private void DelContactDB(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(x => x.Id == id);

            try
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}