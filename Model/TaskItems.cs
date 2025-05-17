using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class TaskItems
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description{ get; set; }

        public int UserId { get; set; }  // Foreign Key
        [ForeignKey("UserId")]
        public User AssignedUser { get; set; }
    }
}
