using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication6.Models;
using WebApplication6.MyDatabase;
using WebApplication6.ViewModels.Skor;

namespace WebApplication6.model
{
    public class SkorController : Controller
    {
        public ActionResult Index(int id, string sort)
        {
            var model = new SkorIndexViewModel();

            using (var db = new SkorModelDB())
            {
                model.CategoryName = string.Join("", db.CategoriesTable.Where(x => x.CategoryId == id).Select(x => x.CategoryName));
                model.CategoryId = id;
                model.SkorList.AddRange(db.SkorTable.Select(p => new SkorIndexViewModel.SkorListViewModel
                {
                    ID = p.SkorId,
                    Name = p.Name,
                    Model = p.Model,
                    Color = p.Color,
                    Size = p.Size,
                    Price = p.Price,
                    Description = p.Description,
                    CategoryId = p.CategoryId
                }).Where(p => p.CategoryId == id));

                model = Sort(model, sort);
                return View(model);
            }
        }


        public ActionResult AllSkor()
        {
            var model = new SkorIndexViewModel();

            using (var db = new SkorModelDB())
            {
                model.SkorList.AddRange(db.SkorTable.Select(x => new SkorIndexViewModel.SkorListViewModel
                {
                    ID = x.SkorId,
                    Name = x.Name,
                    Model = x.Model,
                    Color = x.Color,
                    Size = x.Size,
                    Price = x.Price,
                    Description = x.Description,
                    CategoryId = x.CategoryId
                }));

                return View(model);
            }
        }


        public ActionResult ViewSkor(int id, string sort)
        {
            var model = new SkoViewModel();

            using (var db = new SkorModelDB())
            {
                var sko = db.SkorTable.Find(id);
                model.CategoryId = sko.CategoryId;
                model.CategoryName = sko.Category.CategoryName;
                model.Name = sko.Name;
                model.Model = sko.Model;
                model.Color = sko.Color;
                model.Size = sko.Size;
                model.Price = sko.Price;
                model.Description = sko.Description;

                return View(model);
            }
        }

        public ActionResult Search(string search, string sort)
        {
            var model = new SkorIndexViewModel();
            model.Search = search;

            if (!string.IsNullOrEmpty(search))
            {
                using (var db = new SkorModelDB())
                {
                    model.SkorList.AddRange(db.SkorTable
                        .Select(x => new SkorIndexViewModel.SkorListViewModel
                        {
                            ID = x.SkorId,
                            Name = x.Name,
                            Model = x.Model,
                            Size = x.Size,
                            Color = x.Color,
                            Price = x.Price,
                            Description = x.Description,
                            CategoryId = x.CategoryId
                        }));

                    model.SkorList = model.SkorList.Where(x => x.Name.ToUpper().Contains(search.ToUpper())).ToList();

                    model = Sort(model, sort);

                    return View("Search", model);
                }
            }

            return View("Search", model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Edit(int? id)
        {
            using (var db = new SkorModelDB())
            {
                var product = db.SkorTable.Find(id);
                var model = new SkoViewModel
                {
                    CategoryName = product.Category.CategoryName,
                    CategoryId = product.CategoryId,
                    SkorId = product.SkorId,
                    Name = product.Name,
                    Model = product.Model,
                    Color = product.Color,
                    Size = product.Size,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryDropDownList = DropDownCategories()
                };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Edit(SkoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { id = model.CategoryId });
            }

            using (var db = new SkorModelDB())
            {
                var product = db.SkorTable.FirstOrDefault(x => x.CategoryId == model.CategoryId);

                product.SkorId = model.SkorId;
                product.Name = model.Name;
                product.Model = model.Model;
                product.Color = model.Color;
                product.Size = model.Size;
                product.Price = model.Price;
                product.Description = model.Description;
                product.CategoryId = model.CategoryId;


                db.SaveChanges();
                return RedirectToAction("Index", new { id = product.CategoryId });
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult Create(int? id)
        {
            var model = new SkoViewModel { CategoryId = (int)id };
            using (var db = new SkorModelDB())
            {
                model.CategoryName = string.Join("", db.CategoriesTable.Where(x => x.CategoryId == id).Select(x => x.CategoryName));
                model.CategoryId = (int)id;
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SkorModelDB())
            {
                var skor = new SkorModel
                {
                    SkorId = model.SkorId,
                    Name = model.Name,
                    Model = model.Model,
                    Color = model.Color,
                    Size = model.Size,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryId = model.CategoryId
                };

                db.SkorTable.Add(skor);
                db.SaveChanges();
                return RedirectToAction("Index", "Skor", new { id = model.CategoryId });
            }
        }


        [Authorize(Roles = "Admin, ProductManager")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (var db = new SkorModelDB())
            {
                var prod = db.SkorTable.Find(id);
                var model = new SkoViewModel
                {
                    SkorId = prod.SkorId,
                    Name = prod.Name,
                    Model = prod.Model,
                    Color = prod.Color,
                    Size = prod.Size,
                    Price = prod.Price,
                    Description = prod.Description
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
                return RedirectToAction("Index", "Home");
            }
            using (var db = new SkorModelDB())
            {
                var obj = db.SkorTable.Find(id);
                if (obj != null)
                {
                    db.SkorTable.Remove(obj);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Sko", new { id = obj.CategoryId });
                }
                return RedirectToAction("Index", "Home");
            }
        }


        public SkorIndexViewModel Sort(SkorIndexViewModel model, string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                sort = "SkorNameAsc";
            }
            model.SortOrder = sort == "SkorNameDesc" ? "SkorNameAsc" : "SkorNameDesc";

            switch (sort)
            {
                case "SkorNameAsc":
                    model.SkorList = model.SkorList.OrderBy(x => x.Name).ToList();
                    break;
                case "SkorNameDesc":
                    model.SkorList = model.SkorList.OrderByDescending(x => x.Name).ToList();
                    break;
            }

            return model;
        }


        public List<SelectListItem> DropDownCategories()
        {
            var model = new SkoViewModel();
            model.CategoryDropDownList = new List<SelectListItem>();
            using (var db = new SkorModelDB())
            {
                foreach (var category in db.CategoriesTable)
                {
                    model.CategoryDropDownList.Add(new SelectListItem { Value = category.CategoryId.ToString(), Text = category.CategoryName });
                }

                return model.CategoryDropDownList;
            }
        }
    }
}
 




        


    
    



