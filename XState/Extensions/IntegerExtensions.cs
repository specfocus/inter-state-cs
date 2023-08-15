using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XState.Extensions
{
    // using Microsoft.AspNetCore.Mvc.Filters;

    public static class IntegerExtensions
    {
        public static string ToFilterScopeName(this int integer)
            => integer switch
            {
                10 => nameof(FilterScope.Global),
                20 => nameof(FilterScope.Controller),
                30 => nameof(FilterScope.Action),
                _ => "Undefined Scope",
            };
    }
}
