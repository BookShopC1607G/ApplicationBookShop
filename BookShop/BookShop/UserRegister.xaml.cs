using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BookShop.ServiceReference1;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserRegister : Page
    {
        ServiceBookShopClient client = new ServiceBookShopClient();
        private byte[] i;
        private StorageFile file;
        public UserRegister()
        {
            this.InitializeComponent();
        }

        private async void images_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker f = new FileOpenPicker();
            f.SuggestedStartLocation = PickerLocationId.Desktop;
            f.FileTypeFilter.Add(".jpg");
            f.FileTypeFilter.Add(".png");
            f.FileTypeFilter.Add(".jpeg");
            file = await f.PickSingleFileAsync();
            if (file != null)
            {
                i =await file.AsByteArray();
                viewImage.Source = await file.AsBitmapImage();
                viewImage.MaxWidth = 200;
                viewImage.MaxHeight = 200;
            }
            
        }

        private async void register_Click(object sender, RoutedEventArgs e)
        {
            if (await client.checkEmailAsync(email.Text))
            {
                var usershop = new usersShop() { userName = user.Text, passWordName = pass.Password, bankUser = Int32.Parse(bank.Text), emailAddress = email.Text, phoneNumber = phone.Text };
                await client.createLoginAsync(usershop, file.FileType, i);
                MessageDialog mess = new MessageDialog("Đăng ký thành công!");
                mess.ShowAsync();
                this.Frame.Navigate(typeof(taikhoan));
            }
            else
            {
                MessageDialog mess = new MessageDialog("Email bị trùng! Vui lòng chọn email khác");
            }
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
