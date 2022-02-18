using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_management_REST_API.Models
{
    [Table("students")]
    public class Student: User
    {  
        [RegularExpression("@^[A-Z]{1}[0-9]{9}$",ErrorMessage ="Student reference must match the expression AXXXXXXXXX")]
        public string StdRef { get; set; }
          
        [Required, StringLength(4), Display(Name = "Starting Year")]
        public int StartingYear { get; set; }

        public int Level => (DateTime.Now.Year - StartingYear)+1;
    }
 }
