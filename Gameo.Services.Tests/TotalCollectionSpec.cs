using System.Collections.Generic;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class TotalCollectionSpec
    {
        private TotalCollection totalCollection;

        [SetUp]
        public void SetUp()
        {
            totalCollection = new TotalCollection();
        }

        [Test]
        public void TotalOneTimeGamePaymentCollection_Returns_Total_Price_Of_OneTime_Game_Payments()
        {
            totalCollection.OneTimePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };

            totalCollection.TotalOneGamePaymentCollection.ShouldEqual(30);
        }

        [Test]
        public void TotalPackageGamePaymentCollection_Returns_Total_Price_Of_OneTime_Game_Payments()
        {
            totalCollection.PackagePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };

            totalCollection.TotalPackageGamePaymentCollection.ShouldEqual(30);
        }

        [Test]
        public void TotalRechargeCollection_Returns_Total_Price_Of_Recharges_Made()
        {
            totalCollection.MembershipReCharges = new List<MembershipReCharge> { new MembershipReCharge { Price = 50 }, new MembershipReCharge { Price = 100 } };

            totalCollection.TotalRechargeCollection.ShouldEqual(150);
        }

        [Test]
        public void NetCollection_Returns_The_sum_of_total_recharges_and_game_payments()
        {
            totalCollection.PackagePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };
            totalCollection.MembershipReCharges = new List<MembershipReCharge> { new MembershipReCharge { Price = 50 }, new MembershipReCharge { Price = 100 } };
            totalCollection.OneTimePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };

            totalCollection.NetCollection.ShouldEqual(210);
        }
    }
}