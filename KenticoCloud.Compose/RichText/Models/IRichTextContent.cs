using System.Collections.Generic;

namespace KenticoCloud.Compose.RichText.Models
{
    public interface IRichTextContent
    {
        IEnumerable<IRichTextBlock> Blocks { get; set; }
    }
}
