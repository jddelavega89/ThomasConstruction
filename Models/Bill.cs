using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Bill", Schema = "dbo")]
    public class BillModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_bill { get; set; }
        
        [Display(Name = "Bill Name")]
        required
        public string bill_name { get; set; }

        [Display(Name = "Details")]
        public string? details { get; set; }

        [Display(Name = "Active")]
        public bool active { get; set; }  = true;
    }

    

}