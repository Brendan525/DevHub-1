using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevHub.Models;

namespace DevHub.Controllers
{
    public class DevHubController : Controller
    {
        //Open Questions View, 3 most recent?
        //has to have a search
        //add question button here goes to create
        public IActionResult Index(string search = "")
        {
            if(search == "")
            {
                ViewData["Message"] = "Recent Questions";
                return View(DataAccessor.GetRecentQuestions());
            }
            else
            {
                ViewData["Message"] = "Search Results";
                return View(DataAccessor.SearchByTag(search));
            }
        }

        //Archive, closed questions

        //create a question view
        
        //question detail, edit and submit on this page
        //answer change, add, submit all on same page

    }
}
