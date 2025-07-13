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
        public string project_name { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Profit")]
        public double? profit { get; set; } = 0;

       public int id_customer { get; set; }

        [ForeignKey("id_customer")]
        public required CustomerModel customer { get; set; } // Relation with Customer
    
    }

    

}