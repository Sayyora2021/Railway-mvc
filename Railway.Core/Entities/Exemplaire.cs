using System.ComponentModel.DataAnnotations;

namespace Railway.Core.Entities
{
    public class Exemplaire : Entity
    {

        public Buillet Buillet { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string NumeroInventaire { get; set; } = null!;

        public DateTime? MiseEnService { get; set; }

    }
}
