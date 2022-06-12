using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml.Media.Imaging;
using Contact = ContactsDemo.App.Models.Contact;

namespace ContactsDemo.App.Services
{
    public class ContactService : IContactService
    {
        public async IAsyncEnumerable<Contact> GetAllAsync()
        {
            var allAccessStore = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);

            var contacts = await allAccessStore.FindContactsAsync();

            foreach (var contact in contacts)
            {
                
                yield return new Contact
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    FullName = contact.FullName,
                    Email = contact.Emails.FirstOrDefault()?.Address ?? string.Empty,
                    SourceDisplayPicture = contact.SourceDisplayPicture
                    

                };
            }
        }
    }
}
