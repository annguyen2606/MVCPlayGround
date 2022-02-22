using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCPlayground.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPlayground.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly static List<Item> _items = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PrintTraceLog();
            return View(_items);
        }

        public IActionResult Create() 
        {
            PrintTraceLog();
            return View();
        }

        public IActionResult CreateItem(Item item) 
        {
            PrintTraceLog();
            item.Id = Guid.NewGuid().ToString();
            _items.Add(item);
            _logger.LogInformation($"Item [Id = {item.Id}] added");
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            PrintTraceLog();
            var item = _items.FirstOrDefault(i=> i.Id == id);
            return View(item);
        }

        public IActionResult EditItem(Item item)
        {
            _items[_items.FindIndex(i => i.Id == item.Id)] = item;
            _logger.LogInformation($"Item - [Id = {item.Id}] updated");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id) 
        {
            _items.RemoveAt(_items.FindIndex(i => i.Id == id));
            _logger.LogInformation($"Item - [Id = {id}] remove - List count: {_items.Count}");
            return RedirectToAction("Index");
        }

        public IActionResult Details(string id)
        {
            _logger.LogInformation($"Request at {ControllerContext.HttpContext.Request.Path}");
            var item = _items.FirstOrDefault(i => i.Id == id);
            return View(item);
        }

        private void PrintTraceLog() 
        {
            _logger.LogInformation($"Request at {ControllerContext.HttpContext.Request.Path}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
