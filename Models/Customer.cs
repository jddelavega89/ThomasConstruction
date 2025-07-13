using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;


namespace ThomasConstruction.Models
{
    [Table("Customer", Schema = "dbo")]
    public class CustomerModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_customer { get; set; }

        [Display(Name = "Customer Name")]
        required
        public string customer_name
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

        // [ForeignKey("id_state")]
        //  [Required]
        [ForeignKey("id_state")]
        public required StateModel state { get; set; } // Relación con State
    


    }
    

   

       
    

}