using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ST1_Backend
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<Product> getProducts();

       
        [OperationContract]
        List<Product> getProductsByType(string type);




        [OperationContract]
        Product getProductById(int Id);


        [OperationContract]
        bool editProduct(Product prod);

        [OperationContract]
        bool InsertNewProduct(Product prod);

        [OperationContract]
        bool deleteProduct(int prodId);






        /* Methods for users and managment of users */

        [OperationContract]
        User UserLogIn(string email ,string password);


        [OperationContract]
        List<User> getAllUsers();

        [OperationContract]
        bool DeleteUser(int userId);

        [OperationContract]
        bool ChangeUserRole(int userId);



        [OperationContract]
         List<Invoice> GetUserInvoices(int userId);


        [OperationContract]
         bool DeleteInvoice(int invoiceId);

        [OperationContract]
         Invoice GetInvoiceById(int invoiceId);






        [OperationContract]
        dynamic registerUser(string name , string secName , string surname ,string passHash ,string email ,string address ,string phone);




        [OperationContract]
        List<ReviewUser> getReviews(int prodId);


        [OperationContract]
        bool addReviews(int prodId , int userId , int stars , string reviewText , DateTime date);




        [OperationContract]
        bool updateQuntity(int PId, int Qunt);



        [OperationContract]
        int getCartQuntityfrom(int PId);


        [OperationContract]
        bool addProdToCart(int prodId , int userId , int quntity );

        [OperationContract]
        bool removeFromCart(int Id);


        [OperationContract]
        List<Product> getCartProducts(int userId);



    }

    [DataContract]
    public class ReviewUser
    {
        [DataMember]
        public int ReviewID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string ReviewText { get; set; }

        [DataMember]
        public int Rating { get; set; }

        [DataMember]
        public DateTime ReviewDate { get; set; }
    }





}
