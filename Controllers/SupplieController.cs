using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThomasConstruction.Controllers;

public class SupplieController : Controller
{

  private readonly ApplicationDbContext _context;

  public SupplieController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index()
  {

    return _context.Supplies != null ?
                              View(await _context.Supplies.Include(c => c.project).ToListAsync()) :
                                Problem("Entity set 'ApplicationDbContext.Supplies'  is null.");

  }

  // GET: Supplie/Create
  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.Projects = new SelectList(_context.Projects, "id_project", "project_name");

    return View();
  }

  // POST: Payment/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(SupplieViewModel supplie)
  {
    //if (ModelState.IsValid)
    if (supplie.id_project != 0)
    {
      SupplieModel supplieModel = new SupplieModel()
      {
        details = supplie.details,
        amount = supplie.amount,
        price = supplie.price,
        id_project = supplie.id_project,
        date_supplie = supplie.date_supplie,
        project = _context.Projects.Find(supplie.id_project)

      };


      _context.Add(supplieModel);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");

    return View(supplie);
  }

  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.Supplies == null)
    {
      return NotFound();
    }

    var supplie = await _context.Supplies
        .FirstOrDefaultAsync(m => m.id_supplie == id);
    if (supplie.id_supplie == null)
    {
      return NotFound();
    }

    var viewModel = new SupplieDetailsViewModel
    {
      id_supplie = supplie.id_supplie,
      date_supplie = supplie.date_supplie,
      amount = supplie.amount,
      details = supplie.details,
      project = _context.Projects.Find(supplie.id_project)?.project_name,
      price = supplie.price,
      price_tax = supplie.price_tax,
      total_price = supplie.total_price

    };




    return View(viewModel);
  }



  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.Supplies == null)
    {
      return NotFound();
    }

    var supplie = await _context.Supplies.FindAsync(id);
    if (supplie == null)
    {
      return NotFound();
    }
    //seleccionar el proyecto asociado a la persona

    var viewModel = new SupplieViewModel
    {
      id_supplie = supplie.id_supplie,
      date_supplie = supplie.date_supplie,
      amount = supplie.amount,
      details = supplie.details,
      price = supplie.price,
      price_tax = supplie.price_tax,
      total_price = supplie.total_price,

      id_project = supplie.id_project, // valor que debe aparecer seleccionado
      projects = _context.Projects
              .Select(s => new SelectListItem
              {
                Value = s.id_project.ToString(),
                Text = s.project_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: Payment/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, SupplieViewModel supplie)
  {
    if (id != supplie.id_supplie)
    {
      return NotFound();
    }

    if (supplie.id_supplie != 0)
    {
      try
      {
        var supplieModel = _context.Supplies.Find(supplie.id_supplie);
        supplieModel.date_supplie = supplie.date_supplie!;
        supplieModel.id_project = supplie.id_project!;
        supplieModel.amount = supplie.amount;
        supplieModel.details = supplie.details;
        supplieModel.price = supplie.price;


        _context.Update(supplieModel);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SupplieModelExists(supplie.id_supplie))
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

    supplie.projects = _context.Projects.Select(s => new SelectListItem
    {
      Value = s.id_project.ToString(),
      Text = s.project_name
    }).ToList();

    return View(supplie);



  }
  
      // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supplies == null)
            {
                return NotFound();
            }

            var supplie = await _context.Supplies.Include(c => c.project)
                .FirstOrDefaultAsync(m => m.id_supplie == id);
            if (supplie == null)
            {
                return NotFound();
            }

            return View(supplie);
        }
  
    // POST: Payment/Delete/5
  [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Supplies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Supplie'  is null.");
            }
            var supplie = await _context.Supplies.FindAsync(id);
            if (supplie != null)
            {
                _context.Supplies.Remove(supplie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        




  private bool SupplieModelExists(int id)
  {
    return (_context.Supplies?.Any(e => e.id_supplie == id)).GetValueOrDefault();
  }



}
