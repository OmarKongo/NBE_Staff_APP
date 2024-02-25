using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API_SocialAPP.Models;

namespace API_SocialAPP.Controllers
{
    public class LikesController : ApiController
    {
        private appEntities db;

        // GET: api/Likes
        public IQueryable<Like> GetLikes()
        {
             db= new appEntities();
            try
            {
                return db.Likes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
            
        }

        // GET: api/Likes/5
        [ResponseType(typeof(Like))]
        public async Task<IHttpActionResult> GetLike(int id)
        {
            db = new appEntities();
            try
            {
                Like like = await db.Likes.FindAsync(id);
                if (like == null)
                {
                    return NotFound();
                }

                return Ok(like);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            finally
            {
                db.Dispose();
            }
        }

        // PUT: api/Likes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLike(int id, Like like)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != like.like_id)
                {
                    return BadRequest();
                }

                db.Entry(like).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikeExists(id))
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

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        // POST: api/Likes
        [ResponseType(typeof(Like))]
        public async Task<IHttpActionResult> PostLike(Like like)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Likes.Add(like);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = like.like_id }, like);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }

        }

        // DELETE: api/Likes/5
        [ResponseType(typeof(Like))]
        public async Task<IHttpActionResult> DeleteLike(int id)
        {
            db = new appEntities();
            try
            {
                Like like = await db.Likes.FindAsync(id);
                if (like == null)
                {
                    return NotFound();
                }

                db.Likes.Remove(like);
                await db.SaveChangesAsync();

                return Ok(like);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LikeExists(int id)
        {
            db = new appEntities();
            try
            {
                return db.Likes.Count(e => e.like_id == id) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }

        }
    }
}