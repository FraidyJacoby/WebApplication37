using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication37.Data;
using WebApplication37.Models;

namespace WebApplication37.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {
            var db = new PeopleDb(_connectionString);
            var vm = new IndexViewModel
            {
                People = db.GetPeople()
            };
            return Json(vm);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var db = new PeopleDb(_connectionString);
            db.AddPerson(person);
        }

        [HttpPost]
        public void EditPerson(Person person)
        {
            var db = new PeopleDb(_connectionString);
            db.EditPerson(person);
        }

        [HttpPost]
        public void DeletePerson(DeleteViewModel vm)
        {
            var db = new PeopleDb(_connectionString);
            db.DeletePerson(vm.Id);
        }
    }
}
