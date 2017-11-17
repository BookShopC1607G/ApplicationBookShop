using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.ServiceReference1;
using Windows.UI.Xaml.Media;

namespace BookShop.Models
{
    class CuaHang
    {
        public string Catagory { get; set; }
        public List<bookShop> bookShops { get; set; }
        public List<BindingImage> image { get; set; }
    }
}
