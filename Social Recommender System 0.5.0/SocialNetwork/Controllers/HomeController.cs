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

        public ActionResult About()
        {
            
            IUserRepository user = RepositoryLocator.GetRepository();
            //user.AddUser("Vitor", "vitor1987@hotmail.com", false);
            string[] terms = { "museu", "Barcelona" };
            user.AddMultiTerms(0, new LinkedList<string>(terms));
            AbotCrawler.Crawler.CrawlerInit();
            SocialNetwork.Models.ControlModule.PeriodicMaintenance();


            
            LuceneController.LuceneUsage.Arquive("Eu fui a Barcelona e vi lá um museu muita fixe", "www.vaiaomuseu.com");
            user.SearchLuceneDatabase(false);
            return View("SendEmail",user.updateUsers());

            
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

        //public ActionResult Profile(string user)
        //{
        //    return View();
        //}
    }
}
