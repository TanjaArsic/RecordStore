using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Prodavac
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; }

        [MaxLength(50)]
        public string Prezime { get; set; }
        
        [MaxLength(10)]
        public string BrojTelefona { get; set; }

        public int LicnaKarta { get; set; }

        public Smena RadnoVreme { get; set; }

        public Prodavnica prodavnica { get; set; }




    }
}