using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = ContactsDemo.App.Models.Contact;

namespace ContactsDemo.App.Services
{
    public interface IContactService
    {
        IAsyncEnumerable<Contact> GetAllAsync();
    }
}
