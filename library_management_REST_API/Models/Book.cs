using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace library_management_REST_API.Models
{
    [Table("books")]
    public class Book
    {
        [Key] 
        public int NoOfBooks { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author  { get; set; }

        [DataType(DataType.Currency)]
        public double prix { get; set; } 

        [Url]
        public string imgUrl { get; set; }
    }
 }
