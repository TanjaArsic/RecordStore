using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class SpojProdavnicaPloca
        {
            [Key]
            public int ID { get; set; }

            [Required]
            public int Cena { get; set; }

            public int Kolicina { get; set; }

            [JsonIgnore]
            public Prodavnica prodavnica {get; set; }

            [JsonIgnore]
            public Vinyl ploca {get; set; }

        }
}