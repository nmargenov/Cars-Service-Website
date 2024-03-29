using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppUni.Entities
{
    public class Task
    {
        public enum TaskStatus
        {
            InProgress,
            Done
        }

        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public int OwnerId { get; set; }
        public int AssigneeId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        [ForeignKey("AssigneeId")]
        public virtual User Assignee { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
    }
}