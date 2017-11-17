using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceBookShop.Models;

namespace ServiceBookShop
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceBookShop" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceBookShop.svc or ServiceBookShop.svc.cs at the Solution Explorer and start debugging.
    public class ServiceBookShop : IServiceBookShop
    {
        DataBookShopDataContext dt = new DataBookShopDataContext();
        string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

        public bool buyBook(int idBook, string user)
        {
            var filterUBS = dt.userBookShops.Where(a => a.idBook == idBook).ToList();
            if (filterUBS.Count > 0)
            {
                return false;
            }
            else
            {
                var filterBook = dt.bookShops.Where(a => a.idBook == idBook).Single();
                var price = filterBook.priceBook;
                var filterUser = dt.usersShops.Where(a => a.emailAddress == user).Single();
                filterUser.bankUser = filterUser.bankUser - price;
                userBookShop ubs = new userBookShop();
                ubs.idBook = filterBook.idBook;
                ubs.emailAddress = filterUser.emailAddress;
                dt.userBookShops.InsertOnSubmit(ubs);
                dt.SubmitChanges();
                return true;
            }
        }

        public void createBook(bookShop b,byte[] p,string pdf,string im,byte[] i)
        {
            b.pathBook = UploadBooks(p, pdf);
            b.pathimages = UploadImages(i, im);
            dt.bookShops.InsertOnSubmit(b);
            dt.SubmitChanges();
        }

        public void createLogin(usersShop u, string png, byte[] s)
        {
            u.pathimages = UploadImages(s, png);
            dt.usersShops.InsertOnSubmit(u);
            dt.SubmitChanges();
        }
        public List<bookShop> getAllbook()
        {
            List<bookShop> bsd = new List<bookShop>();
            foreach (var a in dt.bookShops)
            {
                bsd.Add(a);
            }
            return bsd;
        }

        public List<string> getAllCatalogy()
        {
            List<string> listCata = new List<string>();
            foreach (var i in dt.bookShops)
            {
                var a = i.category;
                listCata.Add(a);
            }
            return listCata;
        }

        public List<userBookShop> getUserBook(string user, int idBook)
        {
            List<userBookShop> ubsd = new List<userBookShop>();
            foreach (var a in dt.userBookShops)
            {
                ubsd.Add(a);
            }
            return ubsd;
        }

        public usersShop loginBook(string users, string password)
        {
            var filterUser = dt.usersShops.Where(a => a.emailAddress == users && a.passWordName == password).ToList();
            if (filterUser.Count == 1)
            {
                return filterUser[0];
            }
            else
            {
                return null;
            }
        }
        public Stream convertToSteam(string u)
        {
            var a = new MemoryStream();
            var b = File.ReadAllBytes(u);
            a.Write(b, 0, b.Length);
            a.Position = 0;
            return a;
        }
        public string UploadBooks(byte[] u, string n)
        {
            string filename = String.Format("{0}{1}", Guid.NewGuid(), n);
            string pathfolder = String.Format(@"{0}\Books", path);
            string pathResuft = String.Format(@"\Books\{0}", filename);
            string pathfile = Path.Combine(pathfolder, filename);
            CreateDirectoryIfNotExists(pathfile);
            using (var file = File.Create(pathfile))
            {
                var a = new MemoryStream(u);
                 a.CopyTo(file);
            }
            return pathResuft;
        }
        public string UploadImages(byte[] u, string n)
        {
            string filename = String.Format("{0}{1}", Guid.NewGuid(), n);
            string pathfolder = String.Format(@"{0}\Images", path);
            string pathResuft = String.Format(@"\Images\{0}", filename);
            string pathfile = Path.Combine(pathfolder, filename);
            CreateDirectoryIfNotExists(pathfile);
            using (var file = File.Create(pathfile))
            {
                var a = new MemoryStream(u);
                a.CopyTo(file);
            }
            return pathResuft;
        }

        private void CreateDirectoryIfNotExists(string filePath)
        {
            var directory = new FileInfo(filePath).Directory;
            if (directory == null) throw new Exception("Directory could not be determined for the filePath");

            Directory.CreateDirectory(directory.FullName);
        }

        public bool checkEmail(string email)
        {
            var b=dt.usersShops.Where(a => a.emailAddress == email).ToList();
            if (b.Count > 0)
            {
                return false;
            }
            else { return true; }
        }

        public byte[] getFile(string pathClient)
        {
            string pathfile = String.Format(@"{0}\{1}", path, pathClient);
            var a = File.ReadAllBytes(pathfile);
            return a;
        }

        //public void UploadDocument(byte[] data)
        //{
        //    string filename = DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "") + ".jpg";//define name of image.
        //    string pathfolder = String.Format(@"\Books\{0}", filename);
        //    string pathfile = Path.Combine(path, pathfolder);
        //    CreateDirectoryIfNotExists(pathfile);
        //    using (var file = File.Create(pathfile))
        //    {
        //        Stream u = new MemoryStream(data);
        //        u.CopyTo(file);
        //    }
        //    //FileStream fs = new FileStream(pathfile, FileMode.Create,
        //    //                               FileAccess.Write);
        //    //fs.Write(data, 0, data.Length);
        //    //fs.Close();
        //}

        //public void UpLoadImages(Stream a)
        //{
        //    string filename = DateTime.Now.ToString("dd:mm:yyyy hh:ss").Replace(" ", "").Replace(":", "").Replace("/", "") + Guid.NewGuid() +".jpg";//define name of image.
        //    string pathfolder = String.Format(@"\Books\{0}", filename);
        //    string pathfile = Path.Combine(path, filename);
        //    CreateDirectoryIfNotExists(pathfile);
        //    using (var file = File.Create(pathfile))
        //    {
        //        a.CopyTo(file);
        //    }
        //}
    }
}
