using System;
using System.Collections.Generic;
using PlacowkaZdrowia.Models;

namespace PlacowkaZdrowia.ViewModels
{
    public class LekarzIndexData
    {
        public IEnumerable<Lekarz> Lekarze { get; set; }
        public IEnumerable<Zabieg> Zabiegi { get; set; }
        public IEnumerable<Rejestracja> Rejestracje { get; set; }
    }
}