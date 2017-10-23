using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class RestAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public RestAPIContext() : base("name=AzureMediaPortalContext")
        {
        }

        public System.Data.Entity.DbSet<RestAPI.Models.Product> Products { get; set; }


    }

    [Table("Product")]
    public partial class Product
    {
        [Key]
        public System.Guid Key { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }


    }
}
