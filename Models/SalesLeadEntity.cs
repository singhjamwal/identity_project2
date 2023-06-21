using System.ComponentModel.DataAnnotations;

namespace identity_project2.Models
{
    public class SalesLeadEntity
    {
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }


        public string Source { get; set; }

    }
}
