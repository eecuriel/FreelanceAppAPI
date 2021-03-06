using System.ComponentModel.DataAnnotations;

namespace FreelanceAppAPI.Entities
{
    public class DocumentHeader
    {
    
        [Key]
        public Guid DocId { get; set; }
        public Guid UserId { get; set; }
        public Guid  CutomerId { get; set; }

        [Required(ErrorMessage="Debe colocar el nombre del proyecto")]
        public string ProjectName { get; set; }
        [Required]
        public DateTime PeriodStart { get; set; }
        [Required]
        public DateTime PeriodEnd { get; set; }
        [Required]
        public int WeekHours  { get; set; }
        public int TotalHoursWorked { get; set; }
        public int RegularHours { get; set; }
        public int OvertimeHours { get; set; }
        public int DocState { get; set; } // 1 = Registrado, 2= Completado, 3 = Facturado
        
    }
}