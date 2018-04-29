using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizensComplaintPortal.Models
{

    public class UpdateComplaint{
        public int ComplaintId { get; set; }
        public ComplaintStatus ComplaintStatus { get; set; }
    }
    public class Complaint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintId { get; set; }
        //[Required]
        [StringLength(15)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        //[Required]
        [StringLength(15)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        //[EmailAddress]
        [Display(Name = "Email Id")]
        public string EmailAddress { get; set; }
        //[Required]
        [Display(Name = "Mobile No.")]
        [MaxLength(10,ErrorMessage = "Mobile No. has to be atleast 10 digits long.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile number must be numeric")]
        public string MobileNo { get; set; }
        [Required]
        [Display(Name = "Complaint Description")]
        public string ComplaintDescription { get; set; }
        [Required]
        public string Address { get; set; }
        //[Required]
        [StringLength(6)]
        public string PinCode { get; set; }
        [Required]
        [Display(Name = "Complaint Status")]
        public ComplaintStatus complaintStatus { get; set; }
        [Required]
        [Display(Name = "Complaint Type")]
        public ComplaintType complaintType { get; set; }
        public int? UserId { get; set; }
        public Helpers? HelperName { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

    }

    public class ComplaintHelperMapping
    {
        public int ComplaintId { get; set; }
        public int UserId { get; set; }
        public Helpers? HelperName { get; set; }
        public ComplaintStatus complaintStatus { get; set; }

    }


}
