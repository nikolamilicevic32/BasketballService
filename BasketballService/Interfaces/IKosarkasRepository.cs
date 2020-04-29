using BasketballService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballService.Interfaces
{
    public interface IKosarkasRepository
    {
        IEnumerable<Kosarkas> GetAll();
        Kosarkas GetById(int id);
        void Add(Kosarkas kosarkas);
        void Delete(Kosarkas kosarkas);
        void Update(Kosarkas kosarkas);
        IEnumerable<Kosarkas> GetByYear(int godine);
        IEnumerable<Kosarkas> SearchByGames(int najmanje, int najvise);
    }
}
