using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("State", Schema = "dbo")]
    public class StateModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_state { get; set; }

        [Display(Name = "State")]
        required
        public string state_name
        { get; set; }

        [Display(Name = "Code State")]
        public string? state_code { get; set; }

         [Display(Name = "Active")]
        public bool active { get; set; }  = true;
    }

    

}