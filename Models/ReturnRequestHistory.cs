using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("ReturnRequestHistories")]
    public class ReturnRequestHistory : BaseEntity
    {
        [Required]
        public int ReturnRequestId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Action { get; set; }

        public string Note { get; set; }

        [Required]
        public new int Status { get; set; } = 1;

        [ForeignKey("ReturnRequestId")]
        public ReturnRequest ReturnRequest { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
} 