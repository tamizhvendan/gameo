using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class GameConsole : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string BranchName { get; set; }
        public GameConsoleStatus GameConsoleStatus { get; set; }

        public GameConsole()
        {
            GameConsoleStatus = GameConsoleStatus.Working;
            ;
        }
    }
}