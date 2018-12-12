using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BackEndCapstone.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        // Set up PK -> FK relationships to other objects
        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}