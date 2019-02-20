namespace ShopParser.BD
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BDConect : DbContext
    {
        public BDConect()
            : base("name=BDConect")
        {
        }

        public virtual DbSet<AllGoods> AllGoods { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ChangeDynamics> ChangeDynamics { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<GoodsDescription> GoodsDescription { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<CategoriesPath> CategoriesPath { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllGoods>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<AllGoods>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<AllGoods>()
                .Property(e => e.IdOnSite)
                .IsUnicode(false);

            modelBuilder.Entity<AllGoods>()
                .Property(e => e.ReferenceToTheOriginal)
                .IsUnicode(false);

            modelBuilder.Entity<AllGoods>()
                .HasMany(e => e.ChangeDynamics)
                .WithRequired(e => e.AllGoods)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AllGoods>()
                .HasMany(e => e.GoodsDescription)
                .WithRequired(e => e.AllGoods)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.AllGoods)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasOptional(e => e.CategoriesPath)
                .WithRequired(e => e.Categories);

            modelBuilder.Entity<Currency>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.AllGoods)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shops>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Shops>()
                .HasMany(e => e.AllGoods)
                .WithRequired(e => e.Shops)
                .HasForeignKey(e => e.ShopId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shops>()
                .HasMany(e => e.Categories)
                .WithRequired(e => e.Shops)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoriesPath>()
                .Property(e => e.PathToCategory)
                .IsUnicode(false);
        }
    }
}
