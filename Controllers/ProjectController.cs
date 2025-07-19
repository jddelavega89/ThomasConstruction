using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ThomasConstruction.Controllers;

//[Authorize(Roles = "Admin")]
[Authorize]
public class ProjectController : Controller
{

  private readonly ApplicationDbContext _context;

  public ProjectController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index()
  {

    //  return _context.Projects != null ?
    //    View(await _context.Projects.Include(c => c.customer).ToListAsync()) :
    //    Problem("Entity set 'ApplicationDbContext.Project'  is null.");

    var projects = await _context.Projects
   .Select(p => new ProjectIndexViewModel
   {
     id_project = p.id_project,
     project_name = p.project_name,
     project_date = p.project_date,
     customer = p.customer.customer_name,
     budget = p.budget,
     cost = p.cost,
     profit = p.profit,
     downpayment = p.downpayment,
     total_budget = p.total_budget,
     totalBills = _context.ProjectBills
           .Where(b => b.id_project == p.id_project)
           .Sum(b => (double?)b.amount) ?? 0,
     totalReceipts = _context.Receipts
           .Where(b => b.id_project == p.id_project)
           .Sum(b => (double?)b.amount) ?? 0,
     totalSupplies = _context.Supplies
           .Where(b => b.id_project == p.id_project)
           .Sum(b => (double?)b.amount) ?? 0,
     totalChangeOrders = _context.ChangeOrders
           .Where(b => b.id_project == p.id_project)
           .Sum(b => (double?)b.amount) ?? 0,
     totalPayments = +p.downpayment + (_context.Payments
           .Where(b => b.id_project == p.id_project)
           .Sum(b => (double?)b.amount) ?? 0)

   })
   .ToListAsync();



    return View(projects);

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
      project.total_budget = project.budget;

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

      var changeOrders = await _context.ChangeOrders
                .Where(c => c.id_project == project.id_project)
                .ToListAsync();

      double totalChanges = changeOrders.Sum(c => c.amount);

      try
      {
        var projectModel = _context.Projects.Find(project.id_project);
        projectModel.project_name = project.project_name!;
        projectModel.id_customer = project.id_customer!;
        projectModel.budget = project.budget;
        projectModel.downpayment = project.downpayment;
        projectModel.project_date = project.project_date;
        projectModel.total_budget = project.budget + totalChanges;


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
    //var project = await _context.Projects.FindAsync(id);

    var project = await _context.Projects
  .Include(p => p.payments)
  .Include(p => p.projectBill)
  .Include(p => p.receipt)
  .Include(p => p.supplie)
  .Include(p => p.changeOrder)
  .FirstOrDefaultAsync(p => p.id_project == id);
    if (project != null)
    {
      //debo buscar todos los elementos aosciados al proywecto y eliminarlos
      if (project.payments != null)
        _context.Payments.RemoveRange(project.payments);

      if (project.projectBill != null)
        _context.ProjectBills.RemoveRange(project.projectBill);

      if (project.receipt != null)
        _context.Receipts.RemoveRange(project.receipt);

      if (project.supplie != null)
        _context.Supplies.RemoveRange(project.supplie);

      if (project.changeOrder != null)
        _context.ChangeOrders.RemoveRange(project.changeOrder);

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
