using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.Components.Chart
{
    /// <summary>
    /// It is always a good idea to hide specific implementation details behind a service class
    /// </summary>
    public class ChartInterop : IChartInterop
    {
        public IJSRuntime JSRuntime { get; }
        public ChartInterop(IJSRuntime jsRuntime) => JSRuntime = jsRuntime;
        public void CreateLineChart(string id, LineChartData data, ChartOptions options) => JSRuntime.InvokeAsync<string>("components.chart", id, data, options);
    }
}
