using System;
using System.Collections.Generic;
using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IMembershipRepository : IRepository<Membership>
    {
        bool IsCustomer1ContactNumberExists(string contactNumber);
        Membership FindByMembershipId(string membershipId);
        Membership FindByCustomer1ContactNumber(string customer1ContactNumber);
        void Recharge(string membershipId, MembershipReCharge membershipReCharge);
        IEnumerable<MembershipReCharge> GetRecharges(string branchName, DateTime dateTime);
    }
}