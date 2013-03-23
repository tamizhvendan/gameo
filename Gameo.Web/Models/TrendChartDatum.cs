using System.Collections.Generic;

namespace Gameo.Web.Models
{
    public class TrendChartDatum
    {
        public string Name { get; set; }
        public IList<int> Data { get; set; }
    }
}