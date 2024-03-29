using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppUni.Entities;

namespace WebAppUni.ViewModels.Tasks
{
    public class IndexVM
    {
        public Car Car { get; set; }
        public List<Task> Items { get; set; }
    }
}