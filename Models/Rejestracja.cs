using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.Models
{
    public class Rejestracja
    {
        public int RejestracjaID { get; set; }

        public int ZabiegID { get; set; }

        public int OsobaID { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.#}", ApplyFormatInEditMode = true, NullDisplayText = "Brak typu")]
        public decimal? Typ { get; set; }

        public virtual Zabieg Zabieg { get; set; }
        public virtual Pacjent Pacjent { get; set; }
    }
}