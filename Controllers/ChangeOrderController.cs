using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThomasConstruction.Controllers;

public class ChangeOrderController : Controller
{

  private readonly ApplicationDbContext _context;

  public ChangeOrderController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index()
  {

    return _context.ChangeOrders != null ?
                              View(await _context.ChangeOrders.Include(c => c.project).ToListAsync()) :
                                Problem("Entity set 'ApplicationDbContext.ChangeOrder'  is null.");

  }


  // GET: ChangeOrder/Create
  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.Projects = new SelectList(_context.Projects, "id_project", "project_name");

    return View();
  }

  // POST: ChangeOrder/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(ChangeOrderModel changeOrder)
  {
    //if (ModelState.IsValid)
    if (changeOrder.id_project != 0)
    {
      _context.Add(changeOrder);

       // Buscar proyecto asociado y actualizar su budget_total
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == changeOrder.id_project);

        if (project != null)
        {
            project.total_budget += changeOrder.amount;
            _context.Update(project);
        }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");

    return View(changeOrder);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.ChangeOrders == null)
    {
      return NotFound();
    }

    var changeOrder = await _context.ChangeOrders.FindAsync(id);
    if (changeOrder == null)
    {
      return NotFound();
    }
    //seleccionar el proyecto asociado a la persona

    var viewModel = new ChangeOrderViewModel
    {
      id_change = changeOrder.id_change, 
      change_date = changeOrder.change_date,
      amount = changeOrder.amount,
      details = changeOrder.details,

      id_project = changeOrder.id_project, // valor que debe aparecer seleccionado
      projects = _context.Projects
              .Select(s => new SelectListItem
              {
                Value = s.id_project.ToString(),
                Text = s.project_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: ChangeOrder/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, ChangeOrderViewModel changeOrder)
  {
    if (id != changeOrder.id_change)
    {
      return NotFound();
    }

   if (changeOrder.id_change != 0)
    {
      try
      {

        //antes de actualizar necesito saber el monto del cambio de orden para restarlo al 
        //budget_total y despues sumar el nuevo monto
        var changeOrderBeforeU =  await _context.ChangeOrders
    .AsNoTracking() // importante para evitar conflicto de tracking
    .FirstOrDefaultAsync(c => c.id_change == changeOrder.id_change);

        var changeOrderModel = _context.ChangeOrders.Find(changeOrder.id_change);
        changeOrderModel.change_date = changeOrder.change_date!;
        changeOrderModel.id_project = changeOrder.id_project!;
        changeOrderModel.amount = changeOrder.amount;
        changeOrderModel.details = changeOrder.details;
               

        _context.Update(changeOrderModel);

      // Buscar proyecto asociado y actualizar su budget_total
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == changeOrder.id_project);

        if (project != null)
        {
          double change = changeOrder.amount - (changeOrderBeforeU != null ? changeOrderBeforeU.amount : 0);
          project.total_budget += change ;
          _context.Update(project);
          await _context.SaveChangesAsync(); 
        }

      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ChangeOrderModelExists(changeOrder.id_change))
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

    changeOrder.projects = _context.Projects.Select(s => new SelectListItem
    {
      Value = s.id_project.ToString(),
      Text = s.project_name
    }).ToList();

    return View(changeOrder);



  }
  

    public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChangeOrders == null)
            {
                return NotFound();
            }

            var changeOrder = await _context.ChangeOrders
                .FirstOrDefaultAsync(m => m.id_change == id);
            if (changeOrder == null)
            {
                return NotFound();
            }

             var viewModel = new ChangeOrderDetailsViewModel
             {
               id_change = changeOrder.id_change,
               change_date = changeOrder.change_date,
               amount = changeOrder.amount,
               details = changeOrder.details,
               project = _context.Projects.Find(changeOrder.id_project)?.project_name
                                      
             };




         return View(viewModel);
        }


         // GET: ChangeOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChangeOrders == null)
            {
                return NotFound();
            }

            var changeOrder = await _context.ChangeOrders.Include(c => c.project)
                .FirstOrDefaultAsync(m => m.id_change == id);
            if (changeOrder == null)
            {
                return NotFound();
            }

            return View(changeOrder);
        }

        // POST: ChangeOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChangeOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChangeOrder'  is null.");
            }
            var changeOrder = await _context.ChangeOrders.FindAsync(id);
    if (changeOrder != null)
    {
      _context.ChangeOrders.Remove(changeOrder);
      //rebajar la orden del budget total
              var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == changeOrder.id_project);

        if (project != null)
        {
           project.total_budget -= changeOrder.amount ;
          _context.Update(project);
         
        }
                

    }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        private bool ChangeOrderModelExists(int id)
        {
          return (_context.ChangeOrders?.Any(e => e.id_change == id)).GetValueOrDefault();
        }
          



}
