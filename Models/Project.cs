using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Project", Schema = "dbo")]
    public class ProjectModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_project { get; set; }

        [Display(Name = "Project Name")]
        required
        public string project_name
        { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Profit")]
        public double profit { get; }

        
         [DataType(DataType.Currency)]
        [Display(Name = "Downpayment")]
        public double? downpayment { get; set; } = 0;
      
        [DataType(DataType.Currency)]
        [Display(Name = "Budget")]
        public double budget { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Cost")]
        public double? cost { get; set; } = 0;


        [Display(Name = "Contract Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime? project_date { get; set; } 


        public int id_customer { get; set; }

        [ForeignKey("id_customer")]
        public required CustomerModel customer { get; set; } // Relation with Customer
        
    
    }

    

}