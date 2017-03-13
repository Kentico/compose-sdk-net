using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace KenticoCloud.Compose
{
    public static class ComposeHtmlHelperExtensions
    {
        private const string ActivationScript = "compose.js";

        private static readonly HttpClient HttpClient;

        private static readonly string Endpoint;

        private static readonly Guid ProjectId;

        private static readonly string PreviewToken;

        static ComposeHtmlHelperExtensions()
        {
            ProjectId = GetProjectId();
            PreviewToken = ConfigurationManager.AppSettings["PreviewToken"];
            Endpoint = ConfigurationManager.AppSettings["ComposeEndpoint"];
            HttpClient = CreateHttpClient();
        }

        public static async Task<HtmlString> EditableAreaAsync(this HtmlHelper helper, string areaId, string itemId)
        {
            var script = Scripts.Render(Endpoint + ActivationScript);

            var url = Endpoint + $"widgets/editablearea?location={ProjectId}:{itemId}:{areaId}";
            var ct = helper.ViewContext.HttpContext.Request.TimedOutToken;

            try
            {
                var content = await HttpClient.GetAsync(url, ct).ConfigureAwait(false);
                var html = await content.Content.ReadAsStringAsync().ConfigureAwait(false);

                return new HtmlString(script + html);
            }
            catch (Exception ex)
            {
                return new HtmlString(ex.Message);
            }
        }

        public static HtmlString EditableArea(this HtmlHelper helper, string areaId, string itemId)
        {
            return helper.EditableAreaAsync(areaId, itemId).Result;
        }

        private static HttpClient CreateHttpClient()
        {
            var http = new HttpClient();

            if (!string.IsNullOrEmpty(PreviewToken))
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", PreviewToken);
            }

            return http;
        }

        private static Guid GetProjectId()
        {
            var id = ConfigurationManager.AppSettings["ProjectId"];
            Guid projectId;

            Guid.TryParse(id, out projectId);

            return projectId;
        }
    }
}