using System;
using System.Collections.Generic;
using MS.Internal.Xml.XPath;

namespace Gameo.Domain
{
    public class Membership : Entity
    {
        public string MembershipId { get; set; }
        public string Customer1Name { get; set; }
        public string Customer1ContactNumber { get; set; }
        public string Customer2ContactNumber { get; set; }
        public string Customer2Name { get; set; }
        public double Hours { get; set; }
        public decimal Price { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime LastUsedOn { get; set; }

        private readonly List<Game> games;
        private readonly List<MembershipRefill> memberShipRefills;

        public IEnumerable<Game> Games { get { return games; }
        }

        public IEnumerable<MembershipRefill> MemberShipRefills { get { return memberShipRefills; } }

        public Membership()
        {
            games = new List<Game>();
            memberShipRefills = new List<MembershipRefill>();
        }

    }
}