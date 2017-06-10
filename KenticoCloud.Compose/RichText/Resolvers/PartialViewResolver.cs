using KenticoCloud.Compose.RichText.Models;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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
            try
            {
                return helper.Partial(_viewName, data.Data).ToHtmlString();
            }
            catch (Exception ex)
            {
                return $"Failed to resolve inline content of type {data.Data.GetType().Name}. {ex.Message}";
            }
        }
    }
}
