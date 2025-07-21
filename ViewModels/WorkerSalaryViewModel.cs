using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class WorkerSalaryViewModel
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
        public double salary { get; set; }

        public int id_project { get; set; }

        public string? project { get; set; }

        public required List<SelectListItem> projects { get; set; } // para el <select>
    

        public int id_worker { get; set; }

        public string? worker { get; set; }

        public  required List<SelectListItem> workers { get; set; } // para el <select>
    

    }
    

   

       
    

}