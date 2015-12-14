using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using RimDev.Releases.Infrastructure.GitHub;

namespace RimDev.Releases.Infrastructure
{
    public static class HtmlExtensions {
        public static async Task<HtmlString> MarkdownAsync<T>(
            this IHtmlHelper<T> value,
            string context,
            string markdown)
            {
                var client = value.ViewContext.HttpContext.ApplicationServices.GetService(typeof(Client)) as Client;
                string html = string.Empty;
                
                if (!string.IsNullOrWhiteSpace(context) && !string.IsNullOrWhiteSpace(markdown))
                {                    
                    html = await client.RenderMarkdown(markdown, context);
                }
                
                return new HtmlString(html);
            }
    }
}