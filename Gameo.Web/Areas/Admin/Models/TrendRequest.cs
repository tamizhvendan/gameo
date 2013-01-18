using System;

namespace Gameo.Web.Areas.Admin.Models
{
    public class TrendRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string BranchName { get; set; }
    }
}