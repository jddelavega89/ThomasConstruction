using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class ReceiptDetailsViewModel
    {
          [Key]
        [Display(Name = "Identify")]
        public int id_receipt { get; set; }

        [Display(Name = "Receipt Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime? receipt_date { get; set; } 

         [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public double amount { get; set; }
        
        [Display(Name = "Details")]
        public string? details { get; set; }

       public int id_project { get; set; }

        [Display(Name = "Project")]
        public string? project { get; set; }

    


    }
    

   

       
    

}