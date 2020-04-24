using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Imie wymagane")]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string indexNumber { get; set; }
        [Required]
        public DateTime? BrithDate { get; set; }
        [Required]
        public string Studies { get; set; }
    }
}
