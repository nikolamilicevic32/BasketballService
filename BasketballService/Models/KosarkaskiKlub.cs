using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballService.Models
{
    public class KosarkaskiKlub
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Naziv { get; set; }
        [MaxLength(3)]
        [MinLength(3)]
        public string Liga { get; set; }
        [Range(1945,1999)]
        public int GodinaOsnivanja { get; set; }
        [Range(0,19)]
        public int? BrojTrofeja { get; set; }
    }
}