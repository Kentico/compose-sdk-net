using System;
using System.Web;
using System.Web.Mvc;

using KenticoCloud.Compose.RichText.Models;
using System.Text;

namespace KenticoCloud.Compose.RichText
{
    public static class RichTextHtmlHelperExtensions
    {
        public static HtmlString RichText(this ExtensionPoint<HtmlHelper> helper, IRichTextContent richText, RichTextProcessor<HtmlHelper> resolver = null)
        {
            var output = new StringBuilder();
            resolver = resolver ?? RichTextProcessor<HtmlHelper>.Default;

            foreach (var block in richText.Blocks)
            {
                string html = null;

                var modular = block as IInlineContentItem;
                if (modular != null)
                {
                    html = resolver.ResolveModular(helper.Target, modular);
                }
                else
                {
                    var image = block as IInlineImage;
                    if (image != null)
                    {
                        html = resolver.ResolveImage(helper.Target, image);
                    }
                    else
                    {
                        var htmlBlock = block as IHtmlContent;
                        if (htmlBlock != null)
                        {
                            html = htmlBlock.Html;
                        }
                        else
                        {
                            throw new NotSupportedException($"Unknown block type {block.GetType()}");
                        }
                    }
                }

                output.Append(html);
            }

            return new HtmlString(output.ToString());
        }
    }
}