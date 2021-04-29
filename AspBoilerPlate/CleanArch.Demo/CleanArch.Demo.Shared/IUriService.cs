using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Interfaces
{
   public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
