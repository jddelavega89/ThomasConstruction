using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class ProjectViewModel
    {
         [Key]
        [Display(Name = "Identify")]
        public int id_project { get; set; }
        
        [Display(Name = "Project Name")]
        required
        public string project_name { get; set; }

        [Display(Name = "Profit")]
        public float? profit { get; set; }

       public int id_customer { get; set; }

        public string? customer { get; set; }

        public  required List<SelectListItem> customers { get; set; } // para el <select>
    


    }
    

   

       
    

}