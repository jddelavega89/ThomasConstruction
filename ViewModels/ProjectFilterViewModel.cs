
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThomasConstruction.ViewModels
{

    public class ProjectFilterViewModel
    {
        public int? SelectedProjectId { get; set; }
        public string ControllerName { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }
    }
}