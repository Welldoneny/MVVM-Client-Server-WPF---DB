//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using Microsoft.EntityFrameworkCore;

using System.Data.Common;

namespace Server
{
    internal class BookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BooksDB.db");
        }
        public BookContext() => Database.EnsureCreated();
        public DbSet<Book> Books { get; set; }
    }
}