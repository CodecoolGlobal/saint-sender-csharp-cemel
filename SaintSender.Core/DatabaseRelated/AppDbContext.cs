using Microsoft.Extensions.Configuration;
using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.DatabaseRelated
{
    public class AppDbContext : DbContext  
    {
        private readonly string schema;
        public AppDbContext(string schema) : base("Default")
        {
            this.schema = schema;
            Console.WriteLine(schema);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema(this.schema);
            base.OnModelCreating(builder);
        }

    }


}
