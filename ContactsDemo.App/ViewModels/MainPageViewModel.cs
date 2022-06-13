using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactsDemo.App.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        [ObservableProperty]
        private bool _isFiltered;

        public IEnumerable<string> SortingMethods { get; set; } = new List<string>
        {
            "First Name",
            "Last Name"
        };

        [ObservableProperty]
        private string _selectedSortingMethod;

        private ObservableCollection<Contact> _allContacts;

        // C'tor
        //
        public MainPageViewModel()
        {
            _contactService = new ContactService();
            _allContacts = new ObservableCollection<Contact>();

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
            if (!Contacts.Any())
                return;

            Sort(value);
        }

        private void Sort(string value)
        {
            if (value.Equals("First Name"))
                Contacts = new ObservableCollection<Contact>(Contacts.OrderBy(c => c.FirstName));
            if (value.Equals("Last Name"))
                Contacts = new ObservableCollection<Contact>(Contacts.OrderBy(c => c.LastName));
        }

        partial void OnIsFilteredChanged(bool value)
        {
            if (value)
            {
                _allContacts = Contacts;

                Contacts = new ObservableCollection<Contact>(Contacts.Where(c =>
                    !string.IsNullOrEmpty(c.FirstName) && !string.IsNullOrEmpty(c.LastName)));
            }
            else
            {
                Contacts = _allContacts;
            }

            Sort(_selectedSortingMethod);
        }
    }
}
