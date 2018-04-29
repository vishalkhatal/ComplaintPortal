using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizensComplaintPortal.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required]
        public string text { get; set; }
        public int ComplaintId { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }

}
