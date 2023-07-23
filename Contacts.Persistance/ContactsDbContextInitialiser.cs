using Microsoft.EntityFrameworkCore;

namespace Contacts.Persistance
{
    public class ContactsDbContextInitialiser
    {
        private readonly ContactsDbContext _context;
        public ContactsDbContextInitialiser(ContactsDbContext context)
        {
            _context = context;
        }
        public void Initialise()
        {
            _context.Database.Migrate();
        }
    }
}
