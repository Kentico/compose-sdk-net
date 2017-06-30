using System;
using System.ComponentModel.DataAnnotations;

namespace KenticoCloud.Compose.RichText.Models
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class UseDisplayTemplateAttribute : UIHintAttribute
    {
        public UseDisplayTemplateAttribute(string uiHint)
            : base(uiHint)
        {
        }
    }
}
