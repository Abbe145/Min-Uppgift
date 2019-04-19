using System.Linq;
using System.Web.Mvc;
using WebApplication6.MyDatabase;
using WebApplication6.ViewModels.Category;

namespace WebApplication6.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            var model = new CategoryIndexViewModel();

            using (var db = new SkorModelDB())
            {
                model.Categories.AddRange(db.CategoriesTable.Select(c => new CategoryIndexViewModel.CategoryListViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }));
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Edit(int? id)
        {
            using (var db = new SkorModelDB())
            {
                var category = db.CategoriesTable.FirstOrDefault(p => p.CategoryId == id);

                var model = new CategoryViewModel

                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
                return View(model);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin, ProductManager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SkorModelDB())
            {
                var category = db.CategoriesTable.FirstOrDefault(r => r.CategoryId == model.CategoryId);
                category.CategoryId = model.CategoryId;
                category.CategoryName = model.CategoryName;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ProductManager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SkorModelDB())
            {
                var cat= new Models.CategoryModel
                {
                    CategoryName = model.CategoryName
                };

                db.CategoriesTable.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Delete(int? id)
        {
            using (var db = new SkorModelDB())
            {
                var category = db.CategoriesTable.Find(id);
                var model = new CategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
                return View(model);
            }
        }


        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            using (var db = new SkorModelDB())
            {
                var obj = db.CategoriesTable.Find(id);
                if (obj != null)
                {
                    db.CategoriesTable.Remove(obj);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}
