using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Crawler();
            Console.ReadLine();
        }

        private static async Task Crawler()
        {
            var url = "https://devgo.com.br/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
          var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("Class", "")
                .Equals("blog-article-card css-g70b67")).ToList();


            
            var sites = new List<Links>();
           
            foreach (var div in divs)
            {
                var site = new Links
                {
                     name = div?.Descendants("h1")?.FirstOrDefault().InnerText,
                     link = div?.Descendants("a")?.FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value,
                   
                    
                };

			
                sites.Add(site);
 
            }
        }
    }

    public class Links
    {
       public string name { get; set; }
       public string link { get; set; }
    }
}
