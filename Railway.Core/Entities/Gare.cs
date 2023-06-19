using System.ComponentModel.DataAnnotations;

namespace Railway.Core.Entities
{
    public class Gare : Entity
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; } = null!;
    }
}
