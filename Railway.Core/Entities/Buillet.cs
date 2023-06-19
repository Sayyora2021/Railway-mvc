using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway.Core.Entities
{
    public class Buillet : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Numero { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Titre { get; set; } = null!;

        public virtual List<Destination> Destinations { get; set; } = new List <Destination>();

        public virtual List<Exemplaire> Exemplaires { get; set; } = new List<Exemplaire>();
        public virtual List<Train> Trains { get; set; } = new List<Train>();
    }
}
