using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ThomasConstruction.Controllers;

[Authorize]
public class WorkerController : Controller
{

  private readonly ApplicationDbContext _context;

  public WorkerController(ApplicationDbContext context)
  {
    _context = context;
  }


  public async Task<IActionResult> Index()
  {
    return _context.Workers != null ?
                           View(await _context.Workers.ToListAsync()) :
                           Problem("Entity set 'ApplicationDbContext.Workers'  is null.");
  }

  // GET: Worker/Create
  public IActionResult Create()
  {
    return View();
  }

  // POST: Worker/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("worker_name")] WorkerModel worker)
  {
    if (ModelState.IsValid)
    {
      _context.Add(worker);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(worker);
  }


  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.Workers == null)
    {
      return NotFound();
    }

    var workerModel = await _context.Workers
        .FirstOrDefaultAsync(m => m.id_worker == id);
    if (workerModel == null)
    {
      return NotFound();
    }

    return View(workerModel);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.Workers == null)
    {
      return NotFound();
    }

    var workerModel = await _context.Workers.FindAsync(id);
    if (workerModel == null)
    {
      return NotFound();
    }
    return View(workerModel);
  }

  // POST: Worker/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, [Bind("id_worker,worker_name,active")] WorkerModel workerModel)
  {
    if (id != workerModel.id_worker)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      try
      {
        _context.Update(workerModel);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!WorkerModelExists(workerModel.id_worker))
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
    return View(workerModel);
  }

  // GET: Worker/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null || _context.Workers == null)
    {
      return NotFound();
    }

    var workerModel = await _context.Workers
        .FirstOrDefaultAsync(m => m.id_worker == id);
    if (workerModel == null)
    {
      return NotFound();
    }

    return View(workerModel);
  }

  // POST: Worker/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    if (_context.Workers == null)
    {
      return Problem("Entity set 'ApplicationDbContext.Workers'  is null.");
    }
    var workerModel = await _context.Workers.FindAsync(id);

    //validar que no este asociado a ningun Worker
    if (_context.WorkerSalarys.Any(c => c.id_worker == id))
    {
      // ModelState.AddModelError("", "No se puede eliminar el estado porque tiene clientes asociados.");
      //return View(customer); // o redirigir a Details u otra vista con mensaje
      TempData["ErrorMessage"] = "Cannot be deleted because there are Salarys associated with this worker.";
      return RedirectToAction("Details", new { id = id });
    }


    if (workerModel != null)
    {
      _context.Workers.Remove(workerModel);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }

  private bool WorkerModelExists(int id)
  {
    return (_context.Workers?.Any(e => e.id_worker == id)).GetValueOrDefault();
  }



}
