using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class CustomerViewModel
    {
        [Display(Name = "Identify")]
        public int id_customer { get; set; }

        [Display(Name = "Customer Name")]
        
        public required string customer_name
        { get; set; }

        [Display(Name = "Address")]
        public string? address { get; set; }

        [Display(Name = "City")]
        public string? city { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "El código postal debe tener exactamente 5 dígitos.")]
       
        public string? zip_code { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        [RegularExpression(@"^(\(?\d{3}\)?[\s.-]?)?\d{3}[\s.-]?\d{4}$", 
        ErrorMessage = "El número de teléfono debe ser válido y del formato de EE.UU.")]
        public string? phone { get; set; }

        [Display(Name = "Identify State")]
        [Required]
        public int id_state { get; set; }

         public string? state { get; set; }

        public  required List<SelectListItem> states { get; set; } // para el <select>
    


    }
    

   

       
    

}