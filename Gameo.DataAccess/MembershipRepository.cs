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
    }
}