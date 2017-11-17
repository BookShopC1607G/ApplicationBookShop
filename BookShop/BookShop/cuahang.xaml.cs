using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using BookShop.Models;
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
    public sealed partial class cuahang : Page
    {
        ServiceBookShopClient client = new ServiceBookShopClient();
        ObservableCollection<CuaHang> bs = new ObservableCollection<CuaHang>();
        
        public cuahang()
        {
            this.InitializeComponent();
            
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            bs.Clear();
            var b = await client.getAllCatalogyAsync();
            var g = b.Distinct().OrderBy(n=>n);
            foreach (var i in g)
            {
                var a = await client.getAllbookAsync();
                var filterCatagory = new List<bookShop>();
                filterCatagory = a.Where(o => o.category.Equals(i)).ToList();
                List<BindingImage> ibs = new List<BindingImage>();
                foreach (var y in filterCatagory)
                {
                    ibs.Add(await addBindingImage(y));
                }
                var d = new CuaHang() {Catagory=i,bookShops= filterCatagory,image= ibs };
                bs.Add(d);
              
            }
            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = (ListBox)sender;
            var select =(BindingImage)result.SelectedItem;
            var a = 1;
        }
        private async Task<BindingImage> addBindingImage(bookShop b)
        {
            var c = await client.getFileAsync(b.pathimages);
            BitmapImage imagefile = new BitmapImage();
            BindingImage bst = new BindingImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(c.AsBuffer());
                stream.Seek(0);
                await imagefile.SetSourceAsync(stream);
                bst.image = imagefile;
            }
            bst.idBook = b.idBook;
            return bst;
        }
    }
}
