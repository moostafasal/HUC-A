using HUc.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HUc.Controllers
{
    public class SectionController : Controller
    {
        HosinOldTestingContext context = new HosinOldTestingContext();

        // GET: SectionController
        public ActionResult Index(int? page, string category)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            IQueryable<DepartmentsDepartment> departmentsQuery = context.DepartmentsDepartments;

            if (!string.IsNullOrEmpty(category) && category != "الكل")
            {
                departmentsQuery = departmentsQuery.Where(x => x.Type == category);
            }

            var departments = departmentsQuery.Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToList();

            var totalItems = departmentsQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            SelectList DepartmentSelectList = new SelectList(context.DepartmentsDepartments.ToList());
            ViewBag.allDepartments = DepartmentSelectList;
            ViewBag.Type = category;
            ViewBag.TotalPages = totalPages;

            return View(departments);
        }
        public ActionResult GetDistinctTypes(string Type)
        {
            ViewBag.Type = Type;
            using (var context = new HosinOldTestingContext())
            {
                var distinctTypes = context.DepartmentsDepartments.Select(x => x.Type).Distinct().ToList();
                return View("Index", distinctTypes);
            }
        }

        public ActionResult Category(string category, int? page)
        {
            ViewBag.Type = category;
            var data = GetDataByCategory(category);

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var departments = ((List<DepartmentsDepartment>)data).Skip((pageNumber - 1) * pageSize)
                                                                .Take(pageSize)
                                                                .ToList();

            return RedirectToAction("Index", new { page = pageNumber, category = category });
        }

        private object GetDataByCategory(string category)
        {
            var data = new List<DepartmentsDepartment>();
            if (category == "الكل")
            {
                data = context.DepartmentsDepartments.ToList();

            }
            else
            {
                data = context.DepartmentsDepartments.Where(x => x.Type == category).ToList();

            }
            return data;
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        // GET: UserController/Create
        public ActionResult Create(string name, string enName, int Level, string Type, string ConfessionBook, string Number, string Short, int RegCounterEving, string image, string RegCounterMoring)
        {
            DepartmentsDepartment collection = new DepartmentsDepartment()
            {

                Image = image,
                Name = name,
                NameEn = enName,
                ConfessionBook = ConfessionBook,
                Number = Number,
                Short = Short,
                RegCounterEving = RegCounterEving,
                RegCounterMoring = RegCounterMoring,
                Level = Level,
                Type = Type,
                CreatedDate = DateTime.Now,

            };
            context.DepartmentsDepartments.Add(collection);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
