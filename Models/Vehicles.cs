using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestesAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Brand { get; set; }

        [Required]
        [StringLength(100)]
        public string? Model { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$|^[A-Z]{3}\d{4}$", ErrorMessage = "Invalid format")] // AAA1A11 or AAA1234
        public string? Plate { get; set; }

        public int ClientId { get; set; }

        public Client? Client { get; set; }
    }
}