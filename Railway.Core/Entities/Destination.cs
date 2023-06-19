using System.ComponentModel.DataAnnotations;

namespace Railway.Core.Entities
{
    public class Destination : Entity
    {
        [Required]
        [MaxLength(50)]
        public string? Aller { get; set; } 
        [Required]
        [MaxLength(500)]
        public string? Retour { get; set; }
        public virtual List<Buillet> Buillets { get; set; } = new List<Buillet>();
    }
}
