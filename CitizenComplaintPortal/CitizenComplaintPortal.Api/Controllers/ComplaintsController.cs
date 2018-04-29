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
    public class ComplaintsController : ApiController
    {
        private CitizensCompaintPortalContext db = new CitizensCompaintPortalContext();

        // GET: api/Complaints
        public List<Complaint> GetComplaints(ComplaintStatus status)
        {
            try
            {
                if (status == ComplaintStatus.All)
                {
                    var x = db.Complaints.ToList();
                    return x;

                }
                return db.Complaints.Where(x => x.complaintStatus == status).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult Get(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        // PUT: api/Complaints/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(UpdateComplaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = db.Complaints.Where(x => x.ComplaintId == complaint.ComplaintId).FirstOrDefault();
            data.complaintStatus = complaint.ComplaintStatus;
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

        // POST: api/Complaints
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult Save(Complaint complaint)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                complaint.CreatedDate = DateTime.Now;
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return Ok(complaint.ComplaintId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // DELETE: api/Complaints/5
        //[ResponseType(typeof(Complaint))]
        //public IHttpActionResult DeleteComplaint(int id)
        //{
        //    Complaint complaint = db.Complaints.Find(id);
        //    if (complaint == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Complaints.Remove(complaint);
        //    db.SaveChanges();

        //    return Ok(complaint);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintExists(int id)
        {
            return db.Complaints.Count(e => e.ComplaintId == id) > 0;
        }

    }
}