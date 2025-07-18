using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class ChangeOrderDetailsViewModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_change { get; set; }

        [Display(Name = "Change Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime change_date { get; set; } 

         [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public double amount { get; set; }
        
        [Display(Name = "Details")]
        public string? details { get; set; }

        public int id_project { get; set; }

        public string? project { get; set; }

    


    }
    

   

       
    

}