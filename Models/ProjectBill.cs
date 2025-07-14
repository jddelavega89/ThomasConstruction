using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Project_Bill", Schema = "dbo")]
    public class ProjectBillModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_project_bill { get; set; }

        [Display(Name = "Bill Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime bill_date { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public double amount { get; set; }

        [Display(Name = "Details")]
        public string? details { get; set; }

        public int id_project { get; set; }

        public int id_bill { get; set; }

        [ForeignKey("id_project")]
        public required ProjectModel project { get; set; } // Relation with Project
        
        [ForeignKey("id_bill")]
        public required BillModel bill { get; set; } // Relation with Bill
    }


}