using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

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
        public int VmColumn { get; set; } = 12;
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

            _setClass(output, (VmSm == true ? " form-group-sm" : "form-group") + (" col-xs-"+ VmColumn) + (" col-lg-" + VmColumn));
            _setClass(output, VmClasses);


            var label = Generator.GenerateLabel(ViewContext,
                 VmTarget.ModelExplorer,
                 VmTarget.Name,
                 labelText: VmTarget.Name,
                 htmlAttributes: new Dictionary<string, object>
                 {
                     { "class", "col-xs-12 col-sm-3 col-md-2 col-lg-2 control-label" }
                 });


            var input = Generator.GenerateTextBox(ViewContext,
                 VmTarget.ModelExplorer,
                 VmTarget.Name,
                 value: VmTarget.Model as string,
                 format: null,
                 htmlAttributes: new Dictionary<string, object>
                 {
                     { "type", VmType },
                     { "class", "form-control" }
                 });

            var span = Generator.GenerateValidationMessage(
                 ViewContext,
                 VmTarget.ModelExplorer,
                 VmTarget.Name,
                 message: null,
                 tag: null,
                 htmlAttributes: new Dictionary<string, object>
                 {
                     { "class", "text-danger" }
                 });

            var div = new TagBuilder("div");
            div.Attributes.Add("class", "col-xs-12 col-sm-9 col-md-10 col-lg-10");
            div.InnerHtml.AppendHtml(input);
            div.InnerHtml.AppendHtml(span);

            output.Content.SetHtmlContent(label);
            output.Content.AppendHtml(div);
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

