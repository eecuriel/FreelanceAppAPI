using System;
using System.ComponentModel.DataAnnotations;

namespace FreelanceAppAPI.Entities
{
    public class Customer
    {
        [Key]
        public Guid  CutomerId { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre del cliente")]
        public string CutomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    
    }
}