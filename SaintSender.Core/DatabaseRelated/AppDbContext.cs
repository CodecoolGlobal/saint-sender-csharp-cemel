using Microsoft.EntityFrameworkCore;
using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.DatabaseRelated
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base()
        {
             
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Host=localhost;Port=5432;Database=saintsender;Username=postgres;Password=szopacs11");
            base.OnConfiguring(optionBuilder);
        }
    }

   
}
