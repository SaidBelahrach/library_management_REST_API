using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace library_management_REST_API.Models
{
    [Table("users")]
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
