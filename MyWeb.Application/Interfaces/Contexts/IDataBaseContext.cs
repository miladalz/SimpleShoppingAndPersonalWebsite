using Microsoft.EntityFrameworkCore;
using MyWeb.Domain.Entities.Carts;
using MyWeb.Domain.Entities.Finances;
using MyWeb.Domain.Entities.HomePages;
using MyWeb.Domain.Entities.Orders;
using MyWeb.Domain.Entities.Personal;
using MyWeb.Domain.Entities.Products;
using MyWeb.Domain.Entities.Users;

namespace MyWeb.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImages> ProductImages { get; set; }
        DbSet<ProductFeatures> ProductFeatures { get; set; }
        DbSet<Slider> Sliders { get; set; }
        DbSet<HomePageImages> HomePageImages { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<RequestPay> RequestPays { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<About> Abouts { get; set; }
        DbSet<AboutTask> Tasks { get; set; }
        DbSet<Home> Homes { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<ServiceItem> ServiceItem { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<ProjectItem> ProjectItems { get; set; }
        DbSet<Price> Prices { get; set; }
        DbSet<PriceItem> PriceItems { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<ContactItem> ContactItems { get; set; }
        DbSet<Clinet> Clinets { get; set; }
        DbSet<SocialMedia> SocialMedias { get; set; }
        DbSet<Support> Supports { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<FeatureItem> FeatureItems { get; set; }
        DbSet<Counter> Counters { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<TeamMember> TeamMembers { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<SiteNewsMember> SiteNewsMembers { get; set; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
