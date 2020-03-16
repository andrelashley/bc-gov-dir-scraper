using HtmlAgilityPack;
using Jint.Parser;
using Jint.Parser.Ast;
using ScrapySharp.Extensions;
using System;
using System.Linq;

namespace BCGovDirScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://dir.gov.bc.ca/gtds.cgi?show=Branch&organizationCode=FIN&organizationalUnitCode=GCPE+WCS";
            var webGet = new HtmlWeb();
            if (webGet.Load(url) is HtmlDocument document)
            {
                // var nodes = document.DocumentNode.CssSelect(".searchResultsLink").ToList();
                var nodes = document.DocumentNode.CssSelect("script").ToList();
                foreach (var node in nodes)
                {
                    // Console.WriteLine("Selling: " + node.CssSelect("h2 a").Single().InnerText);
                    if (node.InnerText.Contains("scramble_addr_img"))
                    {
                        // Console.WriteLine(node.InnerText);

                        var program = new JavaScriptParser().Parse(node.InnerText);
                        var body = program.Body;
                        Console.WriteLine(body.First().As<ExpressionStatement>().Expression.ToString());
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
