using CitizensComplaintPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CitizenComplaintPortal.Web.Areas.Helper.Controllers
{
    public class HomeController : Controller
    {
        // GET: Helper/Home
        public async Task<ActionResult> Index()
        {
            List<Complaint> testList = new List<Complaint>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://citizencomplaintportal.azurewebsites.net/api/Helpers/GetAssignedComplaints?id=1");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var complaints = JsonConvert.DeserializeObject<List<Complaint>>(data);
                    testList = complaints;
                }

            }
            return View(testList);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateComplaintStatus(string Options)
        {
            var result = "";
            ComplaintHelperMapping obj = new ComplaintHelperMapping();
            obj.ComplaintId = Convert.ToInt32(Options.Split('#')[0]);
            obj.complaintStatus = (ComplaintStatus)Enum.Parse(typeof(ComplaintStatus), Options.Split('#')[1]);
            
            using (var client = new HttpClient())
            {
                string assignmentData = JsonConvert.SerializeObject(obj);
                var response = await client.PostAsync("http://citizencomplaintportal.azurewebsites.net/api/helpers/UpdateComplaint", new StringContent(assignmentData, Encoding.UTF8, "text/json"));
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = "1";
                        if (obj.complaintStatus == ComplaintStatus.Resolved)
                        {
                            MailMessage mail = new MailMessage("priyankasupplier24@gmail.com", "junhu@microsoft.com");
                            mail.To.Add("brien.christesen@microsoft.com");
                            SmtpClient smtpClient = new SmtpClient();
                            smtpClient.Port = 587;
                            smtpClient.Credentials = new System.Net.NetworkCredential("priyankasupplier24@gmail.com", "Priya@24");
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //smtpClient.UseDefaultCredentials = false;
                            smtpClient.Host = "smtp.gmail.com";
                            smtpClient.EnableSsl = true;
                            mail.Subject = "Issue is resolved with issue id: " + obj.ComplaintId;
                            mail.Body = "Issue is resolved now.";
                            try
                            {
                                smtpClient.Send(mail);
                            }
                            catch (Exception ex)
                            {
                                result = "0";
                            }

                        }
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}