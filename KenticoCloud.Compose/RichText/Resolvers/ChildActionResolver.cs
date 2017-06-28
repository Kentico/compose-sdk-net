using System.Web.Mvc;
using System.Web.Mvc.Html;

using KenticoCloud.Compose.RichText.Models;

namespace KenticoCloud.Compose.RichText.Resolvers
{
    public class ChildActionResolver<T> : IInlineContentResolver<HtmlHelper, T>
    {
        private readonly string _actionName;
        private readonly string _controllerName;

        public ChildActionResolver(string actionName, string controllerName)
        {
            _actionName = actionName;
            _controllerName = controllerName;
        }

        public string Resolve(HtmlHelper helper, ResolvedInlineData<T> data)
        {
            return helper.Action(_actionName, _controllerName, new { item = data.Data }).ToHtmlString();
        }
    }
}
