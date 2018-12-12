using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndCapstone.Models
{
    public class EmployeeTicket
    {
        [Key]
        public int EmployeeTicketId { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }


        [Required]
        [Display(Name = "Ticket Title")]
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

    }
}
