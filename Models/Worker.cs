using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Worker", Schema = "dbo")]
    public class WorkerModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_worker { get; set; }
        
        [Display(Name = "Worker Name")]
        required
        public string worker_name { get; set; }

       
        [Display(Name = "Active")]
        public bool active { get; set; }  = true;
    }

    

}