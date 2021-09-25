using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users;
            int userCount = users.ToList().Count;
            Console.WriteLine("Problem 1: Users in table: " + userCount);

        }


        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products.Where(p => p.Price >= 150);

            foreach (Product product in products)
            {
                Console.WriteLine(product.Name + " $" + product.Price);
            }
        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.

            var products = _context.Products;
            
            foreach (Product product in products)
            {
                if (product.Name.Contains('s'))
                {
                    Console.WriteLine(product.Name);
                }     
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            DateTime dt = new DateTime(2016, 01, 01);
            var users = _context.Users.Where(u => u.RegistrationDate < dt);
            
            foreach(User user in users)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.

            DateTime dt = new DateTime(2016, 01, 01);
            DateTime dt2 = new DateTime(2018, 01, 01);
            var users = _context.Users.Where(u => u.RegistrationDate > dt && u.RegistrationDate < dt2);

            foreach (User user in users)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }


        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var products = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == "afton@gmail.com");
            foreach (ShoppingCart product in products)
            {
                Console.WriteLine(product.Product.Name + " $" + product.Product.Price + " " + product.Quantity);
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var products = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
 
                Console.WriteLine("Total Price: " + products);
            
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var usersInRole = _context.UserRoles.Where(u => u.Role.RoleName == "Employee").Select(u => u.User.Id);
            var userShoppingCartProducts = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => usersInRole.Contains(sc.UserId));

            foreach (var shoppingCart in userShoppingCartProducts)
            {
                Console.WriteLine($"Email: {shoppingCart.User.Email} \n" +
                                $"Product: {shoppingCart.Product.Name} \n" +
                                $"Price: {shoppingCart.Product.Price} \n" +
                                $"Qty: {shoppingCart.Quantity}\n");
            }

        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Guitar",
                Description = "Acoustic cutaway",
                Price = 350
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productID = _context.Products.Where(r => r.Name == "Guitar").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                ProductId = productID,
                UserId = userId,
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Guitar").SingleOrDefault();
            product.Price = 400;
            _context.Products.Update(product);
            _context.SaveChanges();

        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }
        

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".

            Console.WriteLine("Please enter your email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            string password = Console.ReadLine();
            var user =_context.Users.Where(u => u.Email == email).SingleOrDefault();

            if (user != null) 
            {
                if (user.Email == email)
                {
                    if (user.Password == password)
                    {
                        Console.WriteLine("Signed In!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Email or Password.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Email or Password.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Email or Password.");
            }


        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var users = _context.Users.Select(u => u.Id).ToList();
            int grandTotal = 0;

            foreach (var user in users)
            {
                int total = 0;
                var userShoppingCartProducts = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.UserId == user);

                foreach (ShoppingCart product in userShoppingCartProducts)
                {
                    total += (int)product.Product.Price * (int)product.Quantity;
                }

                Console.WriteLine($"User: {user} Total: {total}");
                grandTotal += total;
            }

            Console.WriteLine("Grand Total: " + grandTotal);
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

            bool signedIn = false;
           //string userEmail;

            while (signedIn != true)
            {

                Console.WriteLine("Please enter your email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                string password = Console.ReadLine();
                var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();

                //User Sign in
                if (user != null)
                {
                    if (user.Email == email)
                    {
                        if (user.Password == password)
                        {
                            signedIn = true;
                            //userEmail = email;
                            Console.WriteLine("Signed In!");
                            UserInterface ui = new UserInterface();
                            ui.UserMenu(email);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Email or Password.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Email or Password.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Email or Password.");
                }
            }
        }

            //User interface for bonus 3
            public class UserInterface {
                
                ECommerceContext _context = new ECommerceContext();

                public void UserMenu(string email) 
                {
                    Console.WriteLine("\n\nEnter a menu Option: \n" +
                                    "1. View Shopping Cart \n" +
                                    "2. View Products \n" +
                                    "3. Add Product to Cart \n" +
                                    "4. Remove Item From Cart\n" +
                                    "5. Exit");
                    int userSelection = Convert.ToInt32(Console.ReadLine());

                    switch (userSelection)
                    {
                        case 1: ViewCart(email);
                                UserMenu(email);
                            break;
                        case 2: ViewProducts();
                                UserMenu(email);
                            break;
                        case 3: AddProduct(email);
                                UserMenu(email);
                            break;
                        case 4: RemoveProduct(email);
                                UserMenu(email);
                            break;
                        case 5: Environment.Exit(0);
                            break;

                    }

                }

                //Allows user to view items in cart
                public void ViewCart(string email) 
                {
                    var products = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == email);
                    foreach (ShoppingCart product in products)
                    {
                        Console.WriteLine("Product: " + product.Product.Id + " " + product.Product.Name + " $" + product.Product.Price + " Qty: " + product.Quantity);
                    }
                }

                public void ViewProducts()
                {
                    var products = _context.Products;
                    Console.WriteLine("\n--All Products--");              
                    foreach(Product product in products)
                    {
                        Console.WriteLine($"Product: {product.Id}. {product.Name}");
                    }

                }

              
                //Allows user to add product
                public void AddProduct(string email)
                {
                    Console.WriteLine("Enter Product ID to add to cart: ");
                    int userProductID = Convert.ToInt32(Console.ReadLine());

                    var productID = _context.Products.Where(r => r.Id == userProductID).Select(r => r.Id).SingleOrDefault();
                    var userId = _context.Users.Where(u => u.Email == email).Select(u => u.Id).SingleOrDefault();
                    ShoppingCart newShoppingCart = new ShoppingCart()
                    {
                        ProductId = productID,
                        UserId = userId,
                    };
                    _context.ShoppingCarts.Add(newShoppingCart);
                    _context.SaveChanges();
                }

                //Allows user to remove product
                public void RemoveProduct(string email) 
                {
                    Console.WriteLine("Enter Product ID to remove from cart: ");
                    int userProductID = Convert.ToInt32(Console.ReadLine());

                    var userId = _context.Users.Where(u => u.Email == email).Select(u => u.Id).SingleOrDefault();
                    var products = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == email).Where(ur => ur.ProductId == userProductID).ToList() ;
                
                    foreach (ShoppingCart product in products) {
                        _context.ShoppingCarts.Remove(product);
                        _context.SaveChanges();
                    }

                }
            }


    }

}

