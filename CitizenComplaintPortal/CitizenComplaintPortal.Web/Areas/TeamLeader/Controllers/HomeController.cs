using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CitizensComplaintPortal.Models;
using Newtonsoft.Json;
using System.Net.Mail;

namespace CitizenComplaintPortal.Web.Areas.TeamLeader.Controllers
{
    public class HomeController : Controller
    {
        // GET: ViewComplaints
        public async Task<ActionResult> Index()
        {
            List<Complaint> testList = new List<Complaint>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://citizencomplaintportal.azurewebsites.net/api/Complaints/GetComplaints?status=7");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var complaints = JsonConvert.DeserializeObject<List<Complaint>>(data);
                    testList = complaints;
                    var t = TempData["SuccessMessage"];
                    if(t != null)
                    ViewBag.SuccessMessage = t;
                }

            }
            return View(testList);
        }

        public ActionResult RaiseComplaint()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RaiseComplaint(Complaint complaint)
        {
            using (var client = new HttpClient())
            {
                string complaintData = JsonConvert.SerializeObject(complaint);
                var response = await client.PostAsync("http://citizencomplaintportal.azurewebsites.net/api/Complaints/Save", new StringContent(complaintData, Encoding.UTF8, "text/json"));
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessStatus"] = string.Format("Your issue has been successfully registered. Your issue id is {0}", responseData);
                    }
                }

            }
            return View("RaiseComplaint");
        }
        public async Task<ActionResult> GetHelpers()
        {
            List<User> userList = new List<User>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://citizencomplaintportal.azurewebsites.net/api/helpers/GetHelpers");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(data);
                }

            }
            return View(userList);
        }
        public async Task<ActionResult> AssignHelper(ComplaintHelperMapping assignment)
        {
            using (var client = new HttpClient())
            {
                string assignmentData = JsonConvert.SerializeObject(assignment);
                var response = await client.PostAsync("http://citizencomplaintportal.azurewebsites.net/api/helper/assign", new StringContent(assignmentData, Encoding.UTF8, "text/json"));
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {

                    }
                }

            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<JsonResult> AssignComplaintStatus(string Options)
        {
            var result = "";
            UpdateComplaint ob = new UpdateComplaint();
            ob.ComplaintId = Convert.ToInt32(Options.Split('#')[0]);
            ob.ComplaintStatus = (ComplaintStatus)Enum.Parse(typeof(ComplaintStatus), Options.Split('#')[1]);
            using (var client = new HttpClient())
            {
                string assignmentData = JsonConvert.SerializeObject(ob);
                var response = await client.PostAsync("http://citizencomplaintportal.azurewebsites.net/api/complaints/update", new StringContent(assignmentData, Encoding.UTF8, "text/json"));
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var responseData = response.Content.ReadAsStringAsync().Result;
                   
                    if(ob.ComplaintStatus == ComplaintStatus.Assinged)
                    {
                        MailMessage mail = new MailMessage("priyankasupplier24@gmail.com", "mehmet.aras@microsoft.com");
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new System.Net.NetworkCredential("priyankasupplier24@gmail.com", "Priya@24");
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //smtpClient.UseDefaultCredentials = false;
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.EnableSsl = true;
                        mail.Subject = "New Issue assigned with issue id: " + ob.ComplaintId;
                        mail.Body = "A new issue is assigned to you. Please look into this.";
                        try
                        {
                            smtpClient.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            result = "0";
                        }

                    }
                    if (response.IsSuccessStatusCode)
                    {
                        result = "1";
                    }
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AssignComplaintToHelper(string Options)
        {
            var result = "";
            ComplaintHelperMapping ob = new ComplaintHelperMapping();
            ob.ComplaintId = Convert.ToInt32(Options.Split('#')[0]);
            string helper = Options.Split('#')[1];
            ob.HelperName = (Helpers)Enum.Parse(typeof(Helpers), Options.Split('#')[1]);
            ob.UserId = Convert.ToInt32((Helpers)ob.HelperName);
            using (var client = new HttpClient())
            {
                string assignmentData = JsonConvert.SerializeObject(ob);
                var response = await client.PostAsync("http://citizencomplaintportal.azurewebsites.net/api/helpers/assign", new StringContent(assignmentData, Encoding.UTF8, "text/json"));
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = "1";
                    }
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}