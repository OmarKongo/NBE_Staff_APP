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
    public class PostsController : ApiController
    {
        private appEntities db;

        // GET: api/Posts
        public IQueryable<Post> GetPosts()
        {
            db = new appEntities();
            try
            {
                return db.Posts;
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

        // GET: api/Posts/5
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetPost(int id)
        {
            db = new appEntities();
            try
            {
                Post post = await db.Posts.FindAsync(id);
                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
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

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPost(int id, Post post)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != post.post_id)
                {
                    return BadRequest();
                }

                db.Entry(post).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(id))
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

        // POST: api/Posts
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(Post post)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Posts.Add(post);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = post.post_id }, post);
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

        // DELETE: api/Posts/5
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> DeletePost(int id)
        {
            db = new appEntities();
            try
            {
                Post post = await db.Posts.FindAsync(id);
                if (post == null)
                {
                    return NotFound();
                }

                db.Posts.Remove(post);
                await db.SaveChangesAsync();

                return Ok(post);
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

        private bool PostExists(int id)
        {
            db = new appEntities();
            try
            {
                return db.Posts.Count(e => e.post_id == id) > 0;
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