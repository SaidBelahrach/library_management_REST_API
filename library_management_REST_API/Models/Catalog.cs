using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_management_REST_API.Models
{
    [Table("catalogs")]
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NbCopies { get; set; }

        public List<Book> Books { get; set; }

    }
}
