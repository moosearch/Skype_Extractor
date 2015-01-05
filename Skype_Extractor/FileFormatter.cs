using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Skype_Extractor
{
    class FileFormatter
    {    
        public static string Format(string line)
        {
            // Links
            // <a href="http://www.google.ca">http://www.google.ca</a>
            line = FormatLink(line);

            Dictionary<string, string> symbols = BuildSymbolDictionary();
            line = ReplaceSymbol(line, "&apos;", symbols); // Apostrophes (&apos;)
            line = ReplaceSymbol(line, "&quot;", symbols); // Double Quotations (&quot;)
            line = ReplaceSymbol(line, "&gt;", symbols); // greater than sign (&gt;)
            line = ReplaceSymbol(line, "&lt;", symbols); // less than sign (&lt;)
            line = ReplaceSymbol(line, "&amp;", symbols); // Ampersand (&amp;)

            return line;
        }

        /// <summary>
        /// Removes the HTML syntax for a given URL.
        /// </summary>
        /// <param name="line"></param> The string that contains HTML formatting for a URL.
        /// <returns>The string without any HTML-formatted links. </returns>
        private static string FormatLink(string line)
        {
            Regex r = new Regex(@"<a href="".*"">(?<Link>(.*))</a>");
            Console.WriteLine(r.IsMatch(line));
            Match m = r.Match(line);

            int n = r.GroupNumberFromName("Link");
            Group gg = m.Groups[n];
            Console.WriteLine("Value: " + gg.Value);
            string result = r.Replace(line, gg.Value);

            return result;
        }

        /// <summary>
        /// Replaces symbols with the help of the dictionary
        /// </summary>
        /// <param name="line"></param> A string that contains symbols needing to be replaced
        /// <param name="symbol"></param> The desired symbol to be replaced
        /// <param name="list"></param> Dictionary of symbols that would need replacing
        /// <returns>The string without any of the specified symbols. </returns>
        private static string ReplaceSymbol(string line, string symbol, Dictionary<string, string> list)
        {
            Regex r = new Regex(symbol);
            if (r.IsMatch(line))
            {
                string replacement = "";
                list.TryGetValue(symbol, out replacement);

                string result = r.Replace(line, replacement);
                return result;
            }
            return line;
        }

        /// <summary>
        /// Populates a dictionary with symbols/character to replace
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> BuildSymbolDictionary()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("&amp;", "&");
            list.Add("&gt;", ">");
            list.Add("&lt;", "<");
            list.Add("&apos;", "\'");
            list.Add("&quot;", "\"");
            return list;
        }

    }
}