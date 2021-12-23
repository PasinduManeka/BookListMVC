using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        //can't be null
        [Required]
        public String Name { get; set; }
        [Required]
        public String Author { get; set; }
        [Required]
        public String ISBN { get; set; }


    }
}
