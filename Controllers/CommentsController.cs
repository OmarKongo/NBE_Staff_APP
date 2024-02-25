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
    public class CommentsController : ApiController
    {
        private appEntities db;

        // GET: api/Comments
        public IQueryable<Comment> GetComments()
        {
            db = new appEntities();
            try
            {
                return db.Comments;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                db.Dispose();
            }

        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(int id)
        {
            db = new appEntities();
            try
            {
                Comment comment = await db.Comments.FindAsync(id);
                if (comment == null)
                {
                    return NotFound();
                }

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                db.Dispose();
            }


        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComment(int id, Comment comment)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != comment.comment_id)
                {
                    return BadRequest();
                }

                db.Entry(comment).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(id))
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
                return null;
            }
            finally
            {
                db.Dispose();
            }

        }

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(Comment comment)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Comments.Add(comment);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = comment.comment_id }, comment);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                db.Dispose();
            }

        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            db = new appEntities();
            try
            {
                Comment comment = await db.Comments.FindAsync(id);
                if (comment == null)
                {
                    return NotFound();
                }

                db.Comments.Remove(comment);
                await db.SaveChangesAsync();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return null;
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

        private bool CommentExists(int id)
        {
            db = new appEntities();
            try
            {
                return db.Comments.Count(e => e.comment_id == id) > 0;
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