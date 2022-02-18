using System.ComponentModel.DataAnnotations.Schema;

namespace library_management_REST_API.Models
{
    [Table("staff")]
    public class Staff : User
    {
        public string StaffRef { get; set; }
    }
 }
