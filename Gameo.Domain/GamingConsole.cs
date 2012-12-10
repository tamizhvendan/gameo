using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class GamingConsole : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string BranchName { get; set; }
        public Status Status { get; set; }

        public GamingConsole()
        {
            Status = Status.Working;
        }
    }
}