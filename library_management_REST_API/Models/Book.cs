using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Author { get; set; }

        [DataType(DataType.Currency)]
        public double prix { get; set; }
         
        [DataType(DataType.ImageUrl)]
        public string imgUrl { get; set; }
    }
}
