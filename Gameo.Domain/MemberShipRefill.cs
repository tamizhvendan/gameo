using System;

namespace Gameo.Domain
{
    public class MembershipRefill
    {
        public DateTime RefilledOn { get; set; }
        public decimal Price { get; set; }
        public double Hours { get; set; }
    }
}