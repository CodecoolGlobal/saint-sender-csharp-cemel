
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SaintSender.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SaintSender.Core.DatabaseRelated.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}