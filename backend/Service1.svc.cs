using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ST1_Backend
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DataClasses1DataContext DB = new DataClasses1DataContext();
      

        public List<Product> getProducts()
        {
            dynamic Prods = (from prod in DB.Products
                             where prod.StockQuantity > 0
                             select prod).DefaultIfEmpty();
            try
            {
                if(Prods != null)
                {
                    List<Product> tempProdsList = new List<Product>();


                    foreach (var prod in Prods)
                    {
                        Product pd = new Product
                        {
                            ProductID = prod.ProductID,
                            ProductName = prod.ProductName,
                            Price = prod.Price,
                            ImageURL = prod.ImageURL,
                            Type = prod.Type

                        };

                        tempProdsList.Add(pd);

                    }
                    return tempProdsList;

                    
                }

                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


            
        }

        public Product getProductById(int Id)
        {

            var Prod = (from prod in DB.Products
                             where prod.StockQuantity > 0
                             &&
                             prod.ProductID.Equals(Id)
                             select prod).FirstOrDefault();


            try
            {
                if (Prod != null)
                {
                    Product pd = new Product
                    {
                        ProductID = Prod.ProductID,
                        ProductName = Prod.ProductName,
                        Price = Prod.Price,
                        ImageURL = Prod.ImageURL,
                        Description = Prod.Description

                    };
                    return pd;
                }
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Product> getProductsByType(string type)
        {
             
            
            List<Product> tempProdsList = new List<Product>();
            foreach (var prod in getProducts())
            {
                if(prod.Type == type)
                {
                  
                    tempProdsList.Add(prod);
                }

            }
            return tempProdsList;
        }




        /*prodcust managmetn*/
        public bool deleteProduct(int prodId)
        {
            try
            {
                // Find the product in the database by its ID
                var existingProduct = (from p in DB.Products
                                       where p.ProductID == prodId
                                       select p).FirstOrDefault();
                if (existingProduct == null)
                {
                    return false; // Product not found
                }

                // Delete the product from the database
                DB.Products.DeleteOnSubmit(existingProduct);

                // Submit changes to the database
                DB.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool editProduct(Product prod)
        {
            try
            {
                // Find the product in the database by its ID
                var existingProduct = (from p in DB.Products
                                       where p.ProductID == prod.ProductID
                                       select p).FirstOrDefault();
                if (existingProduct == null)
                {
                    return false; // Product not found
                }

                // Update the product details
                existingProduct.ProductName = prod.ProductName;
                existingProduct.Description = prod.Description;
                existingProduct.Price = prod.Price;
                existingProduct.Type = prod.Type;
                existingProduct.ImageURL = prod.ImageURL;
                existingProduct.StockQuantity = prod.StockQuantity;
                existingProduct.Popularity = prod.Popularity;

                // Submit changes to the database
                DB.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool InsertNewProduct(Product prod)
        {
            try
            {
                // Add the new product to the Products table
                DB.Products.InsertOnSubmit(prod);

                // Submit changes to the database
                DB.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }




        public User UserLogIn(string email, string password)
        {

            var tempUser = (from u in DB.Users
                            where u.Email.Equals(email) &&
                            u.PasswordHash.Equals(password)
                            select u).FirstOrDefault();


           if(tempUser != null)
            {
                return new User {
                    UserID = tempUser.UserID,
                    Name = tempUser.Name,
                    Surname = tempUser.Surname,
                    IsManager = tempUser.IsManager,
                    Email = tempUser.Email
                };
            }
            return null;
        }

        public dynamic registerUser(string name, string secName, string surname, string passHash, string email, string address, string phone)
        {
            var tempUser = (from u in DB.Users
                            where u.Email.Equals(email) 
                            select u).FirstOrDefault();

            if (tempUser != null)
            {
                return "User with email : [" + email  +"] Exists in the database";
            }

            try
            {
                User regUser = new User
                {
                    Name = name,
                    SecName = secName,
                    Surname = surname,
                    PasswordHash = passHash,
                    Email = email,
                    Address = address,
                    PhoneNumber = phone
                };
                DB.Users.InsertOnSubmit(regUser);
                DB.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex); 
                return false;
            }
        }

        public List<ReviewUser> getReviews(int prodId)
        {
            try
            {
                // Fetch reviews for the specified product ID and join with the User table to get the User's name
                var reviews = from r in DB.Reviews
                              join u in DB.Users on r.UserID equals u.UserID
                              where r.ProductID == prodId
                              select new
                              {
                                  r.ReviewID,
                                  r.ReviewText,
                                  r.Rating,
                                  UserName = u.Name + " " + u.Surname,  // Concatenate user's full name
                                  r.ReviewDate  // Use ReviewDate from the Review table
                              };

                List<ReviewUser> RUList = new List<ReviewUser>();

                foreach (var r in reviews)
                {
                    RUList.Add(new ReviewUser
                    {
                        ReviewID = r.ReviewID,
                        UserName = r.UserName,
                        ReviewText = r.ReviewText,
                        Rating = r.Rating ?? 0,  // Ensure the rating is non-null (default to 0 if null)
                        ReviewDate = r.ReviewDate ?? DateTime.Now  // Default to current date if ReviewDate is null
                    });
                }

                return RUList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null if an error occurs
            }
        }

        public bool addReviews(int prodId, int userId, int stars, string reviewText, DateTime date)
        {
            throw new NotImplementedException();
        }

        public bool addProdToCart(int prodId, int userId, int quntity)
        {
            // Check if the product exists and has enough stock
            var product = (from p in DB.Products 
                           where p.ProductID == prodId
                           select p).FirstOrDefault();
            if (product == null || product.StockQuantity < quntity)
            {
                return false;  // Product doesn't exist or not enough stock
            }

            // Add product to the shopping cart
            ShoppingCart cartItem = new ShoppingCart
            {
                UserID = userId,
                ProductID = prodId,
                Quantity = quntity,
                 
                
            };

            DB.ShoppingCarts.InsertOnSubmit(cartItem);

            // Reduce stock quantity
            product.StockQuantity -= quntity;

            // Save changes to the database
            DB.SubmitChanges();
            return true;
        }

        public bool removeFromCart(int Id)
        {
            var cartItem = (from c in DB.ShoppingCarts 
                            where c.ProductID == Id
                            select c).FirstOrDefault();
            if (cartItem == null)
            {
                return false;  // Cart item not found
            }

            // Restore stock quantity
            var product = (from p in DB.Products 
                           where p.ProductID == cartItem.ProductID
                           select p).FirstOrDefault();
            if (product != null)
            {
                product.StockQuantity += cartItem.Quantity;
            }

            // Remove the cart item
            DB.ShoppingCarts.DeleteOnSubmit(cartItem);

            // Save changes to the database
            DB.SubmitChanges();

            return true;
        }

        public List<Product> getCartProducts(int userId)
        {
            // Fetch the user's cart items
            var cartItems =(from c in DB.ShoppingCarts
                            where c.UserID == userId
                            select c);

            List<Product> temp = new List<Product>();


            foreach(var Item in cartItems) {
                var Prod = (from prod in DB.Products
                            where prod.StockQuantity > 0
                            &&
                            prod.ProductID.Equals(Item.ProductID)
                            select prod).FirstOrDefault();


                try
                {
                    if (Prod != null)
                    {
                        Product pd = new Product
                        {
                            ProductID = Prod.ProductID,
                            ProductName = Prod.ProductName,
                            Price = Prod.Price,
                            ImageURL = Prod.ImageURL,
                            Description = Prod.Description

                        };
                        temp.Add(pd);
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

            }
            
            //var products = cartItems.Select(ci => DB.Products.FirstOrDefault(p => p.ProductID == ci.ProductID)).ToList();

            return temp;
        }

        public int getCartQuntityfrom(int PId)
        {
            var cartItems = (from c in DB.ShoppingCarts
                             where c.ProductID == PId
                             select c).FirstOrDefault();
            if (cartItems == null)
                return -1;
            else
                return cartItems.Quantity;

        }

        public bool updateQuntity(int PId , int Qunt)
        {
            var cartItems = (from c in DB.ShoppingCarts
                             where c.ProductID == PId
                             select c).FirstOrDefault();

            if (cartItems != null) {

                cartItems.Quantity = Qunt;
                DB.SubmitChanges();
                return true;
            }

            return false;

        }

        public List<User> getAllUsers()
        {

            var tempUser = (from u in DB.Users
                           
                            select u).DefaultIfEmpty();

            List<User> asll = new List<User>();
            if (tempUser != null)
            {
                foreach(var u in tempUser) {
                asll.Add( new User
                    {
                        UserID = u.UserID,
                        Name = u.Name,
                        Surname = u.Surname,
                        IsManager = u.IsManager,
                        Email = u.Email
                    });
                }
            }


            return asll;
            
        }

        public bool DeleteUser(int userId)
        {
            // Find the user using LINQ
            var user = DB.Users.FirstOrDefault(u => u.UserID == userId);

            if (user != null)
            {
                DB.Users.DeleteOnSubmit(user);  // Remove the user
                DB.SubmitChanges();       // Commit the changes
                return true;
            }

            return false; // User not found
        }



        public bool ChangeUserRole(int userId)
        {
            // Find the user using LINQ
            var user = DB.Users.FirstOrDefault(u => u.UserID == userId);

            if (user != null)
            {
                user.IsManager = !user.IsManager;
                  
                DB.SubmitChanges();      
                return true;
            }

            return false; // User not found
        }
        public List<Invoice> GetUserInvoices(int userId)
        {
            var invoices = (from i in DB.Invoices
                            where i.UserID == userId
                            select i).ToList();

            return invoices;
        }

        public bool DeleteInvoice(int invoiceId)
        {
            var invoice = DB.Invoices.FirstOrDefault(i => i.InvoiceID == invoiceId);
            if (invoice != null)
            {
                DB.Invoices.DeleteOnSubmit(invoice);
                DB.SubmitChanges();
                return true;
            }
            return false;
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return DB.Invoices.FirstOrDefault(i => i.InvoiceID == invoiceId);
        }


    }
}
