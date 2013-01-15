using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.ViewModels
{
    public class AssignedZabiegData
    {
        public int ZabiegID { get; set; }
        public string Tytul { get; set; }
        public bool Assigned { get; set; }
    }
}