using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.Components.Chart
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCharts( this IServiceCollection services) => services.AddTransient<IChartInterop, ChartInterop>();
    }
}
