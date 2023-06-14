using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApplicationRedir.Models;

namespace WebApplicationRedir.Controllers
{
    public class SearchController : Controller
    {
        // GET: SearchController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string criteria)
        {
            ViewBag.SearchResults = criteria;
            return View();
            //return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> Photos()
        {
            IEnumerable<PhotoModel> listPhotos = new System.Collections.Generic.List<PhotoModel>();
            using (var reader = System.IO.File.OpenText("photos.json"))
            {
                var fileText = await reader.ReadToEndAsync();
                listPhotos = JsonSerializer.Deserialize<IEnumerable<PhotoModel>>(fileText);
                
                Int64 TotalCount = listPhotos.Count();
                DataTableResponse dtResponse = new DataTableResponse();
                dtResponse.draw = 10;
                dtResponse.recordsTotal = TotalCount;
                dtResponse.recordsFiltered = TotalCount;
                dtResponse.data = listPhotos;

                return Json(dtResponse);
            }
        }

        // GET: SearchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: SearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchController/Edit/5
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

        // GET: SearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchController/Delete/5
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

    internal class DataTableResponse
    {
        public int draw { get; set; }
        public Int64 recordsTotal { get; set; }
        public Int64 recordsFiltered { get; set; }
        public IEnumerable<PhotoModel> data { get; set; }
        public DataTableResponse() { }
    }
}
