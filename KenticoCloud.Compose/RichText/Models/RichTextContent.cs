using System;
using System.Collections.Generic;

namespace KenticoCloud.Compose.RichText.Models
{
    public class RichTextContent : IRichTextContent
    {
        public IEnumerable<IRichTextBlock> Blocks
        {
            get;
            set;
        }
    }
}
