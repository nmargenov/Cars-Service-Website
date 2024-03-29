using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppUni.ViewModels.ManageCars
{
    public class EditVM
    {
        public int Id { get; set; }
        public int AddedByUserId{ get; set; }

        [DisplayName("Brand: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Brand { get; set; }


        [DisplayName("Model: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string CarModel { get; set; }


        [DisplayName("Horse Power: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public int HorsePower { get; set; }


        [DisplayName("Year of production: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public int YearMade { get; set; }


        [DisplayName("Is the vehicle running?: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public bool IsRunning { get; set; }
    }
}