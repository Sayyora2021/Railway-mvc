namespace Railway.Core.Entities
{
    public class Reservation : Entity
    {
        public Exemplaire Exemplaire { get; set; } = null!;
        public Passager Passager { get; set; } = null!;

        public DateTime? DateAller { get; set; }

        public DateTime? DateRetour { get; set; }

        public DateTime? DateModification { get; set; }
    }
}