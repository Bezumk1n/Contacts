using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
