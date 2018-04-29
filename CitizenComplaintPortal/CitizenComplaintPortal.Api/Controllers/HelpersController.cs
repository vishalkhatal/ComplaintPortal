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
using CitizensComplaintPortal.DataAccess;
using CitizensComplaintPortal.Models;

namespace CitizenComplaintPortal.Api.Controllers
{
    public class HelpersController : ApiController
    {
        private CitizensCompaintPortalContext db = new CitizensCompaintPortalContext();

        // GET: api/Helpers
        public List<User> GetHelpers()
        {
            return db.users.Where(x => x.role == Roles.Helper).ToList();
        }
        public User Login(User loginData)
        {
            var user= db.users.Where(x => x.EmailAddress== loginData.EmailAddress && x.password== loginData.password).FirstOrDefault();
            return user;
        }
        public List<Complaint> GetAssignedComplaints(int id)
        {
            try
            {
                if (id ==0)
                {
                    return null;
                }
                return db.Complaints.Where(x => x.UserId == id && x.complaintStatus!=ComplaintStatus.Unassinged).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: api/Helpers/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetHelper(int id)
        {
            User user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Complaints/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateComplaint(ComplaintHelperMapping complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = db.Complaints.Where(x => x.ComplaintId == complaint.ComplaintId).FirstOrDefault();
            data.complaintStatus = complaint.complaintStatus;
            db.Entry(data).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // PUT: api/Helpers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

        // POST: api/Helpers
        [ResponseType(typeof(User))]
        public IHttpActionResult Add(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        //// DELETE: api/Helpers/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult DeleteHelper(int id)
        //{
        //    User user = db.users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.users.Remove(user);
        //    db.SaveChanges();

        //    return Ok(user);
        //}

        // PUT: api/Helpers/Assign
        [ResponseType(typeof(void))]
        public IHttpActionResult Assign(ComplaintHelperMapping assingnment)
        {
            if (assingnment == null)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var complaint = db.Complaints.Where(x => x.ComplaintId == assingnment.ComplaintId).FirstOrDefault();
                if (complaint == null)
                    return BadRequest();
                complaint.HelperName = assingnment.HelperName;
                complaint.UserId = assingnment.UserId;

                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.OK);
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
            return db.users.Count(e => e.UserId == id) > 0;
        }

    }
}