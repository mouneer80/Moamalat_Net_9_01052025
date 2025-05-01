using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public class CRChecker
    {
        [Required]
        public int? CR { get; set; }
        [Required]
        public DateTime? RegistrationDate { get; set; }
        [Required]
        public string? NationalId { get; set; }
        [Required]
        public double? Capital { get; set; }
        [Required]
        public string? LegalStatusCode { get; set; }
        [Required]
        [RegularExpression(@"^[7,9][0-9]*$")]
        public int? Mobile { get; set; }
       
        [Required]
        public string? GradeId { get; set; }

        public bool? isNpCorporate { get; set; }
    }


    public class PhoneData
    {
        public int PhoneType { set; get; }

        public int CountryCode { set; get; }

        public string PhoneNumber { set; get; }
        public PhoneData()
        { }

    }

   

}
