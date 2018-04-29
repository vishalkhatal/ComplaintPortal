using System.Web.Mvc;

namespace CitizenComplaintPortal.Web.Areas.Citizen
{
    public class CitizenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Citizen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Citizen_default",
                "Citizen/{controller}/{action}/{id}",
                new { controller = "Home", action = "RaiseComplaint", id = UrlParameter.Optional }
            );
        }
    }
}