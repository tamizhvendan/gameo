using System.Collections.Generic;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class TotalCollectionSpec
    {
        private TotalDayCollection totalDayCollection;

        [SetUp]
        public void SetUp()
        {
            totalDayCollection = new TotalDayCollection();
        }

        [Test]
        public void TotalOneTimeGamePaymentCollection_Returns_Total_Price_Of_OneTime_Game_Payments()
        {
            totalDayCollection.OneTimePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };

            totalDayCollection.TotalOneGamePaymentCollection.ShouldEqual(30);
        }

        [Test]
        public void TotalPackageGamePaymentCollection_Returns_Total_Price_Of_OneTime_Game_Payments()
        {
            totalDayCollection.PackagePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };

            totalDayCollection.TotalPackageGamePaymentCollection.ShouldEqual(30);
        }

        [Test]
        public void TotalRechargeCollection_Returns_Total_Price_Of_Recharges_Made()
        {
            totalDayCollection.MembershipReCharges = new List<MembershipReCharge> { new MembershipReCharge { Price = 50 }, new MembershipReCharge { Price = 100 } };

            totalDayCollection.TotalRechargeCollection.ShouldEqual(150);
        }

        [Test]
        public void NetCollection_Returns_The_sum_of_total_recharges_and_game_payments()
        {
            totalDayCollection.PackagePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };
            totalDayCollection.MembershipReCharges = new List<MembershipReCharge> { new MembershipReCharge { Price = 50 }, new MembershipReCharge { Price = 100 } };
            totalDayCollection.OneTimePaymentGames = new List<Game> { new Game { Price = 10 }, new Game { Price = 20 } };

            totalDayCollection.NetCollection.ShouldEqual(210);
        }
    }
}