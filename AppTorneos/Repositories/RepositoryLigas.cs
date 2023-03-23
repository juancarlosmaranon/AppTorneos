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

        public List<Liga> FiltrarLigaNombre(string nombre)
        {
            return this.context.Ligas.Where(x => x.Nombre == nombre).AsEnumerable().ToList();
        }

        public async Task CrearLiga(string nombre)
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
            this.context.Ligas.Add(liga);
            await this.context.SaveChangesAsync();
        }
    }
}
