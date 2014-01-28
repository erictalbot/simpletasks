using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using $rootnamespace$.Entities;
using System.Web.Security;

namespace $rootnamespace$.Data
{
    public partial class ApplicationDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationDB()
        {

        }
        
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Configurations.Add(new UserMap());
           
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }

   
}