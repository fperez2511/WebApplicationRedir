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
            // number of times a request has been made?
            int requestedDraw = Convert.ToInt32(Request.Form["draw"].FirstOrDefault() ?? "0");

            // how many to return
            int requestedLength = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");

            // how many to skip aka where to start
            int requestedStart = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            // The search text
            string requestedSearch = Request.Form["search[value]"].FirstOrDefault() ?? "";

            // Simulate data retrieval
            IEnumerable<PhotoModel> listPhotos = new System.Collections.Generic.List<PhotoModel>();
            using (var reader = System.IO.File.OpenText("photos.json"))
            {
                var fileText = await reader.ReadToEndAsync();
                listPhotos = JsonSerializer.Deserialize<IEnumerable<PhotoModel>>(fileText);
            }
            // select * from Model
            IQueryable<PhotoModel> queryPhotos = listPhotos.AsQueryable();
            // Total de registros antes de filtrar.
            Int64 TotalCount = listPhotos.Count();
            // Build query for filtering by searching using the criteria against the concatenation of all fields 
            queryPhotos = queryPhotos
                .Where(x => string.Concat(x.AlbumId, x.Id, x.Title, x.Url, x.ThumbnailUrl).Contains(requestedSearch));
            // Total de registros ya filtrados.
            int TotalFiltered = queryPhotos.Count(); 
            // Save the new list to return
            listPhotos = queryPhotos.Skip(requestedStart).Take(requestedLength).ToList();
            // Build the response payload
            DataTableResponse dtResponse = new DataTableResponse();
            dtResponse.draw = requestedDraw;
            dtResponse.recordsTotal = TotalCount;
            dtResponse.recordsFiltered = TotalFiltered;
            dtResponse.data = listPhotos;

            return Json(dtResponse);
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
