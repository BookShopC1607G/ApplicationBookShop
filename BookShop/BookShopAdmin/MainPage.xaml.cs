using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BookShopAdmin.ServiceReference1;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BookShopAdmin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<image> PdfPages = new ObservableCollection<image>();
        ServiceBookShopClient client = new ServiceBookShopClient();
        private StorageFile fileimages;
        private StorageFile filePDF;
        private byte[] imagesbytes;
        private byte[] PDFbytes;
        public MainPage()
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
            fileimages = await f.PickSingleFileAsync();
            if (fileimages != null)
            {
                imagesbytes = await fileimages.AsByteArray();
                showImage.Source = await fileimages.AsBitmapImage();
                showImage.MaxWidth = 200;
                showImage.MaxHeight = 200;
            }
        }

        private async void books_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".pdf");
            filePDF = await picker.PickSingleFileAsync();
            PDFbytes = await filePDF.AsByteArray();
            Load(PDFbytes);
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            bookShop bs = new bookShop() {nameBook=namebook.Text,priceBook=Int32.Parse(price.Text),authorBook=authorbook.Text,category=category.Text,descriptionBook=description.Text};
            await client.createBookAsync(bs,PDFbytes,filePDF.FileType,fileimages.FileType,imagesbytes);
            MessageDialog mes = new MessageDialog("Thêm thành công!");
            mes.ShowAsync();
            clear();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }
        private void clear()
        {
            namebook.Text = string.Empty;
            price.Text = string.Empty;
            authorbook.Text = string.Empty;
            category.Text = string.Empty;
            description.Text = string.Empty;
            showImage.Source = null;
            PdfPages.Clear();
        }
        async void Load(byte[] doc)
        {
            InMemoryRandomAccessStream a = new InMemoryRandomAccessStream();
            await a.WriteAsync(doc.AsBuffer());
            PdfDocument docpdf = await PdfDocument.LoadFromStreamAsync(a);
            PdfPages.Clear();

            for (uint i = 0; i < docpdf.PageCount; i++)
            {
                BitmapImage images = new BitmapImage();

                var page = docpdf.GetPage(i);

                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    await page.RenderToStreamAsync(stream);
                    await images.SetSourceAsync(stream);
                }

                PdfPages.Add(new image() { images = images });
            }
        }
    }
}
