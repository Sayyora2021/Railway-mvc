namespace Railway.Core.Entities
{
    public class Train: Entity
    {

        //[Required]
        //[MaxLength(100)]
        public string Numero { get; set; } = null!;
        //[Required]
        //[MaxLength(100)]
        public string Nom { get; set; } = null!;
        public virtual List<Buillet> Buillets { get; set; } = new List<Buillet>();
    }
}
