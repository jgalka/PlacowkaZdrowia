using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.Models
{
    public abstract class Osoba
    {
        [Key]
        public int OsobaID { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [Display(Name = "Nazwisko")]
        [MaxLength(50)]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [Column("Imię")]
        [Display(Name = "Imię")]
        [MaxLength(50)]
        public string Imie { get; set; }

        public string FullName
        {
            get
            {
                return Nazwisko + ", " + Imie;
            }
        }
    }
}