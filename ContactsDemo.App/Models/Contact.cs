using Windows.Storage.Streams;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ContactsDemo.App.Models
{
    public partial class Contact : ObservableObject
    {
        [ObservableProperty]
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        private string _email;

        [ObservableProperty] 
        private IRandomAccessStreamReference _sourceDisplayPicture;
    }
}
