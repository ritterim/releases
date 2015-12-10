using MarkdownSharp;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Octokit;
using System.Threading.Tasks;

namespace RimDev.Releases.Infrastructure
{
    [HtmlTargetElement("markdown", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement(Attributes = "markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        public MarkdownTagHelper(
            GitHubClient client,
            ILogger<MarkdownTagHelper> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private readonly GitHubClient client;
        public ModelExpression Content { get; set; }
        private readonly ILogger<MarkdownTagHelper> logger;

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (output.TagName == "markdown")
            {
                output.TagName = null;
            }

            output.Attributes.RemoveAll("markdown");

            TagHelperAttribute attribute;
            string html;
            var markdown = await GetContent(output);

            if (output.Attributes.TryGetAttribute("context", out attribute))
            {
                logger.LogInformation($"github context : {attribute.Value}");
                var r = new NewArbitraryMarkdown(markdown, "gfm", attribute.Value.ToString());
                html = await client.Miscellaneous.RenderArbitraryMarkdown(r);
            }
            else
            {
                logger.LogInformation($"defaulting to regular markdown.");
                html = new Markdown().Transform(markdown);
            }

            output.Content.SetHtmlContent(html ?? "");
        }

        private async Task<string> GetContent(TagHelperOutput output)
        {
            if (Content == null)
                return (await output.GetChildContentAsync()).GetContent();

            return Content.Model?.ToString();
        }
    }
}