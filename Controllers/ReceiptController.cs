using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThomasConstruction.Controllers;

public class ReceiptController : Controller
{

  private readonly ApplicationDbContext _context;

  public ReceiptController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index()
  {

    return _context.Receipts != null ?
                              View(await _context.Receipts.Include(c => c.project).ToListAsync()) :
                                Problem("Entity set 'ApplicationDbContext.Receipt'  is null.");

  }


  // GET: Receipt/Create
  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.Projects = new SelectList(_context.Projects, "id_project", "project_name");

    return View();
  }

  // POST: Receipt/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(ReceiptModel receipt)
  {
    //if (ModelState.IsValid)
    if (receipt.id_project != 0)
    {
      _context.Add(receipt);

    //actualizar el costo del proyecto
       var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == receipt.id_project);

        if (project != null)
        {
            project.cost += receipt.amount;
            _context.Update(project);
        }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");

    return View(receipt);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.Receipts == null)
    {
      return NotFound();
    }

    var receipt = await _context.Receipts.FindAsync(id);
    if (receipt == null)
    {
      return NotFound();
    }
    //seleccionar el proyecto asociado a la persona

    var viewModel = new ReceiptViewModel
    {
      id_receipt = receipt.id_receipt, 
      receipt_date = receipt.receipt_date,
      amount = receipt.amount,
      details = receipt.details,

      id_project = receipt.id_project, // valor que debe aparecer seleccionado
      projects = _context.Projects
              .Select(s => new SelectListItem
              {
                Value = s.id_project.ToString(),
                Text = s.project_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: Receipt/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, ReceiptViewModel receipt)
  {
    if (id != receipt.id_receipt)
    {
      return NotFound();
    }

   if (receipt.id_receipt != 0)
    {
      
       var receiptBeforeU = await _context.Receipts
    .AsNoTracking() // importante para evitar conflicto de tracking
    .FirstOrDefaultAsync(c => c.id_receipt == receipt.id_receipt);

      try
      {
        var receiptModel = _context.Receipts.Find(receipt.id_receipt);
        receiptModel.receipt_date = receipt.receipt_date!;
        receiptModel.id_project = receipt.id_project!;
        receiptModel.amount = receipt.amount;
        receiptModel.details = receipt.details;


        _context.Update(receiptModel);

         var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == receipt.id_project);
        if (project != null)
        {
          double diff = receipt.amount - (receiptBeforeU != null ? receiptBeforeU.amount : 0);
          project.cost += diff;
          _context.Update(project);

        }

        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ReceiptModelExists(receipt.id_receipt))
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

    receipt.projects = _context.Projects.Select(s => new SelectListItem
    {
      Value = s.id_project.ToString(),
      Text = s.project_name
    }).ToList();

    return View(receipt);



  }
  

    public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(m => m.id_receipt == id);
            if (receipt == null)
            {
                return NotFound();
            }

             var viewModel = new ReceiptDetailsViewModel
             {
               id_receipt = receipt.id_receipt,
               receipt_date = receipt.receipt_date,
               amount = receipt.amount,
               details = receipt.details,
               project = _context.Projects.Find(receipt.id_project)?.project_name
                                      
             };




         return View(viewModel);
        }


         // GET: Receipt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts.Include(c => c.project)
                .FirstOrDefaultAsync(m => m.id_receipt == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receipts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Receipt'  is null.");
            }
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
            }

               //actualizar el costo del proyecto
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == receipt.id_project);

        if (project != null)
        {
            project.cost -= receipt.amount;
            _context.Update(project);
        }
            
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        private bool ReceiptModelExists(int id)
        {
          return (_context.Receipts?.Any(e => e.id_receipt == id)).GetValueOrDefault();
        }
          



}
