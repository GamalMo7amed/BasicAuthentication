using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BasicAuthentication.Models
{
        [Index(nameof(Email), Name = "Index_Email_Unique", IsUnique = true)]
        public class User
        {
            [Key]
            public int Id { get; set; }
            [Required]
            [StringLength(50)]
            public string FirstName { get; set; }
            [Required]
            [StringLength(50)]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; }
            [Required]
            [MinLength(6)]
            public string Password { get; set; }
        }
}
