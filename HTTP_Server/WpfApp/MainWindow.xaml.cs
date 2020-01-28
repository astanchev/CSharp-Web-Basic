using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await DownloadImageAsync(this.Image1, "https://upload.wikimedia.org/wikipedia/commons/3/3c/Salto_del_Angel-Canaima-Venezuela08.JPG");
            await DownloadImageAsync(this.Image2, "https://bigseventravel.com/wp-content/uploads/2019/11/balazs-busznyak-hzSxZM9IoQo-unsplash.jpg");
            await DownloadImageAsync(this.Image3, "https://d36tnp772eyphs.cloudfront.net/blogs/1/2019/10/seljalandsfoss-most-instagrammed-waterfalls-world-1200x855.jpg");
            await DownloadImageAsync(this.Image4, "https://blog.klm.com/assets/uploads/2016/06/Ban-Gioc-Detian-Falls.jpg");
            await DownloadImageAsync(this.Image5, "https://www.pandotrip.com/wp-content/uploads/2018/05/Godafoss-waterfall-from-above.jpg");
            await DownloadImageAsync(this.Image6, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRwhT7i7DfFin2rGBXiIHgV9CRi5QN2-cVe0R4KuQP-_7bRjAMd&s");
        }

        private async Task DownloadImageAsync(Image image, string url)
        {
            var client = new HttpClient();
            await Task.Run(() => Thread.Sleep(3000));
            var response = await client.GetAsync(url);
            var byteData = await response.Content.ReadAsByteArrayAsync();

            image.Source = this.LoadImage(byteData);
        }

        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
