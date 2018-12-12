using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndCapstone.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [Required]
        [Display(Name = "Created by")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


        [Display(Name = "Employee")]
        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Display(Name = "Complete?")]
        public bool IsComplete { get; set; }

        public virtual ICollection<EmployeeTicket> EmployeeTickets { get; set; }
    }
}
