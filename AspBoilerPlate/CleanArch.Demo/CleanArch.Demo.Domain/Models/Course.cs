using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Domain.Models
{
   public class Course :BaseEntity<int>
    {
      
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
