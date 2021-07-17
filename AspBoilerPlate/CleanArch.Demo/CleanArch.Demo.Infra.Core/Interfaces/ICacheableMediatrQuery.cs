using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Infra.Core.Interfaces
{
   public interface ICacheableMediatrQuery
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }
    }
}
