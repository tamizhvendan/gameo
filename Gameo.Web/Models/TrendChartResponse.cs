using System.Collections.Generic;
using Gameo.Domain;
using Gameo.Services;

namespace Gameo.Web.Models
{
    public class TrendChartResponse
    {
        public IEnumerable<GamePriceTrend> GamePriceTrends { get; set; }
        public IEnumerable<TrendChartDatum> TrendCharts { get; set; }
    }
}