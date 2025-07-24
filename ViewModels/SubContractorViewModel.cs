using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class SubContractorViewModel
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

        public string? project { get; set; }

        public  required List<SelectListItem> projects { get; set; } // para el <select>
    


    }
    

   

       
    

}