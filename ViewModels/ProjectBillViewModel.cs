using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class ProjectBillViewModel
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

        public string? project { get; set; }

        public required List<SelectListItem> projects { get; set; } // para el <select>
    

        public int id_bill { get; set; }

        public string? bill { get; set; }

        public  required List<SelectListItem> bills { get; set; } // para el <select>
    

    }
    

   

       
    

}