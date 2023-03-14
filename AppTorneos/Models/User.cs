using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTorneos.Models
{
    [Table("Usuario")]
    public class User
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("UsuarioTag")]
        public string UsuarioTag { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Contrasenia")]
        public string Contrasenia { get; set; }
    }
}
