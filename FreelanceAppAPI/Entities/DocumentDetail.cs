using System;
using System.ComponentModel.DataAnnotations;

namespace FreelanceAppAPI.Entities
{
    public class DocumentDetail
    {
        [Key]
        public Guid RowId { get; set; }
        [Required]
        public DateTime WorkedDate  { get; set; }
        [Required]
        public int TimeIn { get; set; }
        [Required]
        public int TimeOut { get; set; }
        public int OffTimeStart { get; set; }
        public int OffTimeEnd { get; set; }
        public Decimal TotalHours { get; set; }
        public string TaskComment  { get; set; }    
    }
}