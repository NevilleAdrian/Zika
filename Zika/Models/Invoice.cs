using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Zika.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public DateTime DateIssued { get; set; }

        public string Extra { get; set; }

        [ForeignKey("FreightId")]
        public Freight Freight { get; set; }
        public int FreightId { get; set; }
    }
}