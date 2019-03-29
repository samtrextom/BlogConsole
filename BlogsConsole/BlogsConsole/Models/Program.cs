using BlogsConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {

                // Create and save a new Blog
                Console.WriteLine("Enter Your Selection:");
                Console.WriteLine("1) Display Blogs");
                Console.WriteLine("2) Add Blog");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("4) Display Posts");

                var choice = int.Parse( Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter a name for your new blog");

                            var name = Console.ReadLine();

                            var blog = new Blog { Name = name };

                            var db = new BloggingContext();
                            db.AddBlog(blog);
                            logger.Info("Blog added - {name}", name);

                            // Display all Blogs from the database
                            var query = db.Blog.OrderBy(b => b.Name);

                            Console.WriteLine("All blogs in the database:");
                            foreach (var item in query)
                            {
                                Console.WriteLine(item.Name);
                            }
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            Console.WriteLine("Press Enter to Exit");
            String x = Console.ReadLine();
            logger.Info("Program ended");
        }
    }
}
