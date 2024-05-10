using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using to_do_list_project.Data;
using to_do_list_project.Dtos;
using to_do_list_project.Models;

namespace to_do_list_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoContext context;

        public HomeController(ToDoContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index(string id)
        {
            var iddd = HttpContext.Session.GetString("Id");
            if(iddd == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var filters = new Filters(id);
            ViewBag.Filters = filters;

            ViewBag.Categories = context.Catigories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.Catigory)
                .Include(t => t.Status).Where(x=>x.UserId == iddd);

            if (filters.HasCategory)
            {
                query = query.Where(t => t.CatigoryId == filters.CategoryId);
            }
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }

            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate < today);
                }
                else if (filters.IsFuture)
                {
                    query = query.Where(t => t.DueDate > today);
                }
                else if (filters.IsToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }

            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();
            return View(tasks);
        }
        [HttpGet]
        public IActionResult add()
        {
            var iddd = HttpContext.Session.GetString("Id");
            if (iddd == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Categories = context.Catigories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            var task = new ToDoDto { StatusId = "open" };
            return View(task);
        }

        [HttpPost]
        public IActionResult add(ToDoDto model)
        {
            var iddd = HttpContext.Session.GetString("Id");
            if (iddd == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var task = new ToDo();
            task.StatusId = model.StatusId;
            task.DueDate = model.DueDate;
            task.CatigoryId = model.CatigoryId;
            task.Descriotion = model.Descriotion;
            task.UserId = iddd;
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = context.Catigories.ToList();
                ViewBag.Statuses = context.Statuses.ToList();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            var iddd = HttpContext.Session.GetString("Id");
            if (iddd == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            var iddd = HttpContext.Session.GetString("Id");
            if (iddd == null)
            {
                return RedirectToAction("Login", "Account");
            }
            selected = context.ToDos.Find(selected.Id);
            if (selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]

        public IActionResult DeleteComplete(string id)
        {
            var iddd = HttpContext.Session.GetString("Id");
            if (iddd == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var toDelete = context.ToDos.Where(t => t.StatusId == "closed").ToList();

            foreach (var task in toDelete)
            {
                context.ToDos.Remove(task);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }

    }
}