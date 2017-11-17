using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BookShop.ServiceReference1;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BookShop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    
    public sealed partial class MainPage : Page
    {
        public static usersShop check { get; set; }
        ServiceBookShopClient client = new ServiceBookShopClient();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (Content.CanGoBack)Content.GoBack();
            if (Content.CanGoBack ==  false)back.Visibility = Visibility.Collapsed;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Content.CanGoBack == false) back.Visibility = Visibility.Collapsed;
            if (Content.CanGoBack) back.Visibility = Visibility.Visible;
        }

        private void CuaHang_Click(object sender, RoutedEventArgs e)
        {
            Content.Navigate(typeof(cuahang));
            search.Visibility = Visibility.Visible;
            Content.BackStack.Clear();
        }

        private void Tusach_Click(object sender, RoutedEventArgs e)
        {
            Content.Navigate(typeof(tusach));
            search.Visibility = Visibility.Visible;
            Content.BackStack.Clear();
        }

        private void TaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            if (check == null)
            {
                Content.Navigate(typeof(taikhoan));
                search.Visibility = Visibility.Collapsed;
                Content.BackStack.Clear();
            }else
            {
                Content.Navigate(typeof(UserLogin));
                search.Visibility = Visibility.Collapsed;
                Content.BackStack.Clear();
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
