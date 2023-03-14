using AppTorneos.Data;
using AppTorneos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region PROCEDURES
//CREATE OR ALTER PROCEDURE INSERTAREQUIPO_SP(@Nombre nvarchar(50), @Jugador1 int, @Jugador2 int, @Jugador3 int)
//AS
// INSERT INTO EQUIPO VALUES((SELECT ISNULL(MAX(IdEquipo),0) FROM Equipo)+1, @Nombre, @Jugador1, @Jugador2, @Jugador3)
//GO

//CREATE OR ALTER PROCEDURE SELECTEQUIPOID_SP(@IDEQUIPO INT)
//AS
//	SELECT * FROM Equipo WHERE IdEquipo = @IDEQUIPO
//GO

//CREATE OR ALTER PROCEDURE EQUIPOSUSER_SP(@IDJUGADOR INT)
//AS
//	SELECT * FROM Equipo WHERE Jugador1 = @IDJUGADOR OR Jugador2 = @IDJUGADOR OR Jugador3 = @IDJUGADOR
//GO

//CREATE OR ALTER PROCEDURE DELETEEQUIPO_SP(@IDEQUIPO INT)
//AS
//    DELETE FROM Equipo WHERE IdEquipo = @IDEQUIPO
//GO

#endregion

namespace AppTorneos.Repositories
{
    public class RepositoryEquipos
    {
        private BSTournamentContext context;

        public RepositoryEquipos(BSTournamentContext context)
        {
            this.context = context;
        }

        public void InsertarEquipo(string nombre, int jugador1, int jugador2, int jugador3)
        {
            string sql = "INSERTAREQUIPO_SP @Nombre, @Jugador1, @Jugador2, @Jugador3";
            SqlParameter paminombre =
                new SqlParameter("@Nombre", nombre);
            SqlParameter pamjug1 = 
                new SqlParameter("@Jugador1", jugador1);
            SqlParameter pamjug2 =
                new SqlParameter("@Jugador2", jugador2);
            SqlParameter pamjug3 =
                new SqlParameter("@Jugador3", jugador3);

            this.context.Database.ExecuteSqlRaw(sql, paminombre, pamjug1, pamjug2, pamjug3);
        }

        public Equipo SelectEquipo(int idequipo)
        {
            string sql = "SELECTEQUIPOID_SP @IDEQUIPO";
            SqlParameter pamnombre =
                new SqlParameter("@IDEQUIPO", idequipo);
            var consulta = this.context.Equipos.FromSqlRaw(sql, pamnombre);
            Equipo equip = consulta.AsEnumerable().FirstOrDefault();
            return equip;
        }

        public List<Equipo> SelectAllEquipos(int idusuario) 
        {
            string sql = "EQUIPOSUSER_SP @IDJUGADOR";
            SqlParameter pamidusuario =
                new SqlParameter("@IDJUGADOR", idusuario);
            var consulta = this.context.Equipos.FromSqlRaw(sql, pamidusuario);
            List<Equipo> equipos = consulta.AsEnumerable().ToList();
            return equipos;
        }

        public void DeleteEquipos(int idequipo)
        {
            string sql = "DELETEEQUIPO_SP @IDEQUIPO";
            SqlParameter pamidequipo =
                new SqlParameter("@IDEQUIPO", idequipo);
            this.context.Database.ExecuteSqlRaw(sql, pamidequipo);
        }
    }
}
