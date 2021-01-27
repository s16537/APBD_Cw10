using System;
using System.Collections.Generic;

#nullable disable

namespace APBD_Cw10.Models
{
    public partial class PromoteStudentsResult
    {
        public int ErrorCode { get; set; }
        public int IdEnrollment { get; set; }
        public int IdStudies { get; set; }
        public DateTime StartDate{ get; set; }
    }
}
