using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThomasConstruction.Controllers;

public class ProjectController : Controller
{

  private readonly ApplicationDbContext _context;

  public ProjectController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index()
  {

    return _context.Projects != null ?
                              View(await _context.Projects.Include(c => c.customer).ToListAsync()) :
                                Problem("Entity set 'ApplicationDbContext.Project'  is null.");

  }


  // GET: Project/Create
  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.Customers = new SelectList(_context.Customers, "id_customer", "customer_name");

    return View();
  }

  // POST: Project/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(ProjectModel project)
  {
    //if (ModelState.IsValid)
    if (!string.IsNullOrWhiteSpace(project.project_name) && project.id_customer != 0)
    {
      _context.Add(project);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.Customers = new SelectList(_context.Customers.ToList(), "id_customer", "customer_name");

    return View(project);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.Projects == null)
    {
      return NotFound();
    }

    var project = await _context.Projects.FindAsync(id);
    if (project == null)
    {
      return NotFound();
    }
    //seleccionar el estado asociado a la persona

    var viewModel = new ProjectViewModel
    {
      id_project = project.id_project,
      project_name = project.project_name,
      project_date = project.project_date,
      downpayment = project.downpayment,
      budget = project.budget,

      id_customer = project.id_customer, // valor que debe aparecer seleccionado
      customers = _context.Customers
              .Select(s => new SelectListItem
              {
                Value = s.id_customer.ToString(),
                Text = s.customer_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: Customer/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, ProjectViewModel project)
  {
    if (id != project.id_project)
    {
      return NotFound();
    }

   if (!string.IsNullOrWhiteSpace(project.project_name) && project.id_customer != 0)
    {
      try
      {
        var projectModel = _context.Projects.Find(project.id_project);
        projectModel.project_name = project.project_name!;
        projectModel.id_customer = project.id_customer!;
        projectModel.budget = project.budget;
        projectModel.downpayment = project.downpayment;
        projectModel.project_date = project.project_date;
        

        _context.Update(projectModel);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProjectModelExists(project.id_project))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return RedirectToAction(nameof(Index));
    }

    project.customers = _context.Customers.Select(s => new SelectListItem
    {
      Value = s.id_customer.ToString(),
      Text = s.customer_name
    }).ToList();

    return View(project);



  }
  

    public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.id_project == id);
            if (project == null)
            {
                return NotFound();
            }

             var viewModel = new ProjectDetailsViewModel
             {
               id_project = project.id_project,
               project_name = project.project_name,
               budget = project.budget,
               cost = project.cost,
               profit = project.profit,
               downpayment = project.downpayment,
               project_date = project.project_date,
               customer = _context.Customers.Find(project.id_customer)?.customer_name
                                      
             };




         return View(viewModel);
        }


         // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.Include(c => c.customer)
                .FirstOrDefaultAsync(m => m.id_project == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Project'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        private bool ProjectModelExists(int id)
  {
    return (_context.Projects?.Any(e => e.id_project == id)).GetValueOrDefault();
  }
    



}
