using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int OsobaID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Lekarz Lekarz { get; set; }
    }
}

