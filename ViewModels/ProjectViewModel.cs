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

        [DataType(DataType.Currency)]
       [Display(Name = "Profit")]
        public double? profit { get; set; }

          [DataType(DataType.Currency)]
        [Display(Name = "Downpayment")]
        public double? downpayment { get; set; } 
      
        [DataType(DataType.Currency)]
        [Display(Name = "Initial Budget")]
        public double budget { get; set; }

         [DataType(DataType.Currency)]
        [Display(Name = "Total Budget")]
        public double? total_budget { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Cost")]
        public double? cost { get; set; } 

        [Display(Name = "Contract Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime? project_date { get; set; } 

         [Display(Name = "Customer")]
       public int id_customer { get; set; }

        public string? customer { get; set; }

        public  required List<SelectListItem> customers { get; set; } // para el <select>
    


    }
    

   

       
    

}