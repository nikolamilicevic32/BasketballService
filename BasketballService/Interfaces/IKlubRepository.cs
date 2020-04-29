using BasketballService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballService.Interfaces
{
    public interface IKlubRepository
    {
        IEnumerable<KosarkaskiKlub> GetAll();
        KosarkaskiKlub GetById(int id);
        IEnumerable<KosarkaskiKlub> GetByThrophies();
    }
}
