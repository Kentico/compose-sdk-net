using System;
using System.Reflection;

using KenticoCloud.Delivery;

using Newtonsoft.Json.Linq;
using KenticoCloud.Compose.RichText.Models;
using AngleSharp.Parser.Html;
using System.Collections.Generic;
using System.Linq;

namespace KenticoCloud.Compose.RichText
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RichTextContentAttribute : Attribute, IPropertyValueConverter
    {
        public object GetPropertyValue(PropertyInfo property, JToken propValue, Func<string, object> getContentItem, IDeliveryClient client)
        {
            if (!typeof(IRichTextContent).IsAssignableFrom(property.PropertyType))
            {
                throw new InvalidOperationException($"Type of property {property.Name} must implement {nameof(IRichTextContent)} in order to receive rich text content.");
            }

            var value = propValue?.ToObject<string>();
            var links = ((JObject)propValue?.Parent?.Parent)?.Property("links")?.Value;

            // Handle rich_text link resolution
            if (links != null && propValue != null && client.ContentLinkUrlResolver != null)
            {
                value = new ContentLinkResolver(client.ContentLinkUrlResolver).ResolveContentLinks(value, links);
            }

            var blocks = new List<IRichTextBlock>();

            var htmlInput = new HtmlParser().Parse(value);
            foreach (var block in htmlInput.Body.Children)
            {
                if (block.TagName?.Equals("object", StringComparison.OrdinalIgnoreCase) == true && block.GetAttribute("type") == "application/kenticocloud" && block.GetAttribute("data-type") == "item")
                {
                    var codename = block.GetAttribute("data-codename");
                    blocks.Add(new InlineContentItem { ContentItem = getContentItem(codename) });
                }
                else if (block.TagName?.Equals("figure", StringComparison.OrdinalIgnoreCase) == true)
                {
                    var img = block.Children.FirstOrDefault(child => child.TagName?.Equals("img", StringComparison.OrdinalIgnoreCase) == true);
                    if (img != null)
                    {
                        blocks.Add(new InlineImage { Src = img.GetAttribute("src"), AltText = img.GetAttribute("alt") });
                    }
                }
                else
                {
                    blocks.Add(new HtmlContent { Html = block.OuterHtml });
                }
            }

            return new RichTextContent
            {
                Blocks = blocks
            };
        }
    }
}
