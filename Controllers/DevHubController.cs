using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevHub.Models;
using Microsoft.AspNetCore.Razor.Language;

namespace DevHub.Controllers
{
    public class DevHubController : Controller
    {
        //Open Questions View, 3 most recent?
        //has to have a search
        //add question button here goes to create
        public IActionResult Index(string search = "", string column = "")
        {
            ViewData["category"] = DataAccessor.GetCategory();
            ViewData["tags"] = DataAccessor.GetTags();
                
            if(search == "")
            {
                ViewData["Column"] = "tags";
                ViewData["Message"] = "Recent Questions";
                return View(DataAccessor.GetRecentQuestions());
            }
            else
            {
                ViewData["Column"] = column;
                ViewData["Message"] = "Search Results";
                return View(DataAccessor.SearchQuestions(column, search));
            }

        }

        //Archive, closed questions

        //create a question view
        
        //question detail, edit and submit on this page
        //answer change, add, submit all on same page
        [HttpGet]
        public IActionResult Detail(long id)
        {
            Question q = DataAccessor.GetQuestion(id);
            q.answers = DataAccessor.GetQuestionAnswers(id);
            return View(q);
        }

        [HttpPost]
        public IActionResult Detail(long id, string category, string title, int status, string tags, string detail  )
        {
            Question q = DataAccessor.GetQuestion(id);
            q.category = category;
            q.title = title;
            q.status = status;
            q.tags = tags;
            q.detail = detail;
            DataAccessor.SaveQuestion(q);
            return RedirectToAction("detail", "devhub", new { id = id });
        }

        public IActionResult PostQuestion()
        {
            if(HttpContext.Request.Cookies["Username"] is null || HttpContext.Request.Cookies["Username"] == "")
            {
                return RedirectToAction("Index", "DevHub");
            }
            ViewData["Today"] = DateTime.Now.Date.ToShortDateString();
            return View();
        }

        public IActionResult Create(long id, string category, string title, int status, string tags, string detail, string username )
        {
            Question q = new Question();
            q.category = category;
            q.title = title;
            q.status = status;
            q.tags = tags;
            q.detail = detail;
            q.username = username;
            q.posted = DateTime.Now;
            q.id = 0;
            DataAccessor.SaveQuestion(q);
            return RedirectToAction("detail", "devhub", new { id = q.id });
        }

        public IActionResult UpdateAnswer( int answer_id,  string answer_detail, string update)
        {
            Answer a = DataAccessor.GetAnswer(answer_id);
            if (update == "Delete")
            {
                long q_id = a.question_id;
                DataAccessor.DeleteAnswer(answer_id);
                return RedirectToAction("Detail", "DevHub", new { id = q_id });
            }
            else
            {
                a.detail = answer_detail;
                DataAccessor.SaveAnswer(a);
                return RedirectToAction("Detail", "DevHub", new { id = a.question_id });
            }
        }

        public IActionResult Upvote(int answer_upvote)
        {
            Answer a = DataAccessor.GetAnswer(answer_upvote);
            a.upvote++;
            DataAccessor.SaveAnswer(a);
            return RedirectToAction("Detail","DevHub", new { id = a.question_id });
        }

        public IActionResult CreateAnswer(string answer_username, string answer_detail, long question_id)
        {
            Answer a = new Answer();
            a.id = 0;
            a.posted = DateTime.Now;
            a.detail = answer_detail;
            a.username = answer_username;
            a.upvote = 0;
            a.question_id = question_id;
            DataAccessor.SaveAnswer(a);
            return RedirectToAction("Detail", "DevHub", new { id = a.question_id });
        }

        [HttpPost]
        public IActionResult Login(string username)
        {
            if(username is null || username == "")
            {
                return RedirectToAction("Index", "DevHub");
            }
            username = username.ToLower();
            HttpContext.Response.Cookies.Append("Username", username);
            return RedirectToAction("Index", "DevHub");
        }
    }
}
