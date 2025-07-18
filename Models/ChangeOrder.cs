using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Change_Order", Schema = "dbo")]
    public class ChangeOrderModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_change { get; set; }

        [Display(Name = "Change Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime change_date { get; set; } 

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