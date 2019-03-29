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
            var inProgram = true;
            logger.Info("Program started");
            do
            {

                // Create and save a new Blog
                Console.WriteLine("\nEnter Your Selection:");
                Console.WriteLine("1) Display Blogs");
                Console.WriteLine("2) Add Blog");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("4) Display Posts");
                Console.WriteLine("5) To Exit Program");

                var choice = int.Parse(Console.ReadLine());
                logger.Info("Selection " + choice + " was selected.");

                switch (choice)
                {
                    case 1:
                        {
                            var db = new BloggingContext();
                            // Display all Blogs from the database
                            var query = db.Blog.OrderBy(b => b.Name);
                            Console.WriteLine(query.Count()+" blogs returned");

                            Console.WriteLine("All blogs in the database:");
                            foreach (var item in query)
                            {
                                Console.WriteLine(item.Name);
                            }
                            db.SaveChanges();

                            break;
                        }
                    case 2:
                        {
                            var nameIsValid = false;
                            do
                            {
                                Console.Write("Enter a name for your new blog");
                                var name = Console.ReadLine();

                                if (name is null)
                                {
                                    logger.Info("Name cannot be null");
                                }
                                else
                                {
                                    nameIsValid = true;
                                    var blog = new Blog { Name = name };

                                    var db = new BloggingContext();
                                    db.AddBlog(blog);
                                    logger.Info("Blog added - {name}", name);
                                }
                            } while (!nameIsValid);

                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Select the ID number of the blog you'd like to post to\n");

                            var db = new BloggingContext();
                            // Display all Blogs from the database
                            var query = db.Blog;
                            var post = new Post();
                            var validBlog = false;

                            do
                            {
                                Console.WriteLine("All blogs in the database:\n");

                                foreach (var item in query)
                                {
                                    Console.WriteLine(item.BlogId + ") " + item.Name);
                                }

                                var blogChoice = 0;

                                try
                                {
                                    blogChoice = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    logger.Info(blogChoice + "is not a valid integer.");
                                }

                                var blogs = db.Blog.Where(p => p.BlogId == blogChoice);

                                if (blogs.Count() == 0)
                                {
                                    logger.Info("\n{choice} is not a valid Blog Id", blogChoice);
                                }
                                else
                                {
                                    var title = "";
                                    var titleIsValid = false;
                                    do
                                    {
                                        Console.Write("\nEnter a title for your post");
                                        title = Console.ReadLine();

                                        if (title is null)
                                        {
                                            logger.Info("Name cannot be null");
                                        }
                                        else
                                        {
                                            titleIsValid = true;

                                        }
                                    } while (!titleIsValid);


                                    Console.WriteLine("\nEnter the post content:");
                                    var content = Console.ReadLine();

                                    post = new Post { Title = title, Content = content, BlogId = blogChoice };
                                }
                            } while (!validBlog);

                            db.AddPost(post);
                            logger.Info("\nPost added to - {name}", post.Blog.Name);
                            break;
                        }
                    case 4:
                        {
                            var db = new BloggingContext();
                            // Display all Blogs from the database
                            var query = db.Blog;
                            var choiceIsValid = false;

                            do
                            {
                                Console.WriteLine("Select the blog's posts to display:");
                                Console.WriteLine("0) Display all posts");
                                foreach (var item in query)
                                {
                                    Console.WriteLine(item.BlogId + ") Display all posts from " + item.Name);
                                }

                                var postChoice = 0;

                                try
                                {
                                    postChoice = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    logger.Info(postChoice + "is not a valid integer.");
                                }

                                if (postChoice == 0)
                                {
                                    var posts = db.Post;
                                    Console.WriteLine($"{posts.Count()} post(s) returned");
                                    foreach(var p in posts)
                                    {
                                        Console.WriteLine("Blog: " + p.Blog.Name);
                                        Console.WriteLine("Title: " + p.Title);
                                        Console.WriteLine("Content: " + p.Content);
                                    }
                                    choiceIsValid = true;
                                }
                                else
                                {
                                    if (db.Blog.Where(p => p.BlogId == postChoice).Count() == 0)
                                    {
                                        logger.Info("\n{choice} is not a valid Blog Id", postChoice);
                                    }
                                    else
                                    {
                                        var posts = db.Post.Where(p => p.BlogId == postChoice);
                                        Console.WriteLine($"{posts.Count()} post(s) returned");
                                        foreach (var p in posts)
                                        {
                                            Console.WriteLine("Title: " + p.Title);
                                            Console.WriteLine("Content: " + p.Content);
                                        }
                                        choiceIsValid = true;
                                    }
                                }
                            } while (!choiceIsValid);
                            
                            db.SaveChanges();

                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Are you sure you'd like to exit the program? (Y/N)");

                            var exitChoice = Console.ReadLine().ToUpper();
                            if(exitChoice == "Y")
                            {
                                inProgram = false;
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }


            } while (inProgram);
        }
    }
}
