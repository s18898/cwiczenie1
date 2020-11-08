using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cwiczenie_1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if(args.Length==0) throw new ArgumentNullException("The URL link was not given.");
            bool result=Uri.TryCreate(args[0], UriKind.Absolute, out Uri uriResult)&&(uriResult.Scheme==Uri.UriSchemeHttp||uriResult.Scheme==Uri.UriSchemeHttps);
            if(!result) throw new ArgumentException("Invalid URL link.");
            using HttpClient client=new HttpClient();
            HttpResponseMessage response=await client.GetAsync(args[0]);
            if(response.StatusCode==HttpStatusCode.OK)
            {
                string content=await response.Content.ReadAsStringAsync();
                var matches=Regex.Matches(content, "[a-z]+@[a-z.]+").Select(m=>m.Value).Distinct().ToList();
                if(matches.Count()!=0) matches.ForEach(match=>Console.WriteLine(match));
                else Console.WriteLine("No emails found.");
            }
            else throw new ArgumentException("Invalid response.");
        }
    }
}
