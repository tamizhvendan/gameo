using Gameo.Domain;

namespace Gameo.Web.ViewModels
{
    public class MembershipDetaiRequestViewModel
    {
        public string Customer1ContactNumber { get; set; }
        public string MembershipId { get; set; }
        public Membership Membership { get; set; }
    }
}