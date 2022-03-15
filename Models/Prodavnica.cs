using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System;


namespace Models
{
    public class Prodavnica
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Naziv { get; set; }

        [MaxLength(100)]
        public string Adresa { get; set; }

        [MaxLength(100)]
        public string Mail { get; set; }

        public int BrVinyl { get; set; }

        [JsonIgnore]
        public virtual List<Prodavac> prodavci { get; set; }
    
        [JsonIgnore]
        public List<Vinyl> ploce { get; set; }

        
    }
    
}