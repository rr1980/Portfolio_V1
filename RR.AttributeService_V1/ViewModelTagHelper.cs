using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace RR.AttributeService_V1
{
    [HtmlTargetElement("vm-input", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ViewModelTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public ModelExpression VmTarget { get; set; }
        public string VmClasses { get; set; }
        public string VmType { get; set; } = "text";
        public bool VmSm { get; set; } = true;
        private IHtmlGenerator Generator { get; }

        public ViewModelTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            _setClass(output, "form-group" + (VmSm == true ? " form-group-sm" : ""));
            _setClass(output, VmClasses);

            var label = new TagBuilder("label");
            label.Attributes.Add("for", VmTarget.Name);
            label.Attributes.Add("class", "col-md-2 control-label");
            label.InnerHtml.AppendHtml(VmTarget.Name);
            
            var input = new TagBuilder("input");
            input.Attributes.Add("type", VmType);
            input.Attributes.Add("class", "form-control");
            input.Attributes.Add("name", VmTarget.Name);
            input.Attributes.Add("id", VmTarget.Name);
            input.Attributes.Add("value", VmTarget.Model as string);

            
            var span = Generator.GenerateValidationMessage(
                 ViewContext,
                 VmTarget.ModelExplorer,
                 VmTarget.Name,
                 message: null,
                 tag: null,
                 htmlAttributes: null);

            var div = new TagBuilder("div");
            div.Attributes.Add("class", "col-md-10");
            div.InnerHtml.AppendHtml(input);
            div.InnerHtml.AppendHtml(span);

            output.Content.SetHtmlContent(label);
            output.Content.AppendHtml(div);
            //output.Content.AppendHtml(input);
        }

        private static void _setClass(TagHelperOutput output, string classNames)
        {
            if (output.Attributes.ContainsName("class"))
            {
                classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
            }
            output.Attributes.SetAttribute("class", classNames);
        }
    }
}

