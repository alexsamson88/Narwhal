using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service
{
    public static class Extensions
    {
        public static bool IsDateBetween(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }
    }
}
