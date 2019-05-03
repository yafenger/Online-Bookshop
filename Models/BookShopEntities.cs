namespace Bookshop_CATeam11.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookShopEntities : DbContext
    {
        public BookShopEntities()
            : base("name=BookShopEntities")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Sales_Trans> Sales_Trans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.ISBN)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.Discount_Price)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Sales_Trans>()
                .Property(e => e.Total_Amount)
                .HasPrecision(5, 2);
        }
    }
}
