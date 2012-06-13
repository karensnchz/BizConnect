// Type: System.Web.Mvc.Html.PartialExtensions
// Assembly: System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: c:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies\System.Web.Mvc.dll

using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Represents the functionality to render a partial view as an HTML-encoded string.
    /// </summary>
    public static class PartialExtensions
    {
        /// <summary>
        /// Renders the specified partial view as an HTML-encoded string.
        /// </summary>
        /// 
        /// <returns>
        /// The partial view that is rendered as an HTML-encoded string.
        /// </returns>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param><param name="partialViewName">The name of the partial view to render.</param>
        public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName);

        /// <summary>
        /// Renders the specified partial view as an HTML-encoded string.
        /// </summary>
        /// 
        /// <returns>
        /// The partial view that is rendered as an HTML-encoded string.
        /// </returns>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param><param name="partialViewName">The name of the partial view to render.</param><param name="viewData">The view data dictionary for the partial view.</param>
        public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData);

        /// <summary>
        /// Renders the specified partial view as an HTML-encoded string.
        /// </summary>
        /// 
        /// <returns>
        /// The partial view that is rendered as an HTML-encoded string.
        /// </returns>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param><param name="partialViewName">The name of the partial view to render.</param><param name="model">The model for the partial view.</param>
        public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, object model);

        /// <summary>
        /// Renders the specified partial view as an HTML-encoded string.
        /// </summary>
        /// 
        /// <returns>
        /// The partial view that is rendered as an HTML-encoded string.
        /// </returns>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param><param name="partialViewName">The name of the partial view.</param><param name="model">The model for the partial view.</param><param name="viewData">The view data dictionary for the partial view.</param>
        public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData);
    }
}
