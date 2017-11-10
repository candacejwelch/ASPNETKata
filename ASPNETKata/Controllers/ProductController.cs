using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SqlIntro;

namespace ASPNETKata.Controllers
{
    public class ProductController : Controller
    {
        [Dependency]
        public IProductRepository ProductRepository { get; set; }



        // GET: Product
        public ActionResult Index()
        {   
            var list = ProductRepository.GetProducts();
            return View(list);
        }

        //GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var list = ProductRepository.GetDetails(id);
            return View(list);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            ProductRepository.InsertProduct(product);
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {

            product.ProductId = id;
            ProductRepository.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            ProductRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
