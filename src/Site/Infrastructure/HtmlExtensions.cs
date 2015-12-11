using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using Octokit;

namespace RimDev.Releases.Infrastructure
{
    public static class HtmlExtensions {
        public static async Task<HtmlString> MarkdownAsync<T>(
            this IHtmlHelper<T> value,
            string context,
            string markdown)
            {
                var client = value.ViewContext.HttpContext.ApplicationServices.GetService(typeof(GitHubClient)) as GitHubClient;
                string html = string.Empty;
                
                if (!string.IsNullOrWhiteSpace(context) && !string.IsNullOrWhiteSpace(markdown))
                {
                    var r = new NewArbitraryMarkdown(markdown, "gfm", context);
                    html = await client.Miscellaneous.RenderArbitraryMarkdown(r);
                }
                
                return new HtmlString(html);
            }
    }
}