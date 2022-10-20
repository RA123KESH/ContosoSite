using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoSite.CustomHttpHelper
{
    public static class CustomHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string id, string src, string alt, string height, string width)
        {
            // Below line is used for generate new tag with img  
            var builder = new TagBuilder("img");

            // Below five lines are used for adding atrribute for img tag  
            builder.MergeAttribute("id", id);

            builder.MergeAttribute("src", src);

            builder.MergeAttribute("alt", alt);

            builder.MergeAttribute("height", height);

            builder.MergeAttribute("width", width);

            // this method will return MVChtmlstring and Create method will render as selfclosing tag.  

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        //public static MvcHtmlString MyLabel(this HtmlHelper helper, string target, string text)
        //{
        //    TagBuilder tag = new TagBuilder("label");
        //    tag.Attributes.Add("for", target);
        //    tag.SetInnerText(text);
        //    return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        //    //return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        //    //return String.Format("<label for='{0}'>{1}</label>", target, text);
        //}

    }
}