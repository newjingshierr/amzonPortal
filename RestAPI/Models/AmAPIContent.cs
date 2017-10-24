using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace RestAPI.Models
{
    public class AmAPIContent: DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public AmAPIContent() : base("name=AmAPIContext")
        {
        }

        public System.Data.Entity.DbSet<RestAPI.Models.am_news> Am_News { get; set; }
    }

    [Table("am_news")]
    public partial class am_news
    {
        [Key]

        public int ID { get; set; }
        public string Title { get; set; }
        public string imagePath { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public DateTime PublishDate { get; set; }
        public int VisitCount  { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
    }
}