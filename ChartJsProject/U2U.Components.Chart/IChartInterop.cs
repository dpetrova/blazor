using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.Components.Chart
{
    public interface IChartInterop
    {
        void CreateLineChart(string id, LineChartData data, ChartOptions options);
    }
}
