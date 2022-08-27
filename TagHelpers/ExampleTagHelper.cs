using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HotReloadTest.TagHelpers;

[HtmlTargetElement("example", Attributes = "text")]
public class ExampleTagHelper : TagHelper
{
    public string? Text { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetContent(Text);
    }
}
