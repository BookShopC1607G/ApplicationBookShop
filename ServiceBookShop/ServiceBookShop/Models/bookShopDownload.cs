using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ServiceBookShop.Models
{
    public class bookShopDownload
    {
        public bookShop userbookShop { get; set; }
        public string idBook { get; set; }
        public Stream images { get; set; }
        public bookShopDownload(bookShop u)
        {
            images = convertToStream(u.pathimages);
        }
        public bookShopDownload()
        {
            
        }
        public Stream convertToStream(string u)
        {
            var a = new MemoryStream();
            var b = File.ReadAllBytes(u);
            a.Write(b, 0, b.Length);
            a.Position = 0;
            return a;
        }
    }
}