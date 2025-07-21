using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace ThomasConstruction.Controllers;

[Authorize]
public class CustomerController : Controller
{

  private readonly ApplicationDbContext _context;

  public CustomerController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index(int? page)
  {
 int pageNumber = page ?? 1;
    int pageSize = 10;

    return _context.Customers != null ?
                              View(await _context.Customers.Include(c => c.state).ToListAsync()) :
                                Problem("Entity set 'ApplicationDbContext.Customers'  is null.");

  }


  // GET: Customers/Create
  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.States = new SelectList(_context.States, "id_state", "state_name");

    return View();
  }

  // POST: Customers/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CustomerModel customer)
  {
    //if (ModelState.IsValid)
    if (!string.IsNullOrWhiteSpace(customer.customer_name) && customer.id_state != 0)
    {
      _context.Add(customer);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.States = new SelectList(_context.States.ToList(), "id_state", "state_name");

    return View(customer);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.Customers == null)
    {
      return NotFound();
    }

    var customer = await _context.Customers.FindAsync(id);
    if (customer == null)
    {
      return NotFound();
    }
    //seleccionar el estado asociado a la persona

    var viewModel = new CustomerViewModel
    {
      id_customer = customer.id_customer,
      customer_name = customer.customer_name,
      phone = customer.phone,
      address = customer.address,
      city = customer.city,
      zip_code = customer.zip_code,
      id_state = customer.id_state, // valor que debe aparecer seleccionado
      states = _context.States
              .Select(s => new SelectListItem
              {
                Value = s.id_state.ToString(),
                Text = s.state_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: Customer/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, CustomerViewModel customer)
  {
    if (id != customer.id_customer)
    {
      return NotFound();
    }

    if (!string.IsNullOrWhiteSpace(customer.customer_name) && customer.id_state != 0)
    {
      try
      {
        var customerModel = _context.Customers.Find(customer.id_customer);
        customerModel.customer_name = customer.customer_name!;
        customerModel.phone = customer?.phone;
        customerModel.city = customer?.city;
        customerModel.address = customer?.address;
        customerModel.zip_code = customer?.zip_code;
        customerModel.id_state = customer.id_state!;


        _context.Update(customerModel);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CustomerModelExists(customer.id_customer))
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

    customer.states = _context.States.Select(s => new SelectListItem
    {
      Value = s.id_state.ToString(),
      Text = s.state_name
    }).ToList();

    return View(customer);



  }


  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.Customers == null)
    {
      return NotFound();
    }

    var customer = await _context.Customers
        .FirstOrDefaultAsync(m => m.id_customer == id);
    if (customer == null)
    {
      return NotFound();
    }

    var viewModel = new CustomerDetailsViewModel
    {
      id_customer = customer.id_customer,
      customer_name = customer.customer_name,
      phone = customer.phone,
      address = customer.address,
      city = customer.city,
      zip_code = customer.zip_code,
      // id_state = customer.id_state, // valor que debe aparecer seleccionado
      state = _context.States.Find(customer.id_state)?.state_name

    };




    return View(viewModel);
  }


  // GET: Customer/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null || _context.Customers == null)
    {
      return NotFound();
    }

    var customer = await _context.Customers
        .FirstOrDefaultAsync(m => m.id_customer == id);
    if (customer == null)
    {
      return NotFound();
    }

    return View(customer);
  }

  // POST: Customers/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    if (_context.Customers == null)
    {
      return Problem("Entity set 'ApplicationDbContext.Customers'  is null.");
    }
    var customer = await _context.Customers.FindAsync(id);
    //validar que no este asociado a ningun proyecto
    if (_context.Projects.Any(c => c.id_customer == id))
    {
      // ModelState.AddModelError("", "No se puede eliminar el estado porque tiene clientes asociados.");
      //return View(customer); // o redirigir a Details u otra vista con mensaje
      TempData["ErrorMessage"] = "Cannot be deleted because there are clients associated with projects.";
      return RedirectToAction("Details", new { id = id });
    }


    if (customer != null)
    {
      _context.Customers.Remove(customer);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }


  private bool CustomerModelExists(int id)
  {
    return (_context.Customers?.Any(e => e.id_customer == id)).GetValueOrDefault();
  }




}
