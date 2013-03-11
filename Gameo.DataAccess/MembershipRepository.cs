using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess
{
    public class MembershipRepository : RepositoryBase<Membership>, IMembershipRepository
    {
        public bool IsCustomer1ContactNumberExists(string contactNumber)
        {
            return FindByCustomer1ContactNumber(contactNumber) != null;
        }

        public Membership FindByMembershipId(string membershipId)
        {
            return EntityCollection.AsQueryable().FirstOrDefault(membership => membership.MembershipId == membershipId);
        }

        public Membership FindByCustomer1ContactNumber(string customer1ContactNumber)
        {
            return EntityCollection.AsQueryable().FirstOrDefault(membership => membership.Customer1ContactNumber == customer1ContactNumber);
        }

        public void Recharge(MembershipReCharge membershipReCharge)
        {
            var membership = FindByMembershipId(membershipReCharge.MembershipId);
            membership.Recharge(membershipReCharge);
            EntityCollection.Save(membership);
        }

        public IEnumerable<MembershipReCharge> GetRecharges(string branchName, DateTime dateTime)
        {
            var membershipRecharges = new List<MembershipReCharge>();

            var recharges = EntityCollection.AsQueryable().Select(membership => membership.GetRecharges(branchName, dateTime));

            foreach (var recharge in recharges)
            {
                membershipRecharges.AddRange(recharge);
            }

            return membershipRecharges;
        }
    }
}