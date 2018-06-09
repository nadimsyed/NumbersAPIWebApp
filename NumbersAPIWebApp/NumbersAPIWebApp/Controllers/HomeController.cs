using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NumbersAPIWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult DateFact(string Day, string Month)
        {
            int day = int.Parse(Day);
            int month = int.Parse(Month);

            HttpWebRequest request =
                       WebRequest.CreateHttp($"https://numbersapi.p.mashape.com/{day}/{month}/date?json=true");


            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            // Adding keys to the header 
            request.Headers.Add("X-Mashape-Key", "OM9KT6qZq0mshX55glqLqujP5PtBp1MtcfzjsnTl6UmlVkd8Tr");

            HttpWebResponse Response;

            try
            {
                Response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = ex.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string DateFact = reader.ReadToEnd();

            try
            {
                JObject JsonData = JObject.Parse(DateFact);
                ViewBag.Fact = /*(JObject)*/JsonData["text"];
            }
            catch (Exception ex)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = ex.Message;
                return View();
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}