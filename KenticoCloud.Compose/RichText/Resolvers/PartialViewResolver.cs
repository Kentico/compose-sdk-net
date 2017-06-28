using System.Web.Mvc;
using System.Web.Mvc.Html;

using KenticoCloud.Compose.RichText.Models;

namespace KenticoCloud.Compose.RichText.Resolvers
{
    public class PartialViewResolver<T> : IInlineContentResolver<HtmlHelper, T>
    {
        private string _viewName;

        public PartialViewResolver(string viewName)
        {
            _viewName = viewName;
        }

        public string Resolve(HtmlHelper helper, ResolvedInlineData<T> data)
        {
            return helper.Partial(_viewName, data.Data).ToHtmlString();
        }
    }
}
