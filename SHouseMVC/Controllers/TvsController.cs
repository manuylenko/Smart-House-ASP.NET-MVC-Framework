using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SHouseMVC
{
    public class TvsController : ApiController
    {
        private DevicesContext db = new DevicesContext();

        // GET: api/Tvs
        public IEnumerable<Tv> GetTvs()
        {
            return db.Tvs;
        }

        // GET: api/Tvs/5
        [ResponseType(typeof(Tv))]
        public IHttpActionResult GetTv(int id)
        {
            Tv tv = db.Tvs.Find(id);
            if (tv == null)
            {
                return NotFound();
            }

            return Ok(tv);
        }

        // PUT: api/Tvs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTv(int id, Tv tv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tv.Id)
            {
                return BadRequest();
            }
            db.Entry(tv).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TvExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tvs
        [ResponseType(typeof(Tv))]
        public IHttpActionResult PostTv(Tv tv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tvs.Add(tv);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tv.Id }, tv);
        }

        // DELETE: api/Tvs/5
        [ResponseType(typeof(Tv))]
        public IHttpActionResult DeleteTv(int id)
        {
            Tv tv = db.Tvs.Find(id);
            if (tv == null)
            {
                return NotFound();
            }

            db.Tvs.Remove(tv);
            db.SaveChanges();

            return Ok(tv);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TvExists(int id)
        {
            return db.Tvs.Count(e => e.Id == id) > 0;
        }
    }
}