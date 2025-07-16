using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThomasConstruction.ViewModels
{

    public class SupplieViewModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_supplie { get; set; }

        [Display(Name = "Supplie Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime? date_supplie { get; set; } 

        
        [Display(Name = "Amount")]
        public int amount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public double price { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price Tx")]
        public double price_tax { get; set;}
        
        [Display(Name = "Details")]
        public string details { get; set; }

        public int id_project { get; set; }

        public string? project { get; set; }

        public  required List<SelectListItem> projects { get; set; } // para el <select>

        [DataType(DataType.Currency)]
         [Display(Name = "Total Price")]
        public double total_price { get; set; } = 0;
    }

    

}