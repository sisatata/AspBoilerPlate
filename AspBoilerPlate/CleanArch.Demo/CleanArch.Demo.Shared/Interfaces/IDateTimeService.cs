using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Shared.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc
        {
            get;
        }
    }
}
