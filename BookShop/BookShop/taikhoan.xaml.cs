using System;
using System.IO;
using BookShop.ServiceReference1;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class taikhoan : Page
    {
        ServiceBookShopClient client = new ServiceBookShopClient();
        public taikhoan()
        {
            this.InitializeComponent();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            MainPage.check= await client.loginBookAsync(user.Text, pass.Password);
            if (MainPage.check != null)
            {
                this.Frame.Navigate(typeof(UserLogin));
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserRegister));
        }
    }
}
