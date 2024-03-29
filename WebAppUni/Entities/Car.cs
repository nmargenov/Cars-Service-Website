using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppUni.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public int AddedByUserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int HorsePower { get; set; }
        public int YearMade { get; set; }
        public bool IsRunning { get; set; }

        [ForeignKey("AddedByUserId")]
        public virtual User AddedByUser { get; set; }
    }
}