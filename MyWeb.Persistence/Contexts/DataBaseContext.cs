using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Roles;
using MyWeb.Domain.Entities.Carts;
using MyWeb.Domain.Entities.Finances;
using MyWeb.Domain.Entities.HomePages;
using MyWeb.Domain.Entities.Orders;
using MyWeb.Domain.Entities.Personal;
using MyWeb.Domain.Entities.Products;
using MyWeb.Domain.Entities.Users;

namespace MyWeb.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImages> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<RequestPay> RequestPays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutTask> Tasks { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceItem> ServiceItem { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectItem> ProjectItems { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<PriceItem> PriceItems { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactItem> ContactItems { get; set; }
        public DbSet<Clinet> Clinets { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureItem> FeatureItems { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SiteNewsMember> SiteNewsMembers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(p => p.User)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.RequestPay)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);
            //Seed Data
            SeedData(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            ApplyQueryFilter(modelBuilder);
        }

        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductImages>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductFeatures>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomePageImages>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<RequestPay>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(p => !p.IsRemoved);//reflection

            modelBuilder.Entity<About>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<AboutTask>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Home>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Service>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ServiceItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Project>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProjectItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Price>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<PriceItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Contact>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ContactItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Clinet>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<SocialMedia>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Support>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Feature>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<FeatureItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Counter>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Team>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<TeamMember>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Message>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<SiteNewsMember>().HasQueryFilter(p => !p.IsRemoved);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRoles.Customer) });
        }
    }
}
