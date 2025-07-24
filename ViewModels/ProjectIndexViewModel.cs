using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ThomasConstruction.ViewModels
{    public class ProjectIndexViewModel
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
    [Display(Name = "Total Cost")]
    public double? cost { get; set; }

    [Display(Name = "Contract Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? project_date { get; set; }

    public int id_customer { get; set; }

    [Display(Name = "Customer")]
    public string? customer { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Total Bills")]
    public double? totalBills { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Total Receipts")]
    public double? totalReceipts { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Total Supplies")]
    public double? totalSupplies { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Total Payments")]
    public double? totalPayments { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Change Orders")]
    public double? totalChangeOrders { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Worker Salaries")]
    public double? totalWorkers { get; set; }

       [DataType(DataType.Currency)]
    [Display(Name = "SubContractors")]
    public double? totalSubContractor { get; set; }


    }
    

   

       
    

}