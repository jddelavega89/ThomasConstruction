using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using ThomasConstruction.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ThomasConstruction.Controllers;

[Authorize]
public class WorkerSalaryController : Controller
{

  private readonly ApplicationDbContext _context;

  public WorkerSalaryController(ApplicationDbContext context)
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
    var workerQuery = _context.WorkerSalarys.Include(p => p.project).Include(p => p.worker).AsQueryable();

    // Aplica el filtro si se seleccionó un proyecto
    if (id_project.HasValue)
    {
        workerQuery = workerQuery.Where(p => p.id_project == id_project.Value);
    }

    // Carga los proyectos para el filtro
    var projects = await _context.Projects.ToListAsync();
    ViewBag.Projects = new SelectList(projects, "id_project", "project_name",id_project);
    ViewBag.SelectedProjectId = id_project;

    // Ejecuta la consulta
    var workers = await workerQuery.ToListAsync();

    return View(workers);


    //    return _context.WorkerSalarys != null ?
    //                         View(await _context.WorkerSalarys.Include(c => c.project).Include(c => c.bill).ToListAsync()) :
    //                          Problem("Entity set 'ApplicationDbContext.Bills'  is null.");

  }

  public IActionResult Create()
  {
    // Crear SelectList y asignarlo a ViewBag
    ViewBag.Projects = new SelectList(_context.Projects, "id_project", "project_name");
    ViewBag.Workers = new SelectList(_context.Workers.Where(p => p.active == true), "id_worker", "worker_name");

    return View();
  }

  // POST: WorkerSalary/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(WorkerSalaryModel workerSalary)
  {
    //if (ModelState.IsValid)
    if (workerSalary.id_project != 0 && workerSalary.id_worker != 0)
    {
      _context.Add(workerSalary);

       //actualizar el costo del proyecto
      var project = await _context.Projects
           .FirstOrDefaultAsync(p => p.id_project == workerSalary.id_project);

      if (project != null)
      {
        project.cost += workerSalary.price_hour * workerSalary.work_hours;
        _context.Update(project);
      }
    
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    ViewBag.Projects = new SelectList(_context.Projects.ToList(), "id_project", "project_name");
    ViewBag.Workers = new SelectList(_context.Workers.Where(p => p.active == true).ToList(), "id_worker", "worker_name");

    return View(workerSalary);
  }

  public async Task<IActionResult> Details(int? id)
  {
    if (id == null || _context.WorkerSalarys == null)
    {
      return NotFound();
    }

    var workerSalary = await _context.WorkerSalarys
        .FirstOrDefaultAsync(m => m.id_salary == id);
    if (workerSalary == null)
    {
      return NotFound();
    }

    var viewModel = new WorkerSalaryDetailsViewModel
    {
      id_salary = workerSalary.id_salary,
      salary_date = workerSalary.salary_date,
      price_hour = workerSalary.price_hour,
      work_hours = workerSalary.work_hours,
      salary = workerSalary.salary,
      project = _context.Projects.Find(workerSalary.id_project)?.project_name,
      worker = _context.Workers.Find(workerSalary.id_worker)?.worker_name

    };




    return View(viewModel);
  }


  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null || _context.WorkerSalarys == null)
    {
      return NotFound();
    }

    var workerSalary = await _context.WorkerSalarys.FindAsync(id);
    if (workerSalary == null)
    {
      return NotFound();
    }
    //seleccionar el proyecto asociado a la persona

    var viewModel = new WorkerSalaryViewModel
    {
      id_salary = workerSalary.id_salary,
      salary_date = workerSalary.salary_date,
      price_hour = workerSalary.price_hour,
      work_hours = workerSalary.work_hours,
      salary = workerSalary.salary,

      id_project = workerSalary.id_project, // valor que debe aparecer seleccionado
      id_worker = workerSalary.id_worker, // valor que debe aparecer seleccionado
      projects = _context.Projects
              .Select(s => new SelectListItem
              {
                Value = s.id_project.ToString(),
                Text = s.project_name
              }).ToList(),

      workers = _context.Workers.Where(p => p.active == true || p.id_worker == workerSalary.id_worker)
              .Select(s => new SelectListItem
              {
                Value = s.id_worker.ToString(),
                Text = s.worker_name
              }).ToList()
    };


    return View(viewModel);
  }

  // POST: Payment/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, WorkerSalaryViewModel workerSalary)
  {
    if (id != workerSalary.id_salary)
    {
      return NotFound();
    }

    if (workerSalary.id_salary != 0)
    {
          var workerSalaryBeforeU = await _context.WorkerSalarys
  .AsNoTracking() // importante para evitar conflicto de tracking
  .FirstOrDefaultAsync(c => c.id_salary == workerSalary.id_salary);

      try
      {
        var WorkerSalaryModel = _context.WorkerSalarys.Find(workerSalary.id_salary);
        WorkerSalaryModel.salary_date = workerSalary.salary_date!;
        WorkerSalaryModel.id_project = workerSalary.id_project!;
        WorkerSalaryModel.id_worker = workerSalary.id_worker!;
        WorkerSalaryModel.price_hour = workerSalary.price_hour;
        WorkerSalaryModel.work_hours = workerSalary.work_hours;

        _context.Update(WorkerSalaryModel);

         var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.id_project == workerSalary.id_project);
        if (project != null)
        {
          double diff = (workerSalary.price_hour * workerSalary.work_hours) - (workerSalaryBeforeU != null ? workerSalaryBeforeU.salary : 0);
          project.cost += diff;
          _context.Update(project);

        }

        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!WorkerSalaryModelExists(workerSalary.id_salary))
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

    workerSalary.projects = _context.Projects.Select(s => new SelectListItem
    {
      Value = s.id_project.ToString(),
      Text = s.project_name
    }).ToList();

    workerSalary.workers = _context.Workers.Select(s => new SelectListItem
    {
      Value = s.id_worker.ToString(),
      Text = s.worker_name
    }).ToList();

    return View(workerSalary);



  }


  // GET: WorkerSalary/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null || _context.WorkerSalarys == null)
    {
      return NotFound();
    }

    var workerSalary = await _context.WorkerSalarys.Include(c => c.project).Include(c => c.worker)
        .FirstOrDefaultAsync(m => m.id_salary == id);
    if (workerSalary == null)
    {
      return NotFound();
    }

    return View(workerSalary);
  }

  // POST: Payment/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    if (_context.WorkerSalarys == null)
    {
      return Problem("Entity set 'ApplicationDbContext.WorkerSalary'  is null.");
    }
    var workerSalary = await _context.WorkerSalarys.FindAsync(id);
    if (workerSalary != null)
    {
      _context.WorkerSalarys.Remove(workerSalary);
    }

    //actualizar el costo del proyecto
    var project = await _context.Projects
         .FirstOrDefaultAsync(p => p.id_project == workerSalary.id_project);

    if (project != null)
    {
      project.cost -= workerSalary.price_hour * workerSalary.work_hours;
      _context.Update(project);
    }
   

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }


  private bool WorkerSalaryModelExists(int id)
  {
    return (_context.WorkerSalarys?.Any(e => e.id_salary == id)).GetValueOrDefault();
  }







}
