using System;
using System.ComponentModel.DataAnnotations;


namespace APBD.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Imie wymagane")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }
        [Required]
        public DateTime? BrithDate { get; set; }
        [Required]
        public string Studies { get; set; }
    }
}
