using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasConstruction.Models
{
    [Table("Project", Schema = "dbo")]
    public class ProjectModel
    {
        [Key]
        [Display(Name = "Identify")]
        public int id_project { get; set; }

        [Display(Name = "Project Name")]
        required
        public string project_name
        { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Profit")]
        public double profit { get; }


        [DataType(DataType.Currency)]
        [Display(Name = "Downpayment")]
        public double? downpayment { get; set; } = 0;

        [DataType(DataType.Currency)]
        [Display(Name = "Initial Budget")]
        public double budget { get; set; }

        [Display(Name = "Total Budget")]
        public double? total_budget { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Cost")]
        public double? cost { get; set; } = 0;


        [Display(Name = "Contract Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? project_date { get; set; }


        [Display(Name = "Costumer")]
        public int id_customer { get; set; }

        [ForeignKey("id_customer")]
        public required CustomerModel customer { get; set; } // Relation with Customer

        public List<PaymentModel>? payments { get; set; }
        public List<ProjectBillModel>? projectBill { get; set; }

        public List<ReceiptModel>? receipt { get; set; }
        public List<SupplieModel>? supplie { get; set; }

        public List<ChangeOrderModel>? changeOrder { get; set; }
        
           public List<WorkerSalaryModel>? workerSalary { get; set; }
        
    
    }

    

}