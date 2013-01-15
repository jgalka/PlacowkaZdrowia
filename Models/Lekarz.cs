using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.Models
{
    public class Lekarz : Osoba
    {
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Data zatrudnienia jest wymagana.")]
        [Display(Name = "Data zatrudnienia")]
        public DateTime? HireDate { get; set; }

        public virtual ICollection<Zabieg> Zabiegi { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}