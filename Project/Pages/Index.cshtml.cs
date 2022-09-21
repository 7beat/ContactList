using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Project.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using Project.Data;
using Microsoft.EntityFrameworkCore;

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

        public IList<Contact> ContactList { get; set; } = default!;

        //Working, printed records, just need to change to display ContactList instead of Contacts!
        public async Task OnGetAsync()
        {
            if (_context.Contacts != null)
            {
                ContactList = await _context.Contacts.ToListAsync();
            }

            foreach (var item in ContactList)
            {
                Console.WriteLine(item.FirstName);
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
            //return redirectto
            //Refresh
        }



        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);

            if (contact is not null)
            {
                Contact = contact;
                _context.Contacts.Remove(Contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }

        //public async Task<IActionResult> OnPostEdit(int? id){}

        public IActionResult OnPostTest2()
        {
            Console.WriteLine("!!!!!!!!!!!!!POST IACTIONRESULT!!!!!!!!!!!!!!!!!");
            return RedirectToPage("Index");
            //Najpierw wywołał się handler potem przeniusł na Index!
            //Working! Custom OnPost which refreshes page!
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