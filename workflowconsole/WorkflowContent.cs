using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace workflowconsole
{
    public class WorkflowContent : DbContext  {
        public WorkflowContent()
            : base("name=WorkflowEntity")
        {
        }


        public virtual DbSet<WorkflowEntity> WorkflowEntitySet { get; set; }
    }

    [Table("WorkflowEntity")]
    public class WorkflowEntity
    {
        [Key]
        public System.Guid Key { get; set; }

        public string assigner { get; set; }

        public string assignerName { get; set; }

        public DateTime submitDate { get; set; }

        public DateTime modified { get; set; }

        public string category { get; set; }
    }
}
