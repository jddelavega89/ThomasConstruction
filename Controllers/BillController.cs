using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ThomasConstruction.Controllers;

[Authorize]
public class BillController : Controller
{

  private readonly ApplicationDbContext _context;

  public BillController(ApplicationDbContext context)
  {
    _context = context;
  }


  public async Task<IActionResult> Index()
  {
    return _context.Bills != null ?
                           View(await _context.Bills.ToListAsync()) :
                           Problem("Entity set 'ApplicationDbContext.Bills'  is null.");
  }

  // GET: Bill/Create
  public IActionResult Create()
  {
    return View();
  }

  // POST: Bill/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("bill_name,details")] BillModel bill)
  {
    if (ModelState.IsValid)
    {
      _context.Add(bill);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(bill);
  }


  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.Bills == null)
    {
      return NotFound();
    }

    var billModel = await _context.Bills
        .FirstOrDefaultAsync(m => m.id_bill == id);
    if (billModel == null)
    {
      return NotFound();
    }

    return View(billModel);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.Bills == null)
    {
      return NotFound();
    }

    var billModel = await _context.Bills.FindAsync(id);
    if (billModel == null)
    {
      return NotFound();
    }
    return View(billModel);
  }

  // POST: Bill/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, [Bind("id_bill,bill_name,active,details")] BillModel billModel)
  {
    if (id != billModel.id_bill)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      try
      {
        _context.Update(billModel);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BillModelExists(billModel.id_bill))
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
    return View(billModel);
  }

  // GET: Bill/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null || _context.Bills == null)
    {
      return NotFound();
    }

    var billModel = await _context.Bills
        .FirstOrDefaultAsync(m => m.id_bill == id);
    if (billModel == null)
    {
      return NotFound();
    }

    return View(billModel);
  }

  // POST: Bill/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    if (_context.Bills == null)
    {
      return Problem("Entity set 'ApplicationDbContext.Bills'  is null.");
    }
    var billModel = await _context.Bills.FindAsync(id);

    //validar que no este asociado a ningun Bill
    if (_context.ProjectBills.Any(c => c.id_bill == id))
    {
      // ModelState.AddModelError("", "No se puede eliminar el estado porque tiene clientes asociados.");
      //return View(customer); // o redirigir a Details u otra vista con mensaje
      TempData["ErrorMessage"] = "No se puede eliminar porque hay Bills asociados a esta categoria.";
      return RedirectToAction("Details", new { id = id });
    }


    if (billModel != null)
    {
      _context.Bills.Remove(billModel);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }

  private bool BillModelExists(int id)
  {
    return (_context.Bills?.Any(e => e.id_bill == id)).GetValueOrDefault();
  }



}
