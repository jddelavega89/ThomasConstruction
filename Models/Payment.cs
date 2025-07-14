using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Payment", Schema = "dbo")]
    public class PaymentModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_payment { get; set; }

        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime payment_date { get; set; } 

         [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public double amount { get; set; }
        
        [Display(Name = "Details")]
        public string? details { get; set; }

        public int id_project { get; set; }

        [ForeignKey("id_project")]
        public required ProjectModel project { get; set; } // Relation with Project
    }

    

}