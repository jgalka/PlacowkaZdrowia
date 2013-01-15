using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.Models
{
    public class Zabieg
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Numer")]
        public int ZabiegID { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(50)]
        public string Tytul { get; set; }

        [Required(ErrorMessage = "Kwota kosztu jest wymagana w pełnych złotych.")]
        [Range(0, 200, ErrorMessage = "Kwota musi sie mieścic między 0 a 200.")]
        public int Koszty { get; set; }

        [Display(Name = "Dział")]
        public int DzialID { get; set; }

        public virtual Dzial Dzial { get; set; }
        public virtual ICollection<Rejestracja> Rejestracje { get; set; }
        public virtual ICollection<Lekarz> Lekarze { get; set; }
    }
}