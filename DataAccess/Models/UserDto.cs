using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class UserDto
    //:IValidatableObject
    {
        public string id { get; set; }

        public string Password { get; set; }
        public int? Status { get; set; }
        public string StatusTitle { get; set; }
        [Required]
        public string Username { get; set; }
        public string fullname { get; set; }
        public string DatetimeCreated { get; set; }
        public string DatetimeModified { get; set; }
        public int? LoginFailed { get; set; }

        public string MenuPermissionList { get; set; }
        public string Jwt { get; set; }
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        public bool StatusCode { get; set; }
        //	if (Password.Length < 4)
        //	{
        //		yield return new ValidationResult("کلمه عبور نمی تواند کوچکتر از4  کاراکتر باشد");
        //	}
        //}
    }
}
