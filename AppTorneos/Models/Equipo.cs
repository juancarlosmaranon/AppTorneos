using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTorneos.Models
{
    [Table("Equipo")]
    public class Equipo
    {
        [Key]
        [Column("IdEquipo")]
        public int IdEquipo { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Jugador1")]
        public int Jugador1 { get; set; }

        [Column("Jugador2")]
        public int Jugador2 { get; set; }

        [Column("Jugador3")]
        public int Jugador3 { get; set; }
    }
}
