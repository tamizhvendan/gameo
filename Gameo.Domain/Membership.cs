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
                return Math.Round(ReCharges.Sum(reCharge => reCharge.Hours) - Games.Sum(game => game.HoursPlayed), 1);
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
                if (ReCharges.Any())
                {
                    return ReCharges.Last().RechargedOn.AddDays(180);
                }
                return DateTime.MinValue;
            }
        }

        public bool IsExpired
        {
            get
            {
                if (ReCharges.Any())
                {
                    return ExpiresOn < DateTime.UtcNow.ToIST();
                }
                return true;
            }
        }

        public ICollection<Game> Games { get; internal set; }
        public ICollection<MembershipReCharge> ReCharges { get; internal set; }

        public Membership()
        {
            Games = new List<Game>();
            ReCharges = new List<MembershipReCharge>();
            IssuedOn = DateTime.UtcNow.ToIST();
        }

        public void Recharge(MembershipReCharge membershipReCharge)
        {
            ReCharges.Add(membershipReCharge);
        }

        public void AddGame(Game game)
        {
            game.GamePaymentType = GamePaymentType.Membership;
            Games.Add(game);
        }

        public string BranchName { get; set; }

        public IEnumerable<MembershipReCharge> GetRecharges(string branchName, DateTime date)
        {
            return
                ReCharges
                .Where(recharge => recharge.BranchName == branchName && recharge.RechargedOn.Date == date.Date);
        }

        public IEnumerable<MembershipReCharge> GetRecharges(string branchName, DateTime from, DateTime to)
        {
            return
                ReCharges
                .Where(recharge => recharge.BranchName == branchName && recharge.RechargedOn.Date >= from.Date && recharge.RechargedOn.Date <= to.Date);
        }
    }
}