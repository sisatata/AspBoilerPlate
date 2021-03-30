using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.ViewModels
{
   public class PermissionDto
    {
        
            public string RoleId { get; set; }
            public IList<RoleClaimsDto> RoleClaims { get; set; }
        
        

    }
    public class RoleClaimsDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}
