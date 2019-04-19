using System;
using System.Data.Entity;
using WebApplication6.Models;

namespace WebApplication6.MyDatabase
{
    public class SkorModelDB : DbContext
    {
        // Your context has been configured to use a 'SkorModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApplication6.SkorModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SkorModel' 
        // connection string in the application configuration file.
        public SkorModelDB() : base("SkorModelDB")
        {
        }

        public virtual DbSet<CategoryModel> CategoriesTable { get; set; }
        public virtual DbSet<SkorModel> SkorTable { get; set; }
    }

}