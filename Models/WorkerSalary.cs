using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Worker_Salary", Schema = "dbo")]
    public class WorkerSalaryModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_salary { get; set; }

        [Display(Name = "Salary Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? salary_date { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price Hour")]
        public double price_hour { get; set; }

         [DataType(DataType.Currency)]
        [Display(Name = "Work Hours")]
        public double work_hours { get; set; }

         [DataType(DataType.Currency)]
        [Display(Name = "Salary")]
        public double salary { get;  }

        public int id_project { get; set; }

        public int id_worker { get; set; }

        [ForeignKey("id_project")]
        public required ProjectModel project { get; set; } // Relation with Project
        
        [ForeignKey("id_worker")]
        public required WorkerModel worker { get; set; } // Relation with Worker
    }


}