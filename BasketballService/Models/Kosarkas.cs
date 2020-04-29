using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballService.Models
{
    public class Kosarkas
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string ImePrezime { get; set; }
        [Range(1976,1999)]
        public int GodinaRodjenja { get; set; }
        [Range(1,int.MaxValue)]
        public int BrojUtakmica { get; set; }
        [Range(0.01,29.99)]
        public double ProsecanBrPoena { get; set; }
        public int KosarkaskiKlubId { get; set; }
        public KosarkaskiKlub KosarkaskiKlub { get; set; }
    }
}