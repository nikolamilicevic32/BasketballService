using BasketballService.Interfaces;
using BasketballService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasketballService.Controllers
{
    public class KluboviController : ApiController
    {
        private IKlubRepository _klubRepository { get; set; }
        public KluboviController(IKlubRepository klubRepository)
        {
            _klubRepository = klubRepository;
        }

        public IEnumerable<KosarkaskiKlub> Get()
        {
            return _klubRepository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var mesto = _klubRepository.GetById(id);
            if (mesto == null)
            {
                return NotFound();
            }
            return Ok(mesto);
        }
        [Route("api/ekstremi")]
        public IEnumerable<KosarkaskiKlub> GetByThrophies()
        {
            return _klubRepository.GetByThrophies();
        }
    }
}
