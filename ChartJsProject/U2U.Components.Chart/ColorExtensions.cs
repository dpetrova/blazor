using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.Components.Chart
{
    public static class ColorExtensions
    {
        public static string ToJs(this Color c) => $"rgba({c.R}, {c.G}, {c.B}, {c.A})";
    }
}
