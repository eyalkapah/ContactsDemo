using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactsDemo.App.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Contact = ContactsDemo.App.Models.Contact;


namespace ContactsDemo.App.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IContactService _contactService;

        [ObservableProperty] 
        private ObservableCollection<Contact> _contacts;

        [ObservableProperty]
        private bool _contactsLoaded;

        [ObservableProperty]
        private Contact _selectedContact;

        public IEnumerable<string> SortingMethods { get; set; } = new List<string>
        {
            "First Name",
            "Last Name"
        };

        [ObservableProperty]
        private string _selectedSortingMethod;

        // C'tor
        //
        public MainPageViewModel()
        {
            _contactService = new ContactService();

            Contacts = new ObservableCollection<Contact>();

            SelectedSortingMethod = SortingMethods.First();
        }

        [RelayCommand]
        private async void LoadContacts()
        {
            var contacts = _contactService.GetAllAsync();

            await foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }

            ContactsLoaded = true;


        }

        [RelayCommand]
        private void DeleteContact()
        {
            if (_selectedContact == null) 
                return;
            
            Contacts.Remove(_selectedContact);
        }

        partial void OnSelectedSortingMethodChanged(string value)
        {
            if (Contacts.Any())
            {
                if (value.Equals("First Name"))
                    Contacts = new ObservableCollection<Contact>(Contacts.OrderBy(c => c.FirstName).ToList());
                if (value.Equals("Last Name"))
                    Contacts = new ObservableCollection<Contact>(Contacts.OrderBy(c => c.LastName));
            }
        }

    }
}
