using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System;

namespace Models
{
    public enum TipVinyl
    {   
        Nova,
        Polovna
    }
    
    [Table("Vinyl")]
     public class Vinyl
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }
       
        public TipVinyl Tip { get; set; }

        public Zanr Zanr{ get; set; }

        public string Pesme { get; set; }
         
        public int GodinaStampanja { get; set;  }

        [JsonIgnore]
        public Izvodjac izvodjac { get; set; }

        [JsonIgnore]
        public IzdavackaKuca izdavackaKuca { get; set; }

        [JsonIgnore]
        public Prodavnica prodavnica { get; set; }
    }
}