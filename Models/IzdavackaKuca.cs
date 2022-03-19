using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;


namespace Models
{
    public class IzdavackaKuca
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; }

        [MaxLength(100)]
        public string Website { get; set; }

        [JsonIgnore]
        public virtual List<Vinyl> ploce { get; set; }


        
    }
    
}