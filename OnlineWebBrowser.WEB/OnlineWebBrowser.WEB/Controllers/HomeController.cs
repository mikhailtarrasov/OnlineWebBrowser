using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace OnlineWebBrowser.WEB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string websiteAddress)
        {
            string head = String.Empty;
            string body = String.Empty;

            try
            {
                var websiteUri = new Uri(websiteAddress);

                var web = new HtmlWeb();
                var doc = web.Load(websiteUri.AbsoluteUri);

                SaveImagesFromHtml(doc, websiteUri);

                //var xmlHeadNode = doc.DocumentNode.SelectSingleNode("head");
                //if (xmlHeadNode != null)
                //    head = xmlHeadNode.InnerHtml;
                //var xmlBodyNode = doc.DocumentNode.SelectSingleNode("body");
                //if (xmlBodyNode != null)
                //    body = xmlBodyNode.InnerHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ViewBag.Head = head;
            ViewBag.Body = body;
            return View();
        }

        public void SaveImagesFromHtml(HtmlDocument html, Uri uri)
        {
            var images = html.DocumentNode.SelectNodes("//img");
            var baseUri = new Uri(uri.AbsoluteUri.Replace(uri.PathAndQuery, "/"));

            var imagesDir = @"C:\Users\Mikhail\Source\Repos\OnlineWebBrowser\OnlineWebBrowser.WEB\OnlineWebBrowser.WEB\Content\Images cache\" + uri.Host + "\\";
            if (!Directory.Exists(imagesDir))
                Directory.CreateDirectory(imagesDir);

            try
            {
                var webClient = new WebClient();
                for (int imageN = 0; imageN < images.Count; imageN++)
                {
                    var imageSrc = images[imageN].Attributes["src"].Value;
                    Uri tempUri;
                    Uri.TryCreate(imageSrc, UriKind.RelativeOrAbsolute, out tempUri);
                    var absoluteUri = !tempUri.IsAbsoluteUri ? new Uri(baseUri, tempUri) : tempUri;

                    var imagePath = imagesDir + imageN + ".png";
                    webClient.DownloadFile(absoluteUri, imagePath);
                }
            }
            catch (ArgumentNullException argNullEx)
            {
                throw argNullEx;
            }
            catch (WebException webEx)
            {
                throw webEx;
            }
            catch (NotSupportedException notSuppEx)
            {
                throw notSuppEx;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}