using MTOS.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MTOS
{    
    static public class ViewHelper
    {
        static public MvcHtmlString ButtonBuilder(this HtmlHelper helper,
            ButtonType type, string id, string labelText)
        {
            var btn = new TagBuilder("button");
            btn.MergeAttribute("type", type.ToString());
            btn.MergeAttribute("id", id);
            btn.MergeAttribute("class", "btn btn-primary");

            return MvcHtmlString.Create(btn.ToString(TagRenderMode.StartTag) + labelText + btn.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString TextBoxBuilder<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, string labelText)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            var input = helper.TextBoxFor(expression, new { @class = "form-control" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString DropDownListBuilder<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string labelText, bool optionalBlank = false)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            if (optionalBlank)
            {
                var xBlank = new SelectListItem() { Value = string.Empty, Text = string.Empty };
                selectList = xBlank.ToEnumerable().Concat(selectList).OrderBy(o => o.Value);
            }

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            var input = helper.DropDownListFor(expression, selectList, new { @class = "form-control" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString TextAreaBuilder<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, string labelText)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            var input = helper.TextAreaFor(expression, new { @class = "form-control" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString NumberBoxBuilder<TModel>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, decimal>> expression, string labelText)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            string xName = ExpressionHelper.GetExpressionText(expression);
            decimal xValue = expression.Compile().Invoke(helper.ViewData.Model);
            var input = helper.TextBox(xName, xValue, new { type = "number", @class = "form-control", onkeypress = "return event.charCode >= 48" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString IntegerBoxBuilder<TModel>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, int>> expression, string labelText)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            string xName = ExpressionHelper.GetExpressionText(expression);
            decimal xValue = expression.Compile().Invoke(helper.ViewData.Model);
            var input = helper.TextBox(xName, xValue, new { type = "number", @class = "form-control", onkeypress = "return event.charCode >= 48" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString DateBoxBuilder<TModel>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, DateTime>> expression, string labelText)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            string xName = ExpressionHelper.GetExpressionText(expression);
            DateTime xDate = expression.Compile().Invoke(helper.ViewData.Model);
            var input = helper.TextBox(xName, xDate.ToString("yyyy-MM-dd"), new { type = "date", @class = "form-control" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString DateBoxBuilder(this HtmlHelper helper,
            string name, string labelText, DateTime value)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.Label(name, labelText).ToHtmlString();
            var input = helper.TextBox(name, value.ToString("yyyy-MM-dd"), new { type = "date", @class = "form-control" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString DateBoxBuilder<TModel>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, DateTime?>> expression, string labelText)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            string xName = ExpressionHelper.GetExpressionText(expression);
            DateTime? xDate = expression.Compile().Invoke(helper.ViewData.Model);
            var input = helper.TextBox(xName, (xDate.HasValue ? xDate.Value.ToString("yyyy-MM-dd") : string.Empty), new { type = "date", @class = "form-control" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString DateBoxBuilder(this HtmlHelper helper,
            string name, string labelText, DateTime? value)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.Label(name, labelText).ToHtmlString();
            var input = helper.TextBox(name, (value.HasValue ? value.Value.ToString("yyyy-MM-dd") : string.Empty), new { type = "date", @class = "form-control" }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString AutoCompleteBuilder<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, string labelText, string[] collection)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "form-group");

            var label = helper.LabelFor(expression, labelText).ToHtmlString();
            var input = helper.TextBoxFor(expression, new { @class = "form-control", data_provide = "typeahead", data_source = helper.Raw(Json.Encode(collection)) }).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
        }

        static public MvcHtmlString AutoCompleteBuilder(this HtmlHelper helper, string name, string labelText, string[] collection)
        {
            //var div = new TagBuilder("div");
            //div.MergeAttribute("class", "form-group");

            //var label = helper.Label(name, labelText).ToHtmlString();
            var input = helper.TextBox(name, "", new { @class = "form-control", data_provide = "typeahead", data_source = helper.Raw(Json.Encode(collection)) }).ToHtmlString();

            //return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + label + input + div.ToString(TagRenderMode.EndTag));
            return MvcHtmlString.Create(input);
        }

        static public MvcHtmlString GridFilterBuilder(this HtmlHelper helper, string gridid)
        {
            return helper.Partial("GridFilter", gridid);
        }

        static public MvcHtmlString AccordionBuilder(this HtmlHelper helper, Func<object, object> value, string titleTitle)
        {
            return helper.Partial("Accordion", Tuple.Create(value, titleTitle));
        }

        static public MvcHtmlString UploadFileBuilder(this HtmlHelper helper, 
            string name,
            string labelText)
        {
            //var div = new TagBuilder("div");
            //div.MergeAttribute("class", "form-group");

            //var inputlabel = helper.Label(labelText, new { @class = "custom-file-label", @for = name }).ToHtmlString();
            //var input = new TagBuilder("input");
            //input.MergeAttribute("type", "file");
            //input.MergeAttribute("class", "custom-file-input");
            //input.MergeAttribute("id", name);
            //input.MergeAttribute("name", name);

            //return MvcHtmlString.Create(div.ToString(TagRenderMode.StartTag) + input + inputlabel + div.ToString(TagRenderMode.EndTag));
            return helper.Partial("Upload", Tuple.Create(name, labelText));
        }

        static public string ControllerName(this HtmlHelper helper)
        {
            return helper.ViewContext.RouteData.Values["controller"].ToString();
        }
    }
}