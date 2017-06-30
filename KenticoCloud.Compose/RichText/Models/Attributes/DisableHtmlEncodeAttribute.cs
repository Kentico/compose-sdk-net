using System;
using System.ComponentModel.DataAnnotations;

namespace KenticoCloud.Compose.RichText.Models
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class DisableHtmlEncodeAttribute : DisplayFormatAttribute
    {
        public DisableHtmlEncodeAttribute()
        {
            HtmlEncode = false;
        }
    }
}
