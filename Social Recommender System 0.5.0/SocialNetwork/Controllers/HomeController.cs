using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using HtmlAgilityPack;



namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Simulate() {

            //IUserRepository user = RepositoryLocator.GetRepository();
            //user.SearchLuceneDatabase(false);
            ControlModule.PeriodicMaintenance();
            return RedirectToAction("Index");
            //return View("SendEmail", user.updateUsers());
        
        }

        public ActionResult About()
        {

            return RedirectToAction("Index");
            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search() {


            return View();
        
        }
        [HttpPost]
        public ActionResult BeginSearch(string term) {

            string[] terms = term.Split(' ');
            IEnumerable<string> result= RepositoryLocator.GetRepository().FindTerms(terms);
            ViewData["list"]= result;
            return View();
        }

        public ActionResult Profile()
        {
           return View(RepositoryLocator.GetRepository().GetUser(0));
        }

        public ActionResult ChangeTerms(int id) {

            return View(RepositoryLocator.GetRepository().GetUser(id));
        }
        [HttpPost]
        public ActionResult ChangeTerms(int id, string terms) {
            LinkedList<string> newTermList = new LinkedList<string>();
            IUserRepository repo= RepositoryLocator.GetRepository();

            foreach (string s in terms.Split(' ')) newTermList.AddLast(s);
            repo.GetUser(id).ResetTastes(newTermList);
            return RedirectToAction("Profile");
        
        }
    }
}
