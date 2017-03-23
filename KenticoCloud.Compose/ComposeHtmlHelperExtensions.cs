using System;
using System.Configuration;
using System.Diagnostics;
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
            Endpoint = ConfigurationManager.AppSettings["ComposeEndpoint"] ?? GetDefaultEndPoint();
            HttpClient = CreateHttpClient();
        }

        public static async Task<HtmlString> EditableAreaAsync(this ExtensionPoint<HtmlHelper> helper, string areaId, string itemId)
        {
            if (string.IsNullOrEmpty(areaId))
            {
                throw new ArgumentException("Area ID must be a valid identifier.", nameof(areaId));
            }

            Guid itemGuid;
            if (!Guid.TryParse(itemId, out itemGuid) || (itemGuid == Guid.Empty))
            {
                throw new ArgumentException("Item ID must be a valid Guid.", nameof(areaId));
            }

            var script = Scripts.Render(Endpoint + ActivationScript);

            var url = Endpoint + $"editablearea?location={ProjectId}:{itemId}:{areaId}";
            var ct = helper.Target.ViewContext.HttpContext.Request.TimedOutToken;

            try
            {
                var content = await HttpClient.GetAsync(url, ct).ConfigureAwait(false);
                var html = await content.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (content.IsSuccessStatusCode)
                {
                    return new HtmlString(script + html);
                }

                var message = $"Error occurred while rendering editable area: {content.ReasonPhrase} ({(int)content.StatusCode})";
                Debug.WriteLine(message);
                Debug.WriteLine($"Failed request URL: {url}");

                return new HtmlString(GetErrorHtml(message));
            }
            catch (Exception ex)
            {
                var message = $"Error occurred while rendering editable area: {ex.Message}";

                Debug.WriteLine(message);
                Debug.WriteLine($"Failed request URL: {url}");
                Debug.WriteLine(ex.StackTrace);

                return new HtmlString(GetErrorHtml(message));
            }
        }

        private static string GetErrorHtml(string message)
        {
            return $"<div class=\"fx-component-notloaded\">{message}</div>";
        }

        public static HtmlString EditableArea(this ExtensionPoint<HtmlHelper> helper, string areaId, string itemId)
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

        private static string GetDefaultEndPoint()
        {
            return
                !string.IsNullOrEmpty(PreviewToken) ?
                    "https://previewkenticocomposedev.global.ssl.fastly.net/" :
                    "https://kenticocomposedev.global.ssl.fastly.net/";
        }


        private static Guid GetProjectId()
        {
            var id = ConfigurationManager.AppSettings["ProjectId"];
            Guid projectId;

            if (!Guid.TryParse(id, out projectId) || (projectId == Guid.Empty))
            {
                throw new InvalidOperationException("Project ID is not configured in app settings. Please add valid ProjectId to the app settings section of your config file.");
            }

            return projectId;
        }
    }
}