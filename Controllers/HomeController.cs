using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DevHub.Models;

namespace DevHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Test()
        //{
        //    //DataAccessor.Save(new Question() { id=0, category="Specific", detail="Ich habe eine frage", posted=DateTime.Now, status=0, title="Das Frage", username="therick", tags="#German#Language" } );

        //    //DataAccessor.Delete(5);

        //    List<Question> test1 = new List<Question>() { DataAccessor.GetQuestion(4)};
            

        //    return View(test1);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
