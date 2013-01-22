﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class CollectionServiceSpec
    {
        private Mock<IGameRepository> gameRepositoryMock;
        private Mock<IMembershipRepository> membershipRepositoryMock;
        private CollectionService collectionService;
        private string branchName;
        private List<Game> games;
        private DateTime currentTime;

        [SetUp]
        public void SetUp()
        {
            gameRepositoryMock = new Mock<IGameRepository>();
            membershipRepositoryMock = new Mock<IMembershipRepository>();
            collectionService = new CollectionService(gameRepositoryMock.Object, membershipRepositoryMock.Object);
            branchName = "branch1";
            games = new List<Game>
                        {
                            new Game {Price = 50}, new Game { Price = 45}, 
                            new Game { Price = 300, PackageType = PackageType.Package_Of_3_Hours}, new Game { Price  = 500, PackageType = PackageType.Package_Of_5_Hours},
                            new Game { Price = 0, GamePaymentType = GamePaymentType.Membership}
                        };
            currentTime = DateTime.Now.ToIST();
        }

        [Test]
        public void GetTotalCollection_Retrieves_OneTimeGamePayements_In_Given_Branch_On_Given_Day()
        {
            gameRepositoryMock.Setup(repo => repo.GetGames(branchName, currentTime)).Returns(games);

            var totalCollection = collectionService.GetTotalCollection(branchName, currentTime);

            var oneTimePaymentGames = totalCollection.OneTimePaymentGames.ToList();
            oneTimePaymentGames.Count.ShouldEqual(2);
            oneTimePaymentGames.Sum(game => game.Price).ShouldEqual(95);
        }

        [Test]
        public void GetTotalCollection_Retrieves_PackageGamePayments_In_Given_Branch_On_Given_Day()
        {
            gameRepositoryMock.Setup(repo => repo.GetGames(branchName, currentTime)).Returns(games);

            var totalCollection = collectionService.GetTotalCollection(branchName, currentTime);

            var oneTimePaymentGames = totalCollection.PackagePaymentGames.ToList();
            oneTimePaymentGames.Count.ShouldEqual(2);
            oneTimePaymentGames.Sum(game => game.Price).ShouldEqual(800);
        }

        [Test]
        public void GetTotalCollection_Retrieves_MembershipRecharges_In_Given_Branch_On_Given_Day()
        {
            var membershipRecharges = Enumerable.Empty<MembershipReCharge>();
            membershipRepositoryMock.Setup(repo => repo.GetRecharges(branchName, currentTime)).Returns(membershipRecharges);

            var totalCollection = collectionService.GetTotalCollection(branchName, currentTime);
            
            totalCollection.MembershipReCharges.ShouldEqual(membershipRecharges);
        }
    }
}