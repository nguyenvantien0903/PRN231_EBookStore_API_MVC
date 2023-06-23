using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    public class eBookStoreDBContext : DbContext
    {
        public eBookStoreDBContext(DbContextOptions<eBookStoreDBContext> options) : base(options) { }

        public eBookStoreDBContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            //optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=123456;Database=MyStoreDB");
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("eBookStoreDB"));
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BookAuthor> BooksAuthors { get; set; }

        public virtual DbSet<Publisher> Publishers { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Author>(en =>
            //{
            //    en.HasKey(en => en.AuthorId);
            //});
            modelBuilder.Entity<Author>().HasData(
                    new Author { AuthorId = 1, Last_name = "Lmao", First_name = "Lmao", Phone = "1234567890", Address = "Address 1", City = "City 1",State = "State 1", Zip = "Zip 1", Email_address = "Email1@gmail.com"},
                    new Author { AuthorId = 2, Last_name = "Lmao2", First_name = "Lmao2", Phone = "12345678902", Address = "Address 2", City = "City 2", State = "State 2", Zip = "Zip 2", Email_address = "Email2@gmail.com" },
                    new Author { AuthorId = 3, Last_name = "Lmao3", First_name = "Lmao3", Phone = "12345678903", Address = "Address 3", City = "City 3", State = "State 3", Zip = "Zip 3", Email_address = "Email3@gmail.com" },
                    new Author { AuthorId = 4, Last_name = "Lmao4", First_name = "Lmao4", Phone = "12345678904", Address = "Address 4", City = "City 4", State = "State 4", Zip = "Zip 4", Email_address = "Email4@gmail.com" }
                );
            //modelBuilder.Entity<Role>(en =>
            //{
            //    en.HasKey(e => e.RoleId);
            //});
            modelBuilder.Entity<Role>().HasData(
                    new Role { RoleId = 1, Role_desc = "Admin" },
                    new Role { RoleId = 2, Role_desc = "User" }
                );
            //modelBuilder.Entity<Publisher>(en =>
            //{
            //    en.HasKey(e => e.PublisherId);
            //});
            modelBuilder.Entity<Publisher>().HasData(
                    new Publisher { PublisherId = 1, Publisher_name = "Publish 1", City = "City 1", State = "State 1", Country = "Country 1" },
                    new Publisher { PublisherId = 2, Publisher_name = "Publish 2", City = "City 2", State = "State 2", Country = "Country 2" },
                    new Publisher { PublisherId = 3, Publisher_name = "Publish 3", City = "City 3", State = "State 3", Country = "Country 3" }
                );
            //modelBuilder.Entity<User>(en =>
            //{
            //    en.HasKey(e => e.UserId);
            //    en.HasOne(r => r.Role).WithMany(u => u.Users).HasForeignKey(u => u.RoleId);
            //    en.HasOne(p => p.Publisher).WithMany(u => u.Users).HasForeignKey(u => u.PublisherId);
            //});
            modelBuilder.Entity<User>().HasData(
                    new User { UserId = 1, Email_adress = "Email1User@gmail.com", Password = "123456", Source = "Source1", First_name = "First1", Middle_name = "Middle1", Last_name = "Last1", RoleId = 1, PublisherId = 1, Hire_date = new DateTime(2022,12,12)},
                    new User { UserId = 2, Email_adress = "Email2User@gmail.com", Password = "123456", Source = "Source2", First_name = "First2", Middle_name = "Middle2", Last_name = "Last2", RoleId = 2, PublisherId = 2, Hire_date = new DateTime(2022, 2, 12) },
                    new User { UserId = 3, Email_adress = "Email3User@gmail.com", Password = "123456", Source = "Source3", First_name = "First3", Middle_name = "Middle3", Last_name = "Last2", RoleId = 2, PublisherId = 3, Hire_date = new DateTime(2022, 1, 12) }
                );
            //modelBuilder.Entity<Book>(en =>
            //{
            //    en.HasKey(e => e.BookId);
            //    en.HasOne(p => p.Publisher).WithMany(b => b.Books).HasForeignKey(b => b.PublisherId);
            //});
            modelBuilder.Entity<Book>().HasData(
                    new Book { BookId = 1, Title = "Title 1", Type = "Type 1", Price = 10.1m, Advance = 5.1m, Royalty = 1.1m, Ytd_sales = 100m, Notes = "Notes 1", Published_date = new DateTime(2022, 5, 24), PublisherId = 1},
                    new Book { BookId = 2, Title = "Title 2", Type = "Type 2", Price = 100.1m, Advance = 50.1m, Royalty = 10.1m, Ytd_sales = 1000m, Notes = "Notes 2", Published_date = new DateTime(2022, 5, 24), PublisherId = 2 },
                    new Book { BookId = 3, Title = "Title 3", Type = "Type 3", Price = 1000.1m, Advance = 500.1m, Royalty = 100.1m, Ytd_sales = 10000m, Notes = "Notes 3", Published_date = new DateTime(2022, 5, 24), PublisherId = 3 }
                );
            modelBuilder.Entity<BookAuthor>(en =>
            {
                en.HasKey(bu => new { bu.AuthorId, bu.BookId });
            });
            modelBuilder.Entity<BookAuthor>().HasData(
                    new BookAuthor { AuthorId = 1, BookId = 1, Author_order = 100, Royalty_percentage = 0.5m},
                    new BookAuthor { AuthorId = 2, BookId = 2, Author_order = 1000, Royalty_percentage = 0.55m },
                    new BookAuthor { AuthorId = 3, BookId = 3, Author_order = 10000, Royalty_percentage = 0.555m },
                    new BookAuthor { AuthorId = 4, BookId = 1, Author_order = 100000, Royalty_percentage = 0.5555m }
                );
        }
    }
}
