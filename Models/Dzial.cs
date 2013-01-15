using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaZdrowia.Models
{
    public class Dzial
    {
        public int DzialID { get; set; }

        [Required(ErrorMessage = "Wymagana nazwa działu.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        [Required(ErrorMessage = "Wymagana kwota budgetu.")]
        [Column(TypeName = "money")]
        public decimal? Budget { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Administrator")]
        public int? OsobaID { get; set; }
        
        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Lekarz Administrator { get; set; }
        public virtual ICollection<Zabieg> Zabiegi { get; set; }
    }
}