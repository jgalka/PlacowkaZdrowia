using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.ViewModels
{
    public class RejestracjaDateGroup
    {
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataRejestracji { get; set; }

        public int PacjentCount { get; set; }
    }
}