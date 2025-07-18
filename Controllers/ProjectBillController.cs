using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThomasConstruction.Controllers;

public class ProjectBillController : Controller
{

  private readonly ApplicationDbContext _context;

  public ProjectBillController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index()
  {

    return _context.ProjectBills != null ?
                              View(await _context.ProjectBills.Include(c => c.project).Include(c => c.bill).ToListAsync()) :
                                Problem("Entity set 'ApplicationDbContext.Bills'  is null.");

  }

  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.Projects = new SelectList(_context.Projects, "id_project", "project_name");
    ViewBag.Bills = new SelectList(_context.Bills.Where(p => p.active == true), "id_bill", "bill_name");

    return View();
  }

  // POST: ProjectBill/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(ProjectBillModel projectBill)
  {
    //if (ModelState.IsValid)
    if (projectBill.id_project != 0 && projectBill.id_bill != 0)
    {
      _context.Add(projectBill);

      //actualizar el costo del proyecto
       var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == projectBill.id_project);

        if (project != null)
        {
            project.cost += projectBill.amount;
            _context.Update(project);
        }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");
    ViewBag.Bills = new SelectList(_context.Bills.Where(p => p.active == true).ToList(), "id_bill", "bill_name");

    return View(projectBill);
  }

  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.ProjectBills == null)
    {
      return NotFound();
    }

    var projectBill = await _context.ProjectBills
        .FirstOrDefaultAsync(m => m.id_project_bill == id);
    if (projectBill == null)
    {
      return NotFound();
    }

    var viewModel = new ProjectBillDetailsViewModel
    {
      id_project_bill = projectBill.id_project_bill,
      bill_date = projectBill.bill_date,
      amount = projectBill.amount,
      details = projectBill.details,
      project = _context.Projects.Find(projectBill.id_project)?.project_name,
      bill = _context.Bills.Find(projectBill.id_bill)?.bill_name

    };




    return View(viewModel);
  }


  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.ProjectBills == null)
    {
      return NotFound();
    }

    var projectBill = await _context.ProjectBills.FindAsync(id);
    if (projectBill == null)
    {
      return NotFound();
    }
    //seleccionar el proyecto asociado a la persona

    var viewModel = new ProjectBillViewModel
    {
      id_project_bill = projectBill.id_project_bill,
      bill_date = projectBill.bill_date,
      amount = projectBill.amount,
      details = projectBill.details,

      id_project = projectBill.id_project, // valor que debe aparecer seleccionado
      id_bill = projectBill.id_bill, // valor que debe aparecer seleccionado
      projects = _context.Projects
              .Select(s => new SelectListItem
              {
                Value = s.id_project.ToString(),
                Text = s.project_name
              }).ToList(),

      bills = _context.Bills.Where(p => p.active == true || p.id_bill == projectBill.id_bill)
              .Select(s => new SelectListItem
              {
                Value = s.id_bill.ToString(),
                Text = s.bill_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: Payment/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, ProjectBillViewModel projectBill)
  {
    if (id != projectBill.id_project_bill)
    {
      return NotFound();
    }

    if (projectBill.id_project_bill != 0)
    {

       var billBeforeU = await _context.ProjectBills
    .AsNoTracking() // importante para evitar conflicto de tracking
    .FirstOrDefaultAsync(c => c.id_bill == projectBill.id_bill);

      try
      {
        var projectBillModel = _context.ProjectBills.Find(projectBill.id_project_bill);
        projectBillModel.bill_date = projectBill.bill_date!;
        projectBillModel.id_project = projectBill.id_project!;
        projectBillModel.id_bill = projectBill.id_bill!;
        projectBillModel.amount = projectBill.amount;
        projectBillModel.details = projectBill.details;


        _context.Update(projectBillModel);

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == projectBill.id_project);
        if (project != null)
        {
          double diff = projectBill.amount - (billBeforeU != null ? billBeforeU.amount : 0);
          project.cost += diff;
          _context.Update(project);

        }

        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProjectBillModelExists(projectBill.id_project_bill))
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

    projectBill.projects = _context.Projects.Select(s => new SelectListItem
    {
      Value = s.id_project.ToString(),
      Text = s.project_name
    }).ToList();

     projectBill.bills = _context.Bills.Select(s => new SelectListItem
    {
      Value = s.id_bill.ToString(),
      Text = s.bill_name
    }).ToList();

    return View(projectBill);



  } 


   // GET: ProjectBill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectBills == null)
            {
                return NotFound();
            }

            var projectBill = await _context.ProjectBills.Include(c => c.project).Include(c => c.bill)
                .FirstOrDefaultAsync(m => m.id_project_bill == id);
            if (projectBill == null)
            {
                return NotFound();
            }

            return View(projectBill);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectBills == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bills'  is null.");
            }
            var projectBill = await _context.ProjectBills.FindAsync(id);
            if (projectBill != null)
            {
                _context.ProjectBills.Remove(projectBill);
            }
            //actualizar el costo del proyecto
       var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == projectBill.id_project);

        if (project != null)
        {
            project.cost -= projectBill.amount;
            _context.Update(project);
        }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
              
  
         private bool ProjectBillModelExists(int id)
  {
    return (_context.ProjectBills?.Any(e => e.id_project_bill == id)).GetValueOrDefault();
  }
    



  


}
