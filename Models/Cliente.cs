using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TestesAPI.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}