using Gameo.Domain;

namespace Gameo.Web.ViewModels
{
    public class MembershipAssignConsoleViewModel
    {
        public Game Game { get; set; }
        public Membership Membership { get; set; }

        public MembershipAssignConsoleViewModel()
        {
            Game = new Game();
            Membership = new Membership();
        }
    }
}