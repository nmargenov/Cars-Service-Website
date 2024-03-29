using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppUni.Entities;

namespace WebAppUni.ViewModels.Tasks
{
    public class EditVM
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int OwnerId { get; set; }
        public int AssigneeId { get; set; }

        [DisplayName("Title: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Title { get; set; }

        [DisplayName("Description: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Description { get; set; }

        public List<User> Users { get; set; }
    }
}