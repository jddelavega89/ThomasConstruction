using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace ThomasConstruction.Controllers;

[Authorize]
public class PaymentController : Controller
{

  private readonly ApplicationDbContext _context;

  public PaymentController(ApplicationDbContext context)
  {
    _context = context;
  }


  public async Task<IActionResult> Index(int? id_project)
  {

   // int pageNumber = page ?? 1;
    //int pageSize = 10;
    // Verificamos si viene un nuevo valor desde el filtro
    if (Request.Query.ContainsKey("id_project"))
    {
      if (id_project.HasValue)
      {
        HttpContext.Session.SetInt32("SelectedProjectId", id_project.Value);
      }
      else
      {
        HttpContext.Session.Remove("SelectedProjectId");
      }
    }
    else
    {
      id_project = HttpContext.Session.GetInt32("SelectedProjectId");
    }

    // Comienza la consulta como IQueryable
    var paymentsQuery = _context.Payments.Include(p => p.project).AsQueryable();

    // Aplica el filtro si se seleccionó un proyecto
    if (id_project.HasValue)
    {
        paymentsQuery = paymentsQuery.Where(p => p.id_project == id_project.Value);
    }

    // Carga los proyectos para el filtro
    var projects = await _context.Projects.ToListAsync();
    ViewBag.Projects = new SelectList(projects, "id_project", "project_name",id_project);
    ViewBag.SelectedProjectId = id_project;

    // Ejecuta la consulta
    //var payments = await paymentsQuery.Skip((pageNumber - 1) * pageSize)
    //.Take(pageSize).ToListAsync();
    var payments = await paymentsQuery.ToListAsync();

  /*
      int totalCount = await paymentsQuery.CountAsync();
      ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
      ViewBag.CurrentPage = pageNumber;
      */

    return View(payments);
   
     // return _context.Payments != null ?
       //                         View(await _context.Payments.Include(c => c.project).ToListAsync()) :
         //                         Problem("Entity set 'ApplicationDbContext.Payment'  is null.");
    

  }


  // GET: Payment/Create
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
  public async Task<IActionResult> Create(PaymentModel payment)
  {
    //if (ModelState.IsValid)
    if (payment.id_project != 0)
    {
      //debo verificar que la suma de todos los pagos sea menor o igual que el total_bdget
    // 1. Obtener el total de pagos del proyecto del nuevo pago
var totalPaymentProject = await _context.Payments.AsNoTracking()
    .Where(p => p.id_project == payment.id_project)
    .SumAsync(p => p.amount);

// 2. Obtener el presupuesto total del proyecto
var totalBudgetProject = await _context.Projects
    .Where(pr => pr.id_project == payment.id_project)
    .Select(pr => pr.total_budget)
   
    .FirstOrDefaultAsync();

// 3. Validar si se puede agregar el nuevo pago
if ((totalPaymentProject + payment.amount) <= totalBudgetProject)
{
    _context.Add(payment);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
else
{
     ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");
    TempData["ErrorMessage"] = "The payment exceeds the project's total budget.";
    return View(payment);
}




    
    }

    ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");

    return View(payment);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.Payments == null)
    {
      return NotFound();
    }

    var payment = await _context.Payments.FindAsync(id);
    if (payment == null)
    {
      return NotFound();
    }
    //seleccionar el proyecto asociado a la persona

    var viewModel = new PaymentViewModel
    {
      id_payment = payment.id_payment,
      payment_date = payment.payment_date,
      amount = payment.amount,
      details = payment.details,

      id_project = payment.id_project, // valor que debe aparecer seleccionado
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
  public async Task<IActionResult> Edit(int id, PaymentViewModel payment)
  {
    if (id != payment.id_payment)
    {
      return NotFound();
    }

    if (payment.id_payment != 0)
    {
      try
      {
        var paymentModel = _context.Payments.Find(payment.id_payment);
        paymentModel.payment_date = payment.payment_date!;
        paymentModel.id_project = payment.id_project!;
        paymentModel.amount = payment.amount;
        paymentModel.details = payment.details;


        _context.Update(paymentModel);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PaymentModelExists(payment.id_payment))
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

    payment.projects = _context.Projects.Select(s => new SelectListItem
    {
      Value = s.id_project.ToString(),
      Text = s.project_name
    }).ToList();

    return View(payment);



  }


  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.Payments == null)
    {
      return NotFound();
    }

    var payment = await _context.Payments
        .FirstOrDefaultAsync(m => m.id_payment == id);
    if (payment == null)
    {
      return NotFound();
    }

    var viewModel = new PaymentDetailsViewModel
    {
      id_payment = payment.id_payment,
      payment_date = payment.payment_date,
      amount = payment.amount,
      details = payment.details,
      project = _context.Projects.Find(payment.id_project)?.project_name

    };




    return View(viewModel);
  }


  // GET: Payment/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null || _context.Payments == null)
    {
      return NotFound();
    }

    var payment = await _context.Payments.Include(c => c.project)
        .FirstOrDefaultAsync(m => m.id_payment == id);
    if (payment == null)
    {
      return NotFound();
    }

    return View(payment);
  }

  // POST: Payment/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    if (_context.Payments == null)
    {
      return Problem("Entity set 'ApplicationDbContext.Payment'  is null.");
    }
    var payment = await _context.Payments.FindAsync(id);
    if (payment != null)
    {
      _context.Payments.Remove(payment);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }


  private bool PaymentModelExists(int id)
  {
    return (_context.Payments?.Any(e => e.id_payment == id)).GetValueOrDefault();
  }




}
