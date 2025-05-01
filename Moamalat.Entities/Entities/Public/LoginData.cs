using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public class LoginData
    {
        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(@"^[0-9]+$")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^[0-9a-zA-Z]+$")]
        public string? password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RecoverLoginData
    {
        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(@"^[0-9]+$")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "National Id")]
        [RegularExpression(@"^[0-9a-zA-Z]+$")]
        public string? NationalId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$")]
        [Display(Name = "Mobile Number")]
        public string? Mobile { get; set; }
    }

    public class LoginResponse
    {
        public ResponseStatus StatusId { get; set; }
        public int CompanyId { get; set; }
        public string? Mobile { get; set; }
        public int GradeId { get; set; }
        public string Status { get; set; }
       // public UserData? Userdata { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }

       //public List<Menu>? Menus { get; set; }

    }
    public class DBResponse
    {
        public ResponseStatus StatusId { get; set; }
        public string? Status { get; set; }
        public object?  Result { get; set; }
    }
    public enum ResponseStatus
    {
        Failed = -1,
        Succeeded = 1

    }


}
