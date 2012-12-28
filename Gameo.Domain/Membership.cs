using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gameo.Domain
{
    public class Membership : Entity
    {
        public string MembershipId
        {
            get
            {
                return string.Format("{0}{1}{2}-{3}", IssuedOn.Year, IssuedOn.Month, IssuedOn.Day, Customer1ContactNumber);
            }
        }

        public double RemainingHours
        {
            get
            {
                return Math.Round(reCharges.Sum(reCharge => reCharge.Hours) - games.Sum(game => game.HoursPlayed), 1);
            }
        }

        [Required(ErrorMessage = "Customer Name is required.")]
        public string Customer1Name { get; set; }

        [Required(ErrorMessage = "Customer Contact Number is required.")]
        public string Customer1ContactNumber { get; set; }

        public string Customer2ContactNumber { get; set; }
        public string Customer2Name { get; set; }

        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn
        {
            get
            {
                if (reCharges.Any())
                {
                    return reCharges.Last().RechargedOn.AddMonths(6);
                }
                return DateTime.MinValue;
            }
        }

        public bool IsExpired
        {
            get
            {
                if (reCharges.Any())
                {
                    return ExpiresOn < DateTime.Now;
                }
                return true;
            }
        }

        private readonly List<Game> games;
        private readonly List<MembershipReCharge> reCharges;

        public IEnumerable<Game> Games { get { return games; } }

        public IEnumerable<MembershipReCharge> ReCharges { get { return reCharges; } }

        public Membership()
        {
            games = new List<Game>();
            reCharges = new List<MembershipReCharge>();
            IssuedOn = DateTime.Now;
        }

        public void Recharge(MembershipReCharge membershipReCharge)
        {
            reCharges.Add(membershipReCharge);
        }

        public void AddGame(Game game)
        {
            game.GamePaymentType = GamePaymentType.Membership;
            games.Add(game);
        }

        public string BranchName { get; set; }
    }
}