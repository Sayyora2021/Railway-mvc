using System.ComponentModel.DataAnnotations;

namespace Railway.Core.Entities
{
    public class Passager : Entity
    {
        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; } = null!;
        [Required]
        [MaxLength(150)]
        public string Adresse { get; set; } = null!;

        public List<Reservation> ListReservation { get; set; } = new List<Reservation>();

        public bool Cotisation { get; set; }

    }
}
