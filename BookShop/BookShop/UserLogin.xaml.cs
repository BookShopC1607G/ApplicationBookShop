using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BookShop.ServiceReference1;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserLogin : Page
    {
        ObservableCollection<ImageSource> i = new ObservableCollection<ImageSource>();
        ServiceBookShopClient client = new ServiceBookShopClient();
        public UserLogin()
        {
            this.InitializeComponent();
            Init();
        }
        public async void Init() {
            var a = await client.getFileAsync(MainPage.check.pathimages);
            BitmapImage imagefile = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(a.AsBuffer());
                stream.Seek(0);
                await imagefile.SetSourceAsync(stream);
                i.Add(imagefile);
                var b = new ImageBrush();
                b.ImageSource = imagefile;
                image.Fill = b;
            }
            MainPage.check = await client.loginBookAsync(MainPage.check.emailAddress, MainPage.check.passWordName);
            name.Text = MainPage.check.userName;
            banks.Text = String.Format("{0:0} VNĐ", MainPage.check.bankUser) ;
            phone.Text = MainPage.check.phoneNumber;
        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {
            MainPage.check = null;
            this.Frame.Navigate(typeof(taikhoan));
        }
    }
}
