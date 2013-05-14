using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using SitemapParse.Models;
using System.Data.Entity;
using System.Data;
using HtmlAgilityPack;


namespace SitemapParse.Controllers
{
    public class URLController : Controller
    {
        //definitions for current date for filename 
        //when files are saved 
        String _day = DateTime.Today.Day.ToString();
        String _month = DateTime.Today.Month.ToString();
        String _year = DateTime.Today.Year.ToString();

        private URLContext db = new URLContext();
        //
        // GET: /URL/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Parse()
        {
            return View();
        }


        public ActionResult Create(string url)
        {
            //
            GetLinks_FromURL(url);
            return View("Parse");
        }

        public void GetLinks_FromURL(string url)
        {
            try
            {
                string output = "";

                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = hw.Load(url);
                //finds all link of url loaded into 'doc'
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    //each link with attribute 'href'
                    //add the value to the string for the html scaffolding
                    HtmlAttribute att = link.Attributes["href"];
                    output += "\r\n<tr>\r\n\t<td>open</td>"
                                       + "\r\n\t<td>/</td>"
                                       + "\r\n\t<td></td>"
                                       + "\r\n</tr>";

                    output += "\r\n<tr>\r\n\t<td>clickAndWait</td>"
                                       + "\r\n\t<td>link=" + att.Value + "</td>"
                                       + "\r\n\t<td></td>"
                                       + "\r\n</tr>";
                }

                //writer for the stc file
                StreamWriter swSTC = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +  @"\Selenium Files\" + _month + "-" + _day + "-" + _year + ".stc");
                //writer for the text file for the preview
                StreamWriter swTXT = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Selenium Files\" + _month + "-" + _day + "-" + _year + ".txt");

                //write stc file
                using (swSTC)
                {
                    swSTC.Write(output);
                }
                //write text file for preview
                using (swTXT)
                {
                    swTXT.Write(output);
                }

            }
            catch
            {
            }
        }
    }
}




