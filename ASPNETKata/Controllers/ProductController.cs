using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETKata.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace ASPNETKata.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var list = conn.Query<Product>("select * from product ORDER BY ProductId DESC");
                return View(list);
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var prod = new Product
            {
                Name = collection["Name"],
                //ProductId = int.Parse(collection["ProductId"])
            };
            
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute("INSERT into product (Name) values (@Name)", prod);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return View();
                }
            }
            
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var prod = new Product
            {
                Name = collection["Name"]
            };
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute("update product set name = @name where ProductID = @id",
                        new {id, name = prod.Name});
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            
            //var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            //using (var conn = new MySqlConnection(connectionString))
            //{
            //    conn.Query("select (Name, ProductId) from product where ProductID = @id")
            //}
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var productId = id;
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute("delete from product where ProductID = @id", new { id = productId });
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }
    }
}
