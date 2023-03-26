using AppTorneos.Data;
using AppTorneos.Models;

namespace AppTorneos.Repositories
{
    public class RepositoryLigas
    {
        private BSTournamentContext context;

        public RepositoryLigas(BSTournamentContext context)
        {
            this.context = context;
        }

        public List<Liga> GetLigas()
        {
            return this.context.Ligas.AsEnumerable().ToList();
        }

        public Liga GetLiga(int idliga)
        {
            return this.context.Ligas.Where(x => x.IdLiga == idliga).AsEnumerable().FirstOrDefault();
        }

        public List<Equipo> GetInfoEquiposLiga(int idliga)
        {
            var consulta = (from datos in this.context.EquiposLiga
                            where datos.IdLiga == idliga
                            select datos.IdEquipo).Distinct();

            List<int> idequipos = consulta.ToList();

            return this.context.Equipos.Where(x => idequipos.Contains(x.IdEquipo)).AsEnumerable().ToList();
        }

        public List<Liga> FiltrarLigaNombre(string nombre)
        {
            return this.context.Ligas.Where(x => x.Nombre.Contains(nombre)).AsEnumerable().ToList();
        }

        public async Task SolicitarAcceso(int idliga, int idequipo)
        {
            int id = 0;
            if (this.context.EquiposLiga.Count() == 0)
            {
                id = 1;
            }
            else
            {
                id = this.context.EquiposLiga.Max(x => x.Id) + 1;
            }

            EquipoLiga solicitante = new EquipoLiga();
            solicitante.Id = id;
            solicitante.IdLiga = idliga;
            solicitante.IdEquipo = idequipo;
            solicitante.Ganados = 0;
            solicitante.Perdidos = 0;
            solicitante.Empates = 0;
            solicitante.Inscrito = false;
            this.context.EquiposLiga.Add(solicitante);
            await this.context.SaveChangesAsync();
        }

        public async Task AccionAcceso(bool confirmado, int idinscrito)
        {
            EquipoLiga equipo = this.context.EquiposLiga.Where(x => x.Id == idinscrito).AsEnumerable().FirstOrDefault();
            
            if (confirmado == true)
            {
                equipo.Inscrito = true;
            }
            else
            {
                this.context.EquiposLiga.Remove(equipo);
            }

            await this.context.SaveChangesAsync();
        }

        public List<EquipoLiga> GetEquiposXLiga(int idliga)
        {
            return this.context.EquiposLiga.Where(x => x.IdLiga == idliga).AsEnumerable().ToList();
        }

        public async Task CrearLiga(string nombre, int idusuario, int idequipo)
        {
            int id = 0;
            if(this.context.Ligas.Count() == 0)
            {
                id = 1;
            }
            else
            {
                id = this.context.Ligas.Max(x => x.IdLiga) + 1;
            }

            Liga liga = new Liga();
            liga.IdLiga = id;
            liga.Nombre = nombre;
            liga.FechaInicio = null;
            liga.FechaFin = null;
            liga.Estado = -1;
            liga.Creador = idusuario;
            this.context.Ligas.Add(liga);

            await this.context.SaveChangesAsync();
            await this.AniadirEquipoDuenoLiga(id, idequipo);
        }

        public async Task AniadirEquipoDuenoLiga(int idliga, int idequipo)
        {
            int id = 0;
            if (this.context.EquiposLiga.Count() == 0)
            {
                id = 1;
            }
            else
            {
                id = this.context.EquiposLiga.Max(x => x.Id) + 1;
            }

            EquipoLiga solicitante = new EquipoLiga();
            solicitante.Id = id;
            solicitante.IdLiga = idliga;
            solicitante.IdEquipo = idequipo;
            solicitante.Ganados = 0;
            solicitante.Perdidos = 0;
            solicitante.Empates = 0;
            solicitante.Inscrito = true;
            this.context.EquiposLiga.Add(solicitante);

            await this.context.SaveChangesAsync();
        }

        public List<EquipoLiga> GetEquipoLigasApuntadas(int idequipo)
        {
            var consulta =  from datos in this.context.EquiposLiga
                            where datos.IdEquipo == idequipo
                            select datos;

            return consulta.ToList();
        }

        public async Task EmpezarLiga(int idliga, DateTime fechainicio)
        {
            Liga liga = this.context.Ligas.Where(x => x.IdLiga == idliga).AsEnumerable().FirstOrDefault();
            liga.FechaInicio = fechainicio;
            liga.Estado = 0;

            //METODO PARA GENERAR LAS PARTIDAS DE TODOS LOS EQUIPOS
            await this.context.SaveChangesAsync();
        }


    }
}
