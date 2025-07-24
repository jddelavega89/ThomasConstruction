using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ThomasConstruction.Controllers;

[Authorize]
public class SubContractorController : Controller
{

  private readonly ApplicationDbContext _context;

  public SubContractorController(ApplicationDbContext context)
  {
    _context = context;
  }




  public async Task<IActionResult> Index(int? id_project)
  {

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
    var subQuery = _context.SubContractors.Include(p => p.project).AsQueryable();

    // Aplica el filtro si se seleccionó un proyecto
    if (id_project.HasValue)
    {
        subQuery = subQuery.Where(p => p.id_project == id_project.Value);
    }

    // Carga los proyectos para el filtro
    var projects = await _context.Projects.ToListAsync();
    ViewBag.Projects = new SelectList(projects, "id_project", "project_name",id_project);
    ViewBag.SelectedProjectId = id_project;

    // Ejecuta la consulta
    var subc = await subQuery.ToListAsync();

    return View(subc);

  }


  // GET: SubContractor/Create
  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.Projects = new SelectList(_context.Projects, "id_project", "project_name");

    return View();
  }

  // POST: SubContractor/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(SubcontractorModel subContractor)
  {
    //if (ModelState.IsValid)
    if (subContractor.id_project != 0)
    {
      _context.Add(subContractor);

      //actualizar el costo del proyecto
      var project = await _context.Projects
           .FirstOrDefaultAsync(p => p.id_project == subContractor.id_project);

      if (project != null)
      {
        project.cost += subContractor.cost;
        _context.Update(project);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");

    return View(subContractor);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.SubContractors == null)
    {
      return NotFound();
    }

    var subContractor = await _context.SubContractors.FindAsync(id);
    if (subContractor == null)
    {
      return NotFound();
    }
    //seleccionar el proyecto asociado a la persona

    var viewModel = new SubContractorViewModel
    {
      id_subcontractor = subContractor.id_subcontractor,
      date_subc = subContractor.date_subc,
      cost = subContractor.cost,
      details = subContractor.details,

      id_project = subContractor.id_project, // valor que debe aparecer seleccionado
      projects = _context.Projects
              .Select(s => new SelectListItem
              {
                Value = s.id_project.ToString(),
                Text = s.project_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: SubContractor/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, SubContractorViewModel subContractor)
  {
    if (id != subContractor.id_subcontractor)
    {
      return NotFound();
    }

    if (subContractor.id_subcontractor != 0)
    {

      var subcBeforeU = await _context.SubContractors
   .AsNoTracking() // importante para evitar conflicto de tracking
   .FirstOrDefaultAsync(c => c.id_subcontractor == subContractor.id_subcontractor);

      try
      {
        var SubcontractorModel = _context.SubContractors.Find(subContractor.id_subcontractor);
        SubcontractorModel.date_subc = subContractor.date_subc!;
        SubcontractorModel.id_project = subContractor.id_project!;
        SubcontractorModel.cost = subContractor.cost;
        SubcontractorModel.details = subContractor.details;


        _context.Update(SubcontractorModel);

        var project = await _context.Projects
           .FirstOrDefaultAsync(p => p.id_project == subContractor.id_project);
        if (project != null)
        {
          double diff = subContractor.cost - (subcBeforeU != null ? subcBeforeU.cost : 0);
          project.cost += diff;
          _context.Update(project);

        }

        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SubcModelExists(subContractor.id_subcontractor))
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

    subContractor.projects = _context.Projects.Select(s => new SelectListItem
    {
      Value = s.id_project.ToString(),
      Text = s.project_name
    }).ToList();

    return View(subContractor);



  }


  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.SubContractors == null)
    {
      return NotFound();
    }

    var subContractor = await _context.SubContractors
        .FirstOrDefaultAsync(m => m.id_subcontractor == id);
    if (subContractor == null)
    {
      return NotFound();
    }

    var viewModel = new SubContractorDetailsViewModel
    {
      id_subcontractor = subContractor.id_subcontractor,
      date_subc = subContractor.date_subc,
      cost = subContractor.cost,
      details = subContractor.details,
      project = _context.Projects.Find(subContractor.id_project)?.project_name

    };




    return View(viewModel);
  }


  // GET: SubContractor/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null || _context.SubContractors == null)
    {
      return NotFound();
    }

    var subContractor = await _context.SubContractors.Include(c => c.project)
        .FirstOrDefaultAsync(m => m.id_subcontractor == id);
    if (subContractor == null)
    {
      return NotFound();
    }

    return View(subContractor);
  }

  // POST: SubContractor/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    if (_context.SubContractors == null)
    {
      return Problem("Entity set 'ApplicationDbContext.SubContractor'  is null.");
    }
    var subContractor = await _context.SubContractors.FindAsync(id);
    if (subContractor != null)
    {
      _context.SubContractors.Remove(subContractor);
    }

    //actualizar el costo del proyecto
    var project = await _context.Projects
        .FirstOrDefaultAsync(p => p.id_project == subContractor.id_project);

    if (project != null)
    {
      project.cost -= subContractor.cost;
      _context.Update(project);
    }


    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }


  private bool SubcModelExists(int id)
  {
    return (_context.SubContractors?.Any(e => e.id_subcontractor == id)).GetValueOrDefault();
  }




}
