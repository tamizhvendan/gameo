﻿using System;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IGameRepository gameRepository;
        private readonly IMembershipRepository membershipRepository;
        private readonly IDailySaleDetailsRepository dailySaleDetailsRepository;

        public CollectionService(IGameRepository gameRepository, IMembershipRepository membershipRepository, IDailySaleDetailsRepository dailySaleDetailsRepository)
        {
            this.gameRepository = gameRepository;
            this.membershipRepository = membershipRepository;
            this.dailySaleDetailsRepository = dailySaleDetailsRepository;
        }

        public TotalCollection GetTotalCollection(string branchName, DateTime dateTime)
        {
            var games = gameRepository.GetGames(branchName, dateTime).ToList();
            return new TotalCollection
            {
                OneTimePaymentGames = games.Where(game => game.GamePaymentType == GamePaymentType.OneTime && game.PackageType == PackageType.No_Package).ToList(),
                PackagePaymentGames = games.Where(game => game.GamePaymentType == GamePaymentType.OneTime && game.PackageType != PackageType.No_Package).ToList(),
                MembershipReCharges = membershipRepository.GetRecharges(branchName, dateTime),
                DailySaleDetails = dailySaleDetailsRepository.GetDailySaleDetails(branchName, dateTime)
            };

        }
    }
}