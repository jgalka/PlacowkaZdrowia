using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.Models
{
    public class Pacjent : Osoba
    {
        [Required(ErrorMessage = "Data rejestracji jest wymagana.")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data rejestracji (yyyy-mm-dd)")]
        public DateTime? DataRejestracji { get; set; }

        public virtual ICollection<Rejestracja> Rejestracje { get; set; }
    }
}