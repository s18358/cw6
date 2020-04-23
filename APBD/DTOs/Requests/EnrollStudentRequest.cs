using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Imie wymagane")]
        [MaxLength(1000)]
        public string firstName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string lastName { get; set; }
        [RegularExpression("^s[0-9]+$")]
        [Required]
        public string indexNumber { get; set; }
        [Required]
        public DateTime Brithdate { get; set; }
        [Required]
        public string Studies { get; set; }
    }
}
