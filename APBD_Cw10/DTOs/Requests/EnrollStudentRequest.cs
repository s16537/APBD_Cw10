using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Cw10.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [RegularExpression("^[sS][0-9]+$")]
        public string IndexNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string Studies { get; set; }
    }
}
