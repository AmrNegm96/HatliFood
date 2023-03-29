using HatliFood.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HatliFood.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<DeliveryGuy> DeliveryGuys { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<HatliFood.Models.Admin> Admin { get; set; } = default!;
    }
}
