using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared;
using CleanArch.Demo.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArch.Demo.Domain.Models
{
    public class Course : BaseEntity<Guid>, IAuditable
    {

        public string Name { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
