using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class Branch : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}