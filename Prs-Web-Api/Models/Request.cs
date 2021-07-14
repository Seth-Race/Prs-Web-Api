using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Prs_Web_Api.Models
{
    public partial class Request
    {
        public Request()
        {
            LineItem = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public DateTime DateNeeded { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string ReasonForRejection { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<LineItem> LineItem { get; set; }
    }
}
