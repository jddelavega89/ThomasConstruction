namespace ThomasConstruction.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int? SelectedProjectId { get; set; }
    }
}
