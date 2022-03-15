using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Izvodjac
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; }

        [MaxLength(50)]
        public string Prezime { get; set; }

        [JsonIgnore]
        public virtual List<Vinyl> ploce { get; set; }


        
    }
    
}