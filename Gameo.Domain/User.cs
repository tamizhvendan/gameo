using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class User : Entity, IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string BranchName { get; set; }
        public bool IsAdministrator { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsAdministrator && string.IsNullOrEmpty(BranchName))
            {
                yield return new ValidationResult("Branch Name field is required for a non-admin user");
            }
        }
    }
}