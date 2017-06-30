using System.ComponentModel.DataAnnotations;
using KenticoCloud.Compose.RichText.Models;
using System;

namespace KenticoCloud.Compose.RichText
{
    [DisableHtmlEncode]
    internal class HtmlContent : IHtmlContent
    {
        [DataType(DataType.Html)]
        public string Html { get; set; }


        public override string ToString()
        {
            return Html;
        }
    }
}