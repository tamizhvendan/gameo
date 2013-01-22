using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;

namespace Gameo.Services
{
    public class TotalCollection
    {
        public decimal TotalOneGamePaymentCollection
        {
            get { return OneTimePaymentGames.Sum(game => game.Price); }    
        }

        public IEnumerable<Game> OneTimePaymentGames { get; internal set; }
        public IEnumerable<Game> PackagePaymentGames { get; internal set; }
        public IEnumerable<MembershipReCharge> MembershipReCharges { get; internal set; }

        public decimal TotalPackageGamePaymentCollection    
        {
            get { return PackagePaymentGames.Sum(game => game.Price); }
        }

        public decimal TotalRechargeCollection
        {
            get { return MembershipReCharges.Sum(recharge => recharge.Price); }
        }

        public decimal NetCollection
        {
            get { return TotalOneGamePaymentCollection + TotalPackageGamePaymentCollection + TotalRechargeCollection; }
        }

        public TotalCollection()
        {
            OneTimePaymentGames = Enumerable.Empty<Game>();
            PackagePaymentGames = Enumerable.Empty<Game>();
            MembershipReCharges = Enumerable.Empty<MembershipReCharge>();
        }
    }
}