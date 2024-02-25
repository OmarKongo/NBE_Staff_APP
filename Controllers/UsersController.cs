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
    public class UsersController : ApiController
    {
        private appEntities db;

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            db = new appEntities();
            try
            {
                return db.Users;
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

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            db = new appEntities();
            try
            {
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
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

        // GET: api/Users/5

        [ResponseType(typeof(User))]
        [HttpGet]
        [Route("api/usersNamePass")]
        public async Task<IHttpActionResult> GetUserNamePassword(string userName,string password)
        {
            db = new appEntities();
            try
            {
                User user = db.Users.Where(x=>x.user_name==userName && x.password==password && x.isDeleted!=true).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
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
        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != user.user_id)
                {
                    return BadRequest();
                }

                db.Entry(user).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            db = new appEntities();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Users.Add(user);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = user.user_id }, user);
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

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            db = new appEntities();
            try
            {
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                db.Users.Remove(user);
                await db.SaveChangesAsync();

                return Ok(user);
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

        private bool UserExists(int id)
        {
            db = new appEntities();
            try
            {
                return db.Users.Count(e => e.user_id == id) > 0;
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