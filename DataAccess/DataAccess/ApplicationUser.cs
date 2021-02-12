using Microsoft.AspNetCore.Identity;
using System;

namespace DataAccess.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeModified { get; set; }
        public bool Status { get; set; }
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
