using System.Web.Mvc;

namespace CitizenComplaintPortal.Web.Areas.TeamLeader
{
    public class TeamLeaderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TeamLeader";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TeamLeader_default",
                "TeamLeader/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}