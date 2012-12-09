using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class Branch : Entity
    {
        [Required]
        public string Name { get; set; }

        public bool IsOperating { get; set; }

        public Branch()
        {
            IsOperating = true;
        }
    }
}