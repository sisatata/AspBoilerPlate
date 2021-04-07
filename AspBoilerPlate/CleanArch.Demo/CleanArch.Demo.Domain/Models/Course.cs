using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArch.Demo.Domain.Models
{
    public class Course : BaseEntity<Guid>
    {

        public string Name { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
