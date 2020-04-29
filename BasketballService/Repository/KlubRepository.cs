using BasketballService.Interfaces;
using BasketballService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballService.Repository
{
    public class KlubRepository : IKlubRepository, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<KosarkaskiKlub> GetAll()
        {
            return db.Klubovi;
        }

        public KosarkaskiKlub GetById(int id)
        {
            return db.Klubovi.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<KosarkaskiKlub> GetByThrophies()
        {
            IEnumerable<KosarkaskiKlub> lista = GetAll();
            List<KosarkaskiKlub> novaLista = new List<KosarkaskiKlub>();
            var max = lista.OrderBy(x => x.BrojTrofeja ?? int.MaxValue).First();
            var min = lista.OrderBy(x => x.BrojTrofeja ?? int.MinValue).Last();

            novaLista.Add(max);
            novaLista.Add(min);
            novaLista.OrderBy(x => x.BrojTrofeja);

            return novaLista;

        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}