using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Subcontractor", Schema = "dbo")]
    public class SubcontractorModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_subcontractor { get; set; }

        [Display(Name = "Subcontractor Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime? date_subc { get; set; } 

        [DataType(DataType.Currency)]
        [Display(Name = "Cost")]
        public double cost { get; set; }

        
        [Display(Name = "Details")]
        public string details { get; set; }

        public int id_project { get; set; }

        [ForeignKey("id_project")]
        public required ProjectModel project { get; set; } // Relation with Project
    }

    

}