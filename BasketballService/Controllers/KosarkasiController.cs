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
    [Authorize]
    public class KosarkasiController : ApiController
    {
        private IKosarkasRepository _kosarkasRepository { get; set; }
        public KosarkasiController(IKosarkasRepository kosarkasRepository)
        {
            _kosarkasRepository = kosarkasRepository;
        }
        [AllowAnonymous]
        public IEnumerable<Kosarkas> Get()
        {
            return _kosarkasRepository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var kosarkas = _kosarkasRepository.GetById(id);
            if (kosarkas == null)
            {
                return NotFound();
            }
            return Ok(kosarkas);
        }
        public IHttpActionResult Post(Kosarkas kosarkas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _kosarkasRepository.Add(kosarkas);
            return CreatedAtRoute("DefaultApi", new { id = kosarkas.Id }, kosarkas);
        }

        public IHttpActionResult Put(int id, Kosarkas kosarkas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kosarkas.Id)
            {
                return BadRequest();
            }

            try
            {
                _kosarkasRepository.Update(kosarkas);
            }
            catch
            {
                throw;
            }

            return Ok(kosarkas);
        }

        public IEnumerable<Kosarkas> GetByYear(int godine)
        {
            return _kosarkasRepository.GetByYear(godine);
        }

        public IHttpActionResult Delete(int id)
        {
            var kosarkas = _kosarkasRepository.GetById(id);
            if (kosarkas == null)
            {
                return NotFound();
            }

            _kosarkasRepository.Delete(kosarkas);
            return Ok();
        }

        [HttpPost]
        [Route("api/pretraga")]
        public IEnumerable<Kosarkas> PostSearchByGames(KosarkasPretragaDTO kosarkasPretragaDTO)
        {
            return _kosarkasRepository.SearchByGames(kosarkasPretragaDTO.NajmanjiBrUtakmica, kosarkasPretragaDTO.NajveciBrojUtakmica);
        }
    }
}
