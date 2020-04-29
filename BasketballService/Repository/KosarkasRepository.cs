using BasketballService.Interfaces;
using BasketballService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BasketballService.Repository
{
    public class KosarkasRepository : IKosarkasRepository, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Add(Kosarkas kosarkas)
        {
            db.Kosarkasi.Add(kosarkas);
            db.SaveChanges();
        }

        public void Delete(Kosarkas kosarkas)
        {
            db.Kosarkasi.Remove(kosarkas);
            db.SaveChanges();
        }

        public IEnumerable<Kosarkas> GetAll()
        {
            return db.Kosarkasi.Include(x => x.KosarkaskiKlub);
        }

        public Kosarkas GetById(int id)
        {
            return db.Kosarkasi.Include(x => x.KosarkaskiKlub).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Kosarkas> GetByYear(int godine)
        {
            return db.Kosarkasi.Include(x => x.KosarkaskiKlub).Where(x => x.GodinaRodjenja > godine).OrderBy(x => x.GodinaRodjenja);
        }

        public IEnumerable<Kosarkas> SearchByGames(int najmanje, int najvise)
        {
            return db.Kosarkasi.Include(x => x.KosarkaskiKlub).Where(x => x.BrojUtakmica > najmanje && x.BrojUtakmica < najvise);
        }

        public void Update(Kosarkas kosarkas)
        {
            db.Entry(kosarkas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
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