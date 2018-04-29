using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizensComplaintPortal.Models
{
    public enum Roles
    {
        Citizen = 0,
        TeamLead = 2,
        Helper = 3,
        CIO = 4
    }
    public enum ComplaintStatus
    {
        Unassinged = 0,
        Assinged = 1,
        Inprogress = 2,
        Pending = 3,
        Escalated = 4,
        Resolved = 5,
        Closed = 6,
        All = 7
    }
    public enum ComplaintType
    {
        Water = 0,
        Electricity = 1,
        Road = 2,
        Cleaning = 3,
        others = 4
    }
    public enum Helpers
    {
        Mehmet = 1,
    }
}
