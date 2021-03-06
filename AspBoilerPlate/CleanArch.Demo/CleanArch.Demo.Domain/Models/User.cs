using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArch.Demo.Domain.Interfaces
{
   public class User : BaseEntity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUsers { get; set; }
        public string UserId { get; set; }
    }
}
