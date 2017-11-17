using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ServiceBookShop.Models;

namespace ServiceBookShop
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceBookShop" in both code and config file together.
    [ServiceContract]
    public interface IServiceBookShop
    {
        [OperationContract]
        usersShop loginBook(string users,string password);

        [OperationContract]
        void createLogin(usersShop u, string png, byte[] s);

        [OperationContract]
        bool checkEmail(string email);

        [OperationContract]
        byte[] getFile(string path);

        //[OperationContract]
        //Stream Download(string u);
        //[OperationContract]
        //void UploadDocument(byte[] data);

        //[OperationContract]
        //void UpLoadImages(Stream a);

        [OperationContract]
        List<bookShop> getAllbook();

        [OperationContract]
        List<userBookShop> getUserBook(string user, int idBook);

        [OperationContract]
        bool buyBook(int idBook, string user);

        [OperationContract]
        List<string> getAllCatalogy();

        [OperationContract]
        void createBook(bookShop b, byte[] p, string pdf, string im, byte[] i);

    }
}
