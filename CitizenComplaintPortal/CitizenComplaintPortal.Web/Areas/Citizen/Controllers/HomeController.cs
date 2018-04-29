using CitizensComplaintPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CitizenComplaintPortal.Web.Areas.Citizen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult RaiseComplaint()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RaiseComplaint(Complaint complaint)
        {
            using (var client = new HttpClient())
            {
                string complaintData = JsonConvert.SerializeObject(complaint);
                var response = await client.PostAsync("http://citizencomplaintportal.azurewebsites.net/api/Complaints/Save", new StringContent(complaintData, Encoding.UTF8, "text/json"));
                //var response = await client.PostAsync("http://localhost:50956/api/Complaints/Save", new StringContent(complaintData, Encoding.UTF8, "text/json"));

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
            return RaiseComplaint();
        }
    }
}