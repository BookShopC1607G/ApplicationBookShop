using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ServiceBookShop.Models
{
    public class userBookShopDownload
    {
        public userBookShop userbookShop { get; set; }
        public Stream pdfTobyte { get; set; }
        public Stream images { get; set; }
        public userBookShopDownload(userBookShop u)
        {
            pdfTobyte = convertToStream(u.bookShop.pathBook);
            images = convertToStream(u.bookShop.pathimages);
        }
        public userBookShopDownload()
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