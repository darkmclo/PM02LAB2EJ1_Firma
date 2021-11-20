using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PM02LAB2EJ1.Models;
using SQLite;

namespace PM02LAB2EJ1.Controllers
{
    public class DatabaseSQLite
    {
        readonly SQLiteAsyncConnection db;

        public DatabaseSQLite(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Firmas>().Wait();
        }

        public Task<List<Firmas>> ObtenerListaFirmas()
        {
            return db.Table<Firmas>().ToListAsync();
        }

        public Task<Firmas> ObtenerFirmas(int pcodigo)
        {
            return db.Table<Firmas>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GrabarFirmas(Firmas firmas)
        {
            if (firmas.codigo != 0)
            {
                return db.UpdateAsync(firmas);
            }
            else
            {
                return db.InsertAsync(firmas);
            }

        }

        public Task<int> EliminarFirma(Firmas firmas)
        {
            return db.DeleteAsync(firmas);
        }
    }
}
