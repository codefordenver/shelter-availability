using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShelterAvailability.Data;
using Microsoft.EntityFrameworkCore;
using ShelterAvailability.Models.ShelterModels;
using System.Net;
using System.Xml;
using cloudscribe.HtmlAgilityPack;

namespace ShelterAvailability.Controllers
{
    public class SheltersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SheltersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Shelters
        public async Task<ActionResult> Index()
        {
            return View(await _dbContext.Shelters.ToListAsync());
        }

        // GET: Shelters/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Shelter shelter = await _dbContext.Shelters.SingleAsync(s => s.ShelterID == id);
            if (shelter == null)
            {
                return NotFound();
            }
            return View(shelter);
        }

        // GET: Shelters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Shelter shelter)
        {

            shelter.AvailabilityLastUpdated = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                _dbContext.Shelters.Add(shelter);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shelter);
        }

        // GET: Shelters/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Shelter shelter = await _dbContext.Shelters.SingleAsync(s => s.ShelterID == id);
            if (shelter == null)
            {
                return NotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                shelter.AvailabilityLastUpdated = DateTime.UtcNow;

                _dbContext.Entry(shelter).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shelter);
        }

        // GET: Shelters/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Shelter shelter = await _dbContext.Shelters.SingleAsync(s => s.ShelterID == id);
            if (shelter == null)
            {
                return NotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Shelter shelter = await _dbContext.Shelters.SingleAsync(s => s.ShelterID == id);
            _dbContext.Shelters.Remove(shelter);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Synchronize()
        {
            var url = "https://211colorado.communityos.org/z_eda/publicshelterassist.taf?function=list";

            var getHtmlWeb = new HtmlWeb();
            var document = await getHtmlWeb.LoadFromWebAsync(url);

            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//table[3]/tr/td/table/tr");


            if (nodes != null && nodes.Count > 1)
            {
                var shelters = new List<Shelter>();

                var header = nodes[0];
                nodes.RemoveAt(0);

                foreach (var node in nodes)
                {
                    var shelter = new Shelter();

                    List<string> columnValues = node.SelectNodes("td").Select(td => td.InnerText.Replace("&nbsp;", " ").Trim()).ToList();

                    shelter.Name = columnValues[0];
                    shelter.CurrentPopulation = intParseDefault(columnValues[4], 0);
                    shelter.SingleSpacesAvailable = intParseDefault(columnValues[5], 0);
                    shelter.FamilySpacesAvailable = intParseDefault(columnValues[6], 0);
                    shelter.CurrentTotalSpaces = intParseDefault(columnValues[7], 0);
                    shelter.ShelterID = columnValues[12];

                    shelter.AvailabilityLastUpdated = DateTime.UtcNow;

                    shelters.Add(shelter);
                }

                _dbContext.Shelters.RemoveRange(_dbContext.Shelters);
                _dbContext.SaveChanges();
                _dbContext.Shelters.AddRange(shelters);

                _dbContext.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        private int intParseDefault(string p1, int p2)
        {
            int value = p2;

            int.TryParse(p1, out value);

            return value;
        }
    }
}