using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppUni.Entities;

namespace WebAppUni.ViewModels.MyCars
{
    public class IndexVM
    {
        public List<Task> Tasks { get; set; }
        public List<Car> Cars { get; set; }
    }
}