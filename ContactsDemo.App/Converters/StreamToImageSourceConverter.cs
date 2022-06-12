using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ContactsDemo.App.Converters
{
    public class StreamToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return new BitmapImage();

            var stream = value as IRandomAccessStreamReference;

            if (stream == null)
                return new BitmapImage();

            var thumbnailImage = new BitmapImage();

            using var thumbnailStream = stream.OpenReadAsync().GetAwaiter().GetResult();

            thumbnailImage.SetSource(thumbnailStream);

            return thumbnailImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
