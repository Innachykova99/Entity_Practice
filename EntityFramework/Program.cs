using EntityFramework.Classes;

namespace EntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            //Clear the db 
            using (BloggingContext db = new BloggingContext())
            {
                db.Posts.RemoveRange(db.Posts);
                db.Blogs.RemoveRange(db.Blogs);
                db.SaveChanges();
            }

            // Create
            using (BloggingContext db = new BloggingContext())
            {
                Blog FirstBlog = new Blog { Url = "example.com" };

                db.Blogs.Add(FirstBlog);

                FirstBlog.Posts = new List<Post>
                {
                    new Post { Title = "InnaPost1", Content = "InnaChykovaContent" },
                    new Post { Title = "InnaPost2", Content = "18.10.1999"},
                    new Post { Title = "SomePost3", Content = "LoremIpsum"}
                };

                db.SaveChanges();

                Console.WriteLine("\nData after create:");
                var postsAfterCreate = db.Posts.ToList();
                foreach (Post post in postsAfterCreate)
                {
                    Console.WriteLine($"{post.Title}, {post.Content}");
                }

                // Update
                    Post postToUpdate = db.Posts.FirstOrDefault();

                if (postToUpdate != null)
                {
                    postToUpdate.Title = "UpdatedTitleInnaPost1";
                    postToUpdate.Content = "UpdatedNewContent";

                    db.Posts.Update(postToUpdate);
                    db.SaveChanges();
                }

                Console.WriteLine("\nData after update:");
                var postsAfterUpdate = db.Posts.ToList();

                foreach (Post post in postsAfterUpdate)
                {
                    Console.WriteLine($"{post.Title}, {post.Content}");
                }

                // Delete
                Post postToDelete = db.Posts.Skip(1).FirstOrDefault();

                if (postToDelete != null)
                {
                    db.Posts.Remove(postToDelete);
                    db.SaveChanges();
                }

                Console.WriteLine("\nData after delete:");
                var postsAfterDelete = db.Posts.ToList();

                foreach (Post post in postsAfterDelete)
                {
                    Console.WriteLine($"{post.Title}, {post.Content}");
                }
            }
        }
    }
}